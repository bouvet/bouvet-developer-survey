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
    
    public ResultService(DeveloperSurveyContext context, IQuestionService questionService)
    {
        _context = context;
        _questionService = questionService;
    }
    
    public async Task<List<NewResponseDto>> SummarizeFields(List<FieldDto> fields, 
        IEnumerable<QuestionDetailsDto> questions, Domain.Entities.Survey.Survey survey)
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

        foreach (var fieldInstance in fields)
        {
            if(fieldInstance.Value == null || questionChoiceNumber == null) continue;
            
            // Split the field values into individual parts (e.g., "1,2")
            var fieldValues = fieldInstance.Value.Split(",").Select(v => v.Trim());

            foreach (var fieldValue in fieldValues)
            {
                await CheckField(fieldValue, question, survey, responseDtoS, fieldInstance, questionChoiceNumber);
            }
        }
        
        return responseDtoS;
    }

    private async Task CheckField(string fieldValue, QuestionDetailsDto questionDetails, 
        Domain.Entities.Survey.Survey survey, List<NewResponseDto> responseDtoS, FieldDto field, string questionChoiceNumber)
    {
        var choiceNumbers = questionChoiceNumber.Split(',').Select(num => num.Trim());
        var choice = await _context.Choices.FirstOrDefaultAsync(c =>
            c.QuestionId == questionDetails.Id && choiceNumbers.Contains(c.IndexId));

        if (choice == null)
        {
            Console.WriteLine($"Choice not found for question: {questionDetails.Id} with name: {questionDetails.DateExportTag} and index: {questionChoiceNumber}");
            return;
        }

        var answerId = Guid.Empty;

        if (questionDetails.IsMultipleChoice)
        {
            var answerOption = await _context.AnswerOptions.FirstOrDefaultAsync(a =>
                a.SurveyId == survey.Id && a.IndexId == fieldValue);

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
            var existingResponse = responseDtoS.FirstOrDefault(r => r.FieldValue == fieldValue);
            AddOrUpdateResponse(responseDtoS,existingResponse, choice.Id, questionDetails.DateExportTag, fieldValue,field.ResponseId, answerId);
        }
        else
        {
            if(questionDetails.Choices == null) return;
        
            var choiceOnIndex = questionDetails.Choices.FirstOrDefault(c => c.IndexId == fieldValue);
        
            if (choiceOnIndex == null) return;
        
            var existingResponse = responseDtoS.FirstOrDefault(r => r.FieldValue == fieldValue);
            
            AddOrUpdateResponse(responseDtoS,existingResponse, choiceOnIndex.Id, questionDetails.DateExportTag, fieldValue,field.ResponseId, answerId);
        }
    }
    
    private void AddOrUpdateResponse(List<NewResponseDto> responseDtos, NewResponseDto? existingResponse,
        Guid choiceId, string fieldName, string fieldValue, string responseId, Guid answerId)
    {
        if (existingResponse != null)
        {
            Console.WriteLine("Existing response found");
            existingResponse.Value += 1; // Accumulate value
        }
        else
        {
            responseDtos.Add(new NewResponseDto
            {
                ChoiceId = choiceId,
                FieldName = fieldName,
                FieldValue = fieldValue,
                ResponseId = responseId,
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
                for (int i = 0; i < question.Choices.Count; i++)
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