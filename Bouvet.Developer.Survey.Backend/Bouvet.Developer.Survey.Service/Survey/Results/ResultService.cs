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
    
    public async Task<List<NewResponseDto>> SummarizeFields(List<FieldDto> fields, IEnumerable<QuestionDetailsDto> questions, Domain.Entities.Survey.Survey survey)
    {
        var responseDtos = new List<NewResponseDto>();
        string? questionChoiceNumber;
        string? questionExportTag;

        // Select the first field to extract the qfield name
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
            return responseDtos;
        }

        foreach (var fieldInstance in fields)
        {
            if(fieldInstance.Value == null || questionChoiceNumber == null) continue;
            
            // Split the field values into individual parts (e.g., "1,2")
            var fieldValues = fieldInstance.Value.Split(",").Select(v => v.Trim());

            foreach (var fieldValue in fieldValues)
            {
                await CheckField(fieldValue, question, survey, responseDtos, fieldInstance, questionChoiceNumber);
            }
        }

        return responseDtos;
    }

    private async Task CheckField(string fieldValue, QuestionDetailsDto questionDetails, 
        Domain.Entities.Survey.Survey survey, List<NewResponseDto> responseDtos, FieldDto field, string questionChoiceNumber)
    {
        var choice = await _context.Choices.FirstOrDefaultAsync(c =>
            c.QuestionId == questionDetails.Id && c.IndexId == questionChoiceNumber);

        if (choice == null)
        {
            Console.WriteLine($"Choice not found for question: {questionDetails.Id} and index: {questionChoiceNumber}");
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

        if (field.FieldName != null)
            AddOrUpdateResponse(responseDtos, choice.Id, field.FieldName, fieldValue, answerId);
    }
    
    private void AddOrUpdateResponse(List<NewResponseDto> responseDtos, Guid choiceId, string fieldName, string fieldValue, Guid answerId)
    {
        var existingResponse = responseDtos.FirstOrDefault(r => r.FieldValue == fieldValue);
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