using System.Text.Json;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Import;

public class ImportSurveyService : IImportSurveyService
{
    private readonly ISurveyService _surveyService;
    private readonly IQuestionService _questionService;
    private readonly DeveloperSurveyContext _context;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly IBlockElementService _blockElementService;
    private readonly IResponseService _responseService;
    private readonly IResultService _resultService;
    private readonly ICsvToJsonService _csvToJsonService;
    
    public ImportSurveyService(ISurveyService surveyService, DeveloperSurveyContext context, IQuestionService questionService, ISurveyBlockService surveyBlockService,
            IBlockElementService blockElementService, IResponseService responseService, IResultService resultService, ICsvToJsonService csvToJsonService)
    {
        _surveyService = surveyService;
        _context = context; 
        _questionService = questionService;
        _surveyBlockService = surveyBlockService;
        _blockElementService = blockElementService;
        _responseService = responseService;
        _resultService = resultService;
        _csvToJsonService = csvToJsonService;
    }
    
    public async Task<SurveyBlocksDto> UploadSurvey(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var jsonString = await reader.ReadToEndAsync();

        var surveyDto = JsonSerializer.Deserialize<SurveyBlocksDto>(jsonString);

        if (surveyDto == null) throw new BadRequestException("Invalid JSON");
        
        var questionsDto = JsonSerializer.Deserialize<SurveyQuestionsDto>(jsonString);
        
        var mapSurvey = await FindSurveyBlocks(surveyDto);
        
        if(questionsDto != null)
            await FindSurveyQuestions(questionsDto);
        
        return mapSurvey;
    }

    public async Task<SurveyBlocksDto> FindSurveyBlocks(SurveyBlocksDto surveyDto)
    {
        // Filter out elements where Payload is null or empty
        surveyDto.SurveyElements = (surveyDto.SurveyElements ?? throw new BadRequestException("Invalid JSON"))
            .Where(element => element.Payload != null && element.Payload.Count > 0)
            .ToArray();

        await MapJsonSurveyBlocks(surveyDto);
        
        return surveyDto;
    }

    public async Task FindSurveyQuestions(SurveyQuestionsDto surveyQuestionsDto)
    {
        surveyQuestionsDto.SurveyElements = (surveyQuestionsDto.SurveyElements ?? throw new BadRequestException("Invalid JSON"))
        .Where(element => element.Payload != null && element.Payload.Choices.Count > 0)
        .ToArray();
        
        await MapJsonQuestions(surveyQuestionsDto);
    }
    
    public async Task GetQuestionsFromStream(Stream csvStream, string surveyId)
    {
        var questions = await _resultService.GetQuestions(surveyId);
    
        // Convert the CSV stream to JSON format
        var csvRecords = await _csvToJsonService.ConvertCsvToJson(csvStream);
    
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
            var summaryResponse = await _resultService.SummarizeFields(group.Select(g => g).ToList(), questions, survey);
            
            await _responseService.CreateResponse(summaryResponse);
        }
    }
    
    private async Task MapJsonSurveyBlocks(SurveyBlocksDto surveyBlockDto)
    {
        var checkSurveyExists = await 
            _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == surveyBlockDto.SurveyEntry.SurveyId);

        if (checkSurveyExists != null)
        {
            Console.WriteLine("Survey already exists, checking for differences");   
            await CheckForDifferenceSurvey(surveyBlockDto, checkSurveyExists);
        }
        else
        {

            var survey = await _surveyService.CreateSurveyAsync(new NewSurveyDto
            {
                Name = surveyBlockDto.SurveyEntry.SurveyName,
                SurveyId = surveyBlockDto.SurveyEntry.SurveyId,
                Language = surveyBlockDto.SurveyEntry.SurveyLanguage
            });

            if (surveyBlockDto.SurveyElements != null)
            {
                foreach (var surveyElement in surveyBlockDto.SurveyElements)
                {
                    await AddSurveyElements(surveyElement, survey.SurveyId);
                }
            }
        }
    }
    
    
    private async Task MapJsonQuestions(SurveyQuestionsDto questionsDto)
    {
        var survey = await _context.Surveys
            .FirstOrDefaultAsync(s => questionsDto.SurveyElements != null 
                                      && s.SurveyId == questionsDto.SurveyElements.First().SurveyId);
        
        if(survey == null) throw new NotFoundException("Survey not found");

        if(questionsDto.SurveyElements == null) throw new BadRequestException("Invalid JSON");
        
        await _questionService.CheckForDifference(questionsDto, survey);
    }

    private async Task AddSurveyElements(SurveyElementBlockDto surveyBlockDto, string surveyId)
    {
        var blockElements = new List<NewBlockElementDto>();
        
        foreach (var element in surveyBlockDto.Payload.Values)
        {
            var surveyBlock = await _surveyBlockService.CreateSurveyBlock(new NewSurveyBlockDto
            {
                SurveyId = surveyId,
                Type = element.Type,
                Description = element.Description,
                SurveyBlockId = element.Id
            });
                    
            blockElements.AddRange(element.BlockElements.Select(blockElement => new NewBlockElementDto
            {
                BlockId = surveyBlock.Id,
                Type = blockElement.Type,
                QuestionId = blockElement.QuestionId
            }));
        }

        await _blockElementService.CreateBlockElements(blockElements);
    }


    private async Task CheckForDifferenceSurvey(SurveyBlocksDto surveyBlockDto, Domain.Entities.Survey.Survey survey)
    {
        //Find the survey, check if there are any differences in the name, Language
        var surveyEntry = surveyBlockDto.SurveyEntry;

        if (survey.Name != surveyEntry.SurveyName || survey.SurveyLanguage != surveyEntry.SurveyLanguage)
        {
            Console.WriteLine("Survey has been updated, updating survey");
            await _surveyService.UpdateSurveyAsync(survey.Id, new NewSurveyDto
            {
                Name = surveyEntry.SurveyName,
                Language = surveyEntry.SurveyLanguage
            });
        }
        
        if(surveyBlockDto.SurveyElements == null) return;
        
        foreach (var surveyElement in surveyBlockDto.SurveyElements)
        {
            var surveyElementsList = survey?.SurveyBlocks?.ToList();
            
            if(surveyElementsList == null) continue;
            
            //Check for differences in the surveyBlock
            if (survey != null)
            {
                await _surveyBlockService.CheckSurveyBlockElements(survey.Id, surveyElement);
                // await _blockElementService.CheckBlockElements(survey.Id, surveyElement);
            }
        }

        if (survey != null) survey.LastSyncedAt = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }
    
    // Helper method to check if a string is numeric
    private bool IsNumeric(string value)
    {
        return !string.IsNullOrEmpty(value) && decimal.TryParse(value, out _); // Return true if the string can be parsed as a number
    }
}