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
        
        foreach (var group in groupedFields)
        {
            var summaryResponse = await SummarizeFields(
                group.Select(g => g).ToList(), questions,survey);
            
            try
            {
                await _responseService.CreateResponse(summaryResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }
    }

    private async Task<List<NewResponseDto>> SummarizeFields(List<FieldDto> fields, 
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
    
        // Load all choices for the question at once
        var choicesDictionary = await _context.Choices
            .Where(c => c.QuestionId == question.Id)
            .ToDictionaryAsync(c => c.IndexId);
    
        foreach (var fieldInstance in fields)
        {
            if (fieldInstance.Value == null || questionChoiceNumber == null) continue;
            
            await CheckField(fieldInstance, question, responseDtoS, fieldInstance, 
                    questionChoiceNumber, choicesDictionary);
        }
    
        return responseDtoS;
    }

    private async Task CheckField(FieldDto fieldInstance, QuestionDetailsDto questionDetails, List<NewResponseDto> responseDtoS, FieldDto field,
    string questionChoiceNumber, Dictionary<string, Choice> choicesDictionary)
    {
        var choiceNumbers = questionChoiceNumber.Split(',').Select(n => n.Trim()).ToList();
        
        // Try to find the choice for the current choiceNumber
        if (!choicesDictionary.TryGetValue(choiceNumbers.First(), out var choice))
        {
            Console.WriteLine($"Choice not found for question: {questionDetails.Id} with name: {questionDetails.DateExportTag} and index: {choiceNumbers.First()}");
            return;  // If choice not found, skip processing for this choiceNumber
        }

        var answerId = Guid.Empty;

        // If it's a multiple-choice question, find the appropriate answer option
        if (questionDetails.IsMultipleChoice)
        {
            // Handle the case where both 1 and 2 are selected
            if (fieldInstance.Value.Contains("1") && fieldInstance.Value.Contains("2"))
            {
                // Set both HasWorkedWith and WantsToWorkWith to true if both 1 and 2 are selected
                SetResponseFlag(responseDtoS, field, fieldInstance.Value, choice.Id, answerId, true, true);
            }
            else
            {
                // Otherwise, handle the individual flags
                if (fieldInstance.Value == "1")
                {
                    // Set HasWorkedWith to true if the value is 1
                    SetResponseFlag(responseDtoS, field, fieldInstance.Value, choice.Id, answerId, true, false);
                }
                else if (fieldInstance.Value == "2")
                {
                    // Set WantsToWorkWith to true if the value is 2
                    SetResponseFlag(responseDtoS, field, fieldInstance.Value, choice.Id, answerId, false, true);
                }
            }
        }
        else
        {
            if (questionDetails.Choices == null) return;

            var fieldValues = fieldInstance.Value.Split(",").Select(v => v.Trim());
            foreach (var fieldValue in fieldValues)
            {
                var choiceOnIndex = questionDetails.Choices.FirstOrDefault(c => c.IndexId == fieldValue);

                if (choiceOnIndex == null)
                {
                    Console.WriteLine($"Choice not found for question: {questionDetails.Id} with name: {questionDetails.DateExportTag} and index: {fieldValue}");
                    return;
                }

                var existingResponse =
                    responseDtoS.FirstOrDefault(r => r.FieldName == field.FieldName && r.FieldValue == fieldValue);

                // For non-multiple-choice questions, set the value to 1 by default (only for the first value)
                CreateNonMultipleValueResponse(responseDtoS, existingResponse, field.FieldName, fieldValue, choiceOnIndex.Id,
                    Guid.Empty);
            }
        }
    }
    
    private void CreateNonMultipleValueResponse(List<NewResponseDto> responseDtos, NewResponseDto? existingResponse, string fieldName, 
        string fieldValue, Guid choiceId, Guid answerId)
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
                Value = 1, // Always 1 for non-multiple-choice or when working with values 1 or 2
                HasWorkedWith = false,
                WantsToWorkWith = false
            });
        }
    }
    
    private void SetResponseFlag(List<NewResponseDto> responseDtos, FieldDto field, string fieldValue, 
        Guid choiceId, Guid answerId, bool hasWorkedWith, bool wantsToWorkWith)
    {
        responseDtos.Add(new NewResponseDto
        {
            ChoiceId = choiceId,
            FieldName = field.FieldName,
            FieldValue = fieldValue,
            AnswerOptionId = answerId == Guid.Empty ? null : answerId,
            Value = 1, // Always 1 for non-multiple-choice or when working with values 1 or 2
            HasWorkedWith = hasWorkedWith,
            WantsToWorkWith = wantsToWorkWith
        });
        
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