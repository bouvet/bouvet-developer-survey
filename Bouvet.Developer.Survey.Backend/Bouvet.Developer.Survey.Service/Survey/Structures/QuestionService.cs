using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
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

            //Add new questions to survey
            if (question == null)
            {
                var newQuestion = new NewQuestionDto
                {
                    BlockElementId = surveyElement.PrimaryAttribute,
                    QuestionId = surveyElement.PrimaryAttribute,
                    SurveyId = survey.SurveyId,
                    DateExportTag = surveyElement.Payload != null ? surveyElement.Payload.DataExportTag : string.Empty,
                    QuestionText = surveyElement.Payload != null ? surveyElement.Payload.QuestionText : string.Empty,
                    QuestionDescription = surveyElement.Payload != null
                        ? surveyElement.Payload.QuestionDescription
                        : string.Empty,
                    Choices = surveyElement.Payload?.Choices.Values.Select(c => new NewChoiceDto
                    {
                        Text = c.Display
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
                    DateExportTag = surveyElement.Payload != null ? surveyElement.Payload.DataExportTag : string.Empty,
                    QuestionText = surveyElement.Payload != null ? surveyElement.Payload.QuestionText : string.Empty,
                    QuestionDescription = surveyElement.Payload != null
                        ? surveyElement.Payload.QuestionDescription
                        : string.Empty,
                    Choices = surveyElement.Payload?.Choices.Values.Select(c => new NewChoiceDto
                    {
                        Text = c.Display
                    }).ToList()
                };
                
                await UpdateQuestionAsync(question.Id, updateQuestion);
            }
            
            //Delete questions that are not in the survey
            foreach (var questionToBeDeleted in allQuestionToSurvey)
            {
                if (surveyQuestionsDto.SurveyElements.All(q => q.PrimaryAttribute != questionToBeDeleted.QuestionId))
                {
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

    public async Task<QuestionDto> CreateQuestionAsync(NewQuestionDto newQuestionDto)
    {
        var blockElement = await _context.BlockElements.FirstOrDefaultAsync(be => be.QuestionId == newQuestionDto.BlockElementId);
        
        if (blockElement == null) throw new NotFoundException("Block element not found");
        
        var question = new Question
        {
            Id = Guid.NewGuid(),
            BlockElementId = blockElement.Id,
            SurveyId = newQuestionDto.SurveyId,
            QuestionId = newQuestionDto.QuestionId,
            DateExportTag = newQuestionDto.DateExportTag,
            QuestionText = newQuestionDto.QuestionText,
            QuestionDescription = newQuestionDto.QuestionDescription,
            CreatedAt = DateTimeOffset.Now
        };
        
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        
        if(newQuestionDto.Choices != null)
            await _choiceService.CreateChoice(newQuestionDto.Choices, question.Id);
        
        var dto = QuestionDto.CreateFromEntity(question);
        
        return dto;
    }

    public async Task<QuestionDto> GetQuestionByIdAsync(Guid questionId)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        
        if (question == null) throw new NotFoundException("Question not found");
        
        var dto = QuestionDto.CreateFromEntity(question);
        
        return dto;
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestionsBySurveyBlockIdAsync(Guid surveyBlockId)
    {
        var questions = await _context.Questions
            .Where(q => q.BlockElementId == surveyBlockId)
            .ToListAsync();
        
        if(questions.Count == 0) throw new NotFoundException("No questions found");
        
        var dtoS = questions.Select(QuestionDto.CreateFromEntity).ToList();
        
        return dtoS;
    }

    public async Task<QuestionDto> UpdateQuestionAsync(Guid questionId, NewQuestionDto updateQuestionDto)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        
        if (question == null) throw new NotFoundException("Question not found");

        var change = false;
        
        if (question.DateExportTag != updateQuestionDto.DateExportTag)
        {
            question.DateExportTag = updateQuestionDto.DateExportTag;
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
        
        var dto = QuestionDto.CreateFromEntity(question);
        
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