using System.Globalization;
using System.Text.Json;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Results;




public class CsvToJsonService : ICsvToJsonService
{
    private readonly IQuestionService _questionService;
    private readonly IResponseService _responseService;
    private readonly DeveloperSurveyContext _context;
    
    public CsvToJsonService(IQuestionService questionService, IResponseService responseService, DeveloperSurveyContext context)
    {
        _questionService = questionService;
        _responseService = responseService;
        _context = context;
    }
    
    public async Task GetQuestionsFromStream(Stream csvStream, string surveyId)
    {
        // Retrieve questions for the given survey ID
        var questions = await GetQuestions(surveyId);
    
        // Convert the CSV stream to JSON format
        var csvRecords = await ConvertCsvToJson(csvStream);
    
        // Deserialize the CSV records to a list of dictionaries
        var records = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(csvRecords);
    
        // Create a HashSet of DateExportTags from questions for quick lookup
        var exportTagSet = new HashSet<string>(questions.Select(q => q.DateExportTag));

        // Filter the records to include only those fields present in the exportTagSet and contain numeric values
        var filteredFields = records
            .SelectMany(record => 
                exportTagSet
                    .Where(tag => record.ContainsKey(tag) && IsNumeric(record[tag]?.ToString())) // Check if the value is a valid number
                    .Select(tag => new FieldDto // Create a new DTO with the field name and its value
                    {
                        FieldName = tag,
                        Value = record[tag].ToString()
                    })
            )
            .Distinct()
            .ToList();
        
        await MapFieldsToResponse(filteredFields, surveyId);
    }

    private async Task MapFieldsToResponse(List<FieldDto> fieldDto, string surveyId)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == surveyId);

        if (survey == null) throw new NotFoundException("Survey not found");

        var questions = await _questionService.GetQuestionsBySurveyIdAsync(surveyId);
        
        // Group fields by FieldName and add them to a list
        var groupedFields = fieldDto.GroupBy(f => f.FieldName).ToList();
            
        foreach (var group in groupedFields)
        {
            var summaryResponse = await SummarizeFields(group.Select(g => g).ToList(), questions, survey);
            
            await _responseService.CreateResponse(summaryResponse);
        }
    }

    private async Task<List<NewResponseDto>> SummarizeFields(List<FieldDto> fields, IEnumerable<QuestionDto> questions, Domain.Entities.Survey.Survey survey)
    {
        var responseDtos = new List<NewResponseDto>();
        string questionChoiceNumber = null;
        string questionExportTag = null;

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
    
    // Helper method to check if a string is numeric
    private bool IsNumeric(string value)
    {
        return !string.IsNullOrEmpty(value) && decimal.TryParse(value, out _); // Return true if the string can be parsed as a number
    }
    
    public async Task<string> ConvertCsvToJson(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        await csv.ReadAsync();
        csv.ReadHeader();
        
        // Read CSV records into a list of dictionaries
        var records = new List<Dictionary<string, object>>();
        while (await csv.ReadAsync()) // Limit the number of records to 50
        {
            var record = new Dictionary<string, object>();
            foreach (var header in csv.HeaderRecord)
            {
                record[header] = csv.GetField(header); // Get the field value by header
            }
            records.Add(record);
        }
        
        // Serialize the records to JSON
        var json = JsonSerializer.Serialize(records, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        return json;
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