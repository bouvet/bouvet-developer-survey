using System.Globalization;
using System.Text.Json;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import;
using CsvHelper;
using CsvHelper.Configuration;

namespace Bouvet.Developer.Survey.Service.Survey;

public class ExportTagDto
{
    public string DateExportTag { get; set; }
}

public class FieldDto
{
    public string FieldName { get; set; }
    public string Value { get; set; }
}


public class CsvToJsonService : ICsvToJsonService
{
    private readonly IQuestionService _questionService;
    
    public CsvToJsonService(IQuestionService questionService)
    {
        _questionService = questionService;
    }
    
    public async Task<IEnumerable<FieldDto>> GetQuestionsFromStream(Stream csvStream, string surveyId)
    {
        // Retrieve questions for the given survey ID
        var questions = await GetQuestions(surveyId);
        
        // Convert the CSV stream to JSON format
        var csvRecords = await ConvertCsvToJson(csvStream);
        
        // Deserialize the CSV records to a list of dictionaries
        var records = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(csvRecords);
        
        // Create a HashSet of DateExportTags from questions for quick lookup
        var exportTagSet = new HashSet<string>(questions.Select(q => q.DateExportTag));

        // Filter the records to include only those fields present in the exportTagSet
        var filteredFields = records
            .SelectMany(record => 
                exportTagSet
                    .Where(tag => record.ContainsKey(tag)) // Check if the record contains the field name
                    .Select(tag => new FieldDto // Create a new DTO with the field name and its value
                    {
                        FieldName = tag,
                        Value = record[tag].ToString()
                    })
            )
            .Distinct()
            .ToList();

        return filteredFields;
    }
    
    public async Task<string> ConvertCsvToJson(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

      

        await csv.ReadAsync();
        csv.ReadHeader();
        
        // Read CSV records into a list of dictionaries
        var records = new List<Dictionary<string, object>>();
        
        int recordCount = 0;
        while (await csv.ReadAsync() && recordCount < 10) // Limit the number of records to 10
        {
            var record = new Dictionary<string, object>();
            foreach (var header in csv.HeaderRecord)
            {
                record[header] = csv.GetField(header); // Get the field value by header
            }
            records.Add(record);
            recordCount++; // Increment the counter
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
    
    // public async Task<IEnumerable<ExportTagDto>> GetQuestions(string surveyId)
    // {
    //     var questions = await _questionService.GetQuestionsBySurveyIdAsync(surveyId);
    //
    //     foreach (var question in questions)
    //     {
    //         if(question.Choices.Count > 0)
    //         {
    //             foreach (var choice in question.Choices)
    //             {
    //                 
    //             }
    //         }
    //     }
    //     
    //     //Selects exportTag from questions
    //     var exportTags = questions.Select(q => new ExportTagDto { DateExportTag = q.DateExportTag }).ToList();
    //     
    //     
    //     return exportTags;
    // }
}