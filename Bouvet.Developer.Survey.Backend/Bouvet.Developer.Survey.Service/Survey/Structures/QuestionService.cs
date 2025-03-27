using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class QuestionService : IQuestionService
{
    private readonly DeveloperSurveyContext _context;
    private readonly IChoiceService _choiceService;

    public QuestionService(DeveloperSurveyContext context, IChoiceService choiceService)
    {
        _context = context;
        _choiceService = choiceService;
    }

    public async Task CheckForDifference(SurveyQuestionsDto surveyQuestionsDto, Domain.Entities.Survey.Survey survey)
    {
        var allQuestionToSurvey = await _context.Questions
            .Where(q => q.SurveyId == survey.SurveyId)
            .ToListAsync();

        if(surveyQuestionsDto.SurveyElements == null) return;

        foreach (var surveyElement in surveyQuestionsDto.SurveyElements)
        {
            var question = allQuestionToSurvey.FirstOrDefault(q => q.QuestionId == surveyElement.PrimaryAttribute);

            // Not adding questions with DataExportTag "Arbeidsrolle" or "Enhet" to the survey because of confidentiality
            if(surveyElement.Payload is { DataExportTag: "Arbeidsrolle" } or { DataExportTag: "Enhet" } ) continue;

            //Add new questions to survey
            if (question == null)
            {
                Console.WriteLine("Creating new question");
                var newQuestion = new NewQuestionDto
                {
                    BlockElementId = surveyElement.PrimaryAttribute,
                    QuestionId = surveyElement.PrimaryAttribute,
                    SurveyId = survey.SurveyId,
                    IsMultipleChoice = surveyElement.Payload != null && surveyElement.Payload.Answers?.Count > 0,
                    DataExportTag = surveyElement.Payload != null ? surveyElement.Payload.DataExportTag : string.Empty,
                    QuestionText = surveyElement.Payload != null ? surveyElement.Payload.QuestionText : string.Empty,
                    QuestionDescription = surveyElement.Payload != null
                        ? surveyElement.Payload.QuestionDescription
                        : string.Empty,
                    Choices = surveyElement.Payload?.Choices.Select(c => new NewChoiceDto
                    {
                        IndexId = c.Key,
                        Text = c.Value.Display
                    }).ToList()
                };

                await CreateQuestionAsync(newQuestion);
            }
            // Check for differences in existing questions
            else
            {
                var updateQuestion = new NewQuestionDto
                {
                    BlockElementId = surveyElement.PrimaryAttribute,
                    SurveyId = survey.SurveyId,
                    IsMultipleChoice = surveyElement.Payload != null && surveyElement.Payload.Answers?.Count > 0,
                    DataExportTag = surveyElement.Payload != null ? surveyElement.Payload.DataExportTag : string.Empty,
                    QuestionText = surveyElement.Payload != null ? surveyElement.Payload.QuestionText : string.Empty,
                    QuestionDescription = surveyElement.Payload != null
                        ? surveyElement.Payload.QuestionDescription
                        : string.Empty,
                    Choices = surveyElement.Payload?.Choices.Select(c => new NewChoiceDto
                    {
                        IndexId = c.Key,
                        Text = c.Value.Display
                    }).ToList()
                };

                await UpdateQuestionAsync(question.Id, updateQuestion);
            }

            //Delete questions that are not in the survey
            foreach (var questionToBeDeleted in allQuestionToSurvey)
            {
                if (surveyQuestionsDto.SurveyElements.All(q => q.PrimaryAttribute != questionToBeDeleted.QuestionId))
                {
                    Console.WriteLine("Deleting question with id: " + questionToBeDeleted.Id);
                    await DeleteQuestionAsync(questionToBeDeleted.Id);
                }
            }

            //Check for differences in choices
            if (question != null && surveyElement.Payload != null)
            {
                await _choiceService.CheckForDifferences(question.Id, surveyElement.Payload);
            }
        }

    }

    public async Task<QuestionDetailsDto> CreateQuestionAsync(NewQuestionDto newQuestionDto)
    {
        var blockElement = await _context.BlockElements.FirstOrDefaultAsync(be => be.QuestionId == newQuestionDto.BlockElementId);

        if (blockElement == null) throw new NotFoundException("Block element not found");

        var question = new Question
        {
            Id = Guid.NewGuid(),
            BlockElementId = blockElement.Id,
            SurveyId = newQuestionDto.SurveyId,
            IsMultipleChoice = newQuestionDto.IsMultipleChoice,
            QuestionId = newQuestionDto.QuestionId,
            DataExportTag = newQuestionDto.DataExportTag,
            QuestionText = newQuestionDto.QuestionText,
            QuestionDescription = newQuestionDto.QuestionDescription,
            CreatedAt = DateTimeOffset.Now
        };

        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();

        if(newQuestionDto.Choices != null)
            await _choiceService.CreateChoice(newQuestionDto.Choices, question.Id);

        var dto = QuestionDetailsDto.CreateFromEntity(question);

        return dto;
    }

    public async Task<QuestionResponseDto> GetQuestionByIdAsync(Guid questionId)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);

        if (question == null) throw new NotFoundException("Question not found");

        var choiceDtoS = await CreateChoicesAsync(questionId, question);

        var dto = QuestionResponseDto.CreateFromEntity(question, choiceDtoS);

        return dto;
    }

    private async Task<ICollection<GetChoiceDto>> CreateChoicesAsync(Guid questionId, Question question)
    {
        var choices = await _context.Choices
            .Where(c => c.QuestionId == questionId)
            .ToListAsync();

        var choiceList = new List<GetChoiceDto>();

        foreach (var choice in choices)
        {
            var tbd = await CreateChoiceStatsAsync(choice.Id, question);
            var choiceDto = GetChoiceDto.CreateFromEntity(choice, tbd);

            choiceList.Add(choiceDto);
        }

        return choiceList;
    }

    private async Task<AnswerDto> CreateChoiceStatsAsync(Guid choiceId, Question question)
    {
		// This is the responses for a specific choice
		// So all the responses are the same
        var responses = await _context.Responses
            .Where(r => r.ChoiceId == choiceId)
            .ToListAsync();

        var numberOfRespondents = await _context.ResponseUsers
            .Where(r => r.QuestionId == question.Id)
            .Select(r => r.UserId)
            .Distinct()
            .CountAsync();

        var totalAdmired = responses.Count(r => r.HasWorkedWith && r.WantsToWorkWith);
        var totalDesired = responses.Count(r => r.WantsToWorkWith && !r.HasWorkedWith);
        var admiredRespondents = responses.Count(r => r.HasWorkedWith);

        var totalColumns = responses.Count;

        var admiredPercentage = admiredRespondents > 0
            ? (int)Math.Ceiling(totalAdmired / (float)admiredRespondents * 100)
            : 0;

        var desiredPercentage = totalColumns > 0
            ? (int)Math.Ceiling(totalDesired / (float)totalColumns * 100)
            : 0;

        // Adjust totalPercentage calculation based on IsMultipleChoice flag
        var totalPercentage = 0;

        if (question.IsMultipleChoice)
        {
            // If it's multiple choice, calculate percentage based on admiredRespondents and numberOfRespondents
            totalPercentage = admiredRespondents > 0
                ? (int)Math.Ceiling(admiredRespondents / (float)numberOfRespondents * 100)
                : 0;
        }
        else
        {
            // If it's not multiple choice, calculate percentage based on the response values
            var totalValue = responses.Sum(r => r.Value); // Sum of values for non-multiple-choice
            totalPercentage = totalValue > 0
                ? (int)Math.Ceiling((totalValue / (float)numberOfRespondents) * 100) // Normalize the value to a percentage
                : 0;
		}

		// Add multiple choide responses array
		var questionResponses = new List<GetResponseDto>();

		if (question.IsMultipleChoice && responses.Count(r => r.HasWorkedWith) > 0)
		{
			questionResponses.Add(new GetResponseDto
				{
					Index = 0,
					Text = "Jobbet med SISTE året",
					Percentage = totalPercentage,
				}
			);
		}

		if (question.IsMultipleChoice && responses.Count(r => r.WantsToWorkWith) > 0)
		{
			questionResponses.Add(new GetResponseDto
				{
					Index = 1,
					Text = "Ønsker å jobbe med NESTE året",
					Percentage = (int)Math.Ceiling((totalAdmired + totalDesired)/ (float)numberOfRespondents * 100),
				}
			);
		}

		var choiceText = await _context.Choices
			.Where(c => c.Id == choiceId)
			.Select(c => c.Text)
			.FirstOrDefaultAsync();

		if (!question.IsMultipleChoice)
		{
			questionResponses.Add(new GetResponseDto
				{
					Index = 0,
					Text = choiceText,
					Percentage = totalPercentage
				}
			);
		}


		return new AnswerDto{
			Stats = new GetStatsDto{
				TotalPercentage = totalPercentage,
				AdmiredPercentage = admiredPercentage,
				DesiredPercentage = desiredPercentage,
			},
			Responses = questionResponses
		};
	}

    public async Task<IEnumerable<QuestionDetailsDto>> GetQuestionsBySurveyBlockIdAsync(Guid surveyBlockId)
    {
        var questions = await _context.Questions
            .Where(q => q.BlockElementId == surveyBlockId)
            .ToListAsync();

        if(questions.Count == 0) throw new NotFoundException("No questions found");

        var dtoS = questions.Select(QuestionDetailsDto.CreateFromEntity).ToList();

        return dtoS;
    }

    public async Task<List<QuestionDetailsDto>> GetQuestionsBySurveyIdAsync(string surveyId)
    {
        var questions = await _context.Questions
            .Where(q => q.SurveyId == surveyId)
            .ToListAsync();

        if(questions.Count == 0) throw new NotFoundException("No questions found");

        var dtoS = questions.Select(QuestionDetailsDto.CreateFromEntity).ToList();

        return dtoS;
    }

    public async Task<QuestionDetailsDto> UpdateQuestionAsync(Guid questionId, NewQuestionDto updateQuestionDto)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);

        if (question == null) throw new NotFoundException("Question not found");

        var change = false;

        if (question.DataExportTag != updateQuestionDto.DataExportTag)
        {
            question.DataExportTag = updateQuestionDto.DataExportTag;
            change = true;
        }

        if (question.QuestionText != updateQuestionDto.QuestionText)
        {
            question.QuestionText = updateQuestionDto.QuestionText;
            change = true;
        }

        if (question.QuestionDescription != updateQuestionDto.QuestionDescription)
        {
            question.QuestionDescription = updateQuestionDto.QuestionDescription;
            change = true;
        }

        if (change)
        {
            question.UpdatedAt = DateTimeOffset.Now;
            await _context.SaveChangesAsync();
        }

        var dto = QuestionDetailsDto.CreateFromEntity(question);

        return dto;
    }

    public async Task DeleteQuestionAsync(Guid questionId)
    {
        var questionToBeDeleted = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);

        if (questionToBeDeleted == null) throw new NotFoundException("Question not found");

        questionToBeDeleted.DeletedAt = DateTimeOffset.Now;

        await _context.SaveChangesAsync();
    }
}
