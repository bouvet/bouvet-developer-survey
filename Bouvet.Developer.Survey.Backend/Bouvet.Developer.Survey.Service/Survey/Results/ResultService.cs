using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Results;

public class ResultService : IResultService
{
    private readonly DeveloperSurveyContext _context;
    private readonly IQuestionService _questionService;
    private readonly IResponseService _responseService;
    
    public ResultService(DeveloperSurveyContext context, IQuestionService questionService, IResponseService responseService)
    {
        _context = context;
        _questionService = questionService;
        _responseService = responseService;
    }

    public async Task CheckForDifferences(List<FieldDto> fieldDto, List<QuestionDetailsDto> questions, 
        Domain.Entities.Survey.Survey survey)
    {
        var groupedFields = fieldDto.GroupBy(f => f.FieldName).ToList();
        var answerOptions = await _context.AnswerOptions.Where(a => a.SurveyId == survey.Id).ToListAsync();
        foreach (var group in groupedFields)
        {
            var summaryResponse = await SummarizeFields(
                group.Select(g => g).ToList(), questions, answerOptions,survey);

            try
            {
                await _responseService.CheckForDifferences(summaryResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }
    }

    private async Task<List<NewResponseDto>> SummarizeFields(List<FieldDto> fields, 
        IEnumerable<QuestionDetailsDto> questions,List<AnswerOption> answerOptionsList, Domain.Entities.Survey.Survey survey)
    {
        var responseDtoS = new List<NewResponseDto>();
        string? questionChoiceNumber;
        string? questionExportTag;

        // Select the first field to extract the field name
        var field = fields.First();
        
        // Extract questionExportTag from fieldName
        if (field.FieldName != null && field.FieldName.Contains('_'))
        {
            questionExportTag = field.FieldName.Split('_').First();
            questionChoiceNumber = field.FieldName.Split('_').Last();
        }
        else
        {
            questionExportTag = field.FieldName;
            questionChoiceNumber = field.Value;
        }
        
        var question = questions.FirstOrDefault(q => q.DateExportTag == questionExportTag);

        if (question == null)
        {
            Console.WriteLine("Question not found for export tag: " + questionExportTag);
            return responseDtoS;
        }
        
        // Load all choices for the question at once
        var choicesDictionary = await _context.Choices
            .Where(c => c.QuestionId == question.Id)
            .ToDictionaryAsync(c => c.IndexId);
        
        foreach (var fieldInstance in fields)
        {
            if(fieldInstance.Value == null || questionChoiceNumber == null) continue;
            
            // Split the field values into individual parts (e.g., "1,2")
            var fieldValues = fieldInstance.Value.Split(",").Select(v => v.Trim());

            foreach (var fieldValue in fieldValues)
            {
                await CheckField(fieldValue, question, survey, responseDtoS,answerOptionsList, fieldInstance, 
                    questionChoiceNumber, choicesDictionary);
            }
        }
        
        return responseDtoS;
    }

    private async Task CheckField(string fieldValue, QuestionDetailsDto questionDetails, 
        Domain.Entities.Survey.Survey survey, List<NewResponseDto> responseDtoS,List<AnswerOption> answerOptionsList,
        FieldDto field, string questionChoiceNumber, Dictionary<string, Choice> choicesDictionary)
    {
        if (!choicesDictionary.TryGetValue(questionChoiceNumber, out var choice))
        {
            Console.WriteLine($"Choice not found for question: {questionDetails.Id} with name: {questionDetails.DateExportTag} and index: {questionChoiceNumber}");
            return;
        }

        var answerId = Guid.Empty;

        if (questionDetails.IsMultipleChoice)
        {
            var answerOption = answerOptionsList.FirstOrDefault(a => a.IndexId == fieldValue);

            if (answerOption == null)
            {
                Console.WriteLine($"Answer option not found for survey: {survey.Id} and index: {fieldValue}");
                return;
            }
                    
            answerId = answerOption.Id;
        }
        
        if (field.FieldName == null) return;
        
        if (questionDetails.IsMultipleChoice)
        {
            var existingResponse = responseDtoS.FirstOrDefault(r => r.FieldName == field.FieldName && r.FieldValue == fieldValue);
            AddOrUpdateResponse(responseDtoS, existingResponse, choice.Id, field.FieldName, fieldValue, answerId);
        }
        else
        {
            if (questionDetails.Choices == null) return;

            var choiceOnIndex = questionDetails.Choices.FirstOrDefault(c => c.IndexId == fieldValue);

            if (choiceOnIndex == null) return;

            var existingResponse = responseDtoS.FirstOrDefault(r => r.FieldName == field.FieldName && r.FieldValue == fieldValue);

            AddOrUpdateResponse(responseDtoS, existingResponse, choiceOnIndex.Id, field.FieldName, fieldValue,
                answerId);
        }
    }
    
    private void AddOrUpdateResponse(List<NewResponseDto> responseDtos, NewResponseDto? existingResponse,
        Guid choiceId, string fieldName, string fieldValue, Guid answerId)
    {
        if (existingResponse != null)
        {
            existingResponse.Value += 1; // Accumulate value
        }
        else
        {
            responseDtos.Add(new NewResponseDto
            {
                ChoiceId = choiceId,
                FieldName = fieldName,
                FieldValue = fieldValue,
                AnswerOptionId = answerId == Guid.Empty ? null : answerId,
                Value = 1
            });
        }
    }
    
    public async Task<IEnumerable<ExportTagDto>> GetQuestions(string surveyId)
    {
        // Fetch questions for the given survey ID
        var questions = await _questionService.GetQuestionsBySurveyIdAsync(surveyId);
    
        // Initialize a list to hold the export tags
        var exportTags = new List<ExportTagDto>();

        foreach (var question in questions)
        {
            // If the question has choices, add each choice with an index to the export tags
            if (question.Choices != null && question.Choices.Count > 0)
            {
                exportTags.Add(new ExportTagDto { DateExportTag = question.DateExportTag });
                for (var i = 0; i < question.Choices.Count; i++)
                {
                    // Append the choice index to the DateExportTag
                    var choiceTag = $"{question.DateExportTag}_{i + 1}"; // Index starts from 1 for user-friendliness
                    exportTags.Add(new ExportTagDto { DateExportTag = choiceTag });
                }
            }
            else
            {
                // If no choices, add the original DateExportTag
                exportTags.Add(new ExportTagDto { DateExportTag = question.DateExportTag });
            }
        }
    
        return exportTags; // Return the list of export tags
    }
}