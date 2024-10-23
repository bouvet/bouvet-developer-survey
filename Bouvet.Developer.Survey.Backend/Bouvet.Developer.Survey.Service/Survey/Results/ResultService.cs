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
    
    public async Task<List<NewResponseDto>> SummarizeFields(List<FieldDto> fields, IEnumerable<QuestionDto> questions, Domain.Entities.Survey.Survey survey)
    {
        var responseDtos = new List<NewResponseDto>();
        string? questionChoiceNumber;
        string? questionExportTag;

        // Assuming all fields in the group share the same FieldName
        var field = fields.First();
        
        // Extract questionExportTag from fieldName
        if (field.FieldName.Contains('_'))
        {
            questionExportTag = field.FieldName.Split('_').First();
            questionChoiceNumber = field.FieldName.Split("_").Last();
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
            // Split the field values into individual parts (e.g., "1,2")
            var fieldValues = fieldInstance.Value.Split(",").Select(v => v.Trim());

            foreach (var fieldValue in fieldValues)
            {
                var choice = await _context.Choices.FirstOrDefaultAsync(c =>
                    c.QuestionId == question.Id && c.IndexId == questionChoiceNumber);

                if (choice == null)
                {
                    Console.WriteLine($"Choice not found for question: {question.Id} and index: {questionChoiceNumber}");
                    continue;
                }

                var answerId = Guid.Empty;

                if (question.IsMultipleChoice)
                {

                    var answerOption = await _context.AnswerOptions.FirstOrDefaultAsync(a =>
                        a.SurveyId == survey.Id && a.IndexId == fieldValue);

                    if (answerOption == null)
                    {
                        Console.WriteLine($"Answer option not found for survey: {survey.Id} and index: {fieldValue}");
                        continue;
                    }
                    
                    answerId = answerOption.Id;
                }

                // Create a summarized response for each fieldValue
                var responseDto = new NewResponseDto
                {
                    ChoiceId = choice.Id,
                    FieldName = field.FieldName,
                    FieldValue = fieldValue,
                    AnswerOptionId = answerId == Guid.Empty ? null : answerId,
                    Value = 1 // Assuming each value counts as one instance
                };

                // If the fieldValue already exists in responseDtos, accumulate the value
                var existingResponse = responseDtos.FirstOrDefault(r => r.FieldValue == fieldValue);
                if (existingResponse != null)
                {
                    existingResponse.Value += 1; // Accumulate value
                }
                else
                {
                    responseDtos.Add(responseDto); // Add new response
                }
            }
        }

        return responseDtos;
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
            if (question.Choices.Count > 0)
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