using System.Text.Json;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Import;

public class ImportSurveyService : IImportSurveyService
{
    private readonly ISurveyService _surveyService;
    private readonly IImportSyncService _importSyncService;
    private readonly DeveloperSurveyContext _context;
    
    public ImportSurveyService(ISurveyService surveyService, IImportSyncService importSyncService,
         DeveloperSurveyContext context)
    {
        _surveyService = surveyService;
        _context = context; 
        _importSyncService = importSyncService;
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
    
    public async Task<SurveyBlocksDto> ShowJsonSurvey(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var jsonString = await reader.ReadToEndAsync();

        var surveyDto = JsonSerializer.Deserialize<SurveyBlocksDto>(jsonString);

        if (surveyDto == null) throw new BadRequestException("Invalid JSON");
        
        
        return surveyDto;
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
    
    private async Task MapJsonSurveyBlocks(SurveyBlocksDto surveyBlockDto)
    {
        var checkSurveyExists = await 
            _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == surveyBlockDto.SurveyEntry.SurveyId);

        if (checkSurveyExists != null)
        {
            await _importSyncService.CheckForDifference(surveyBlockDto, checkSurveyExists);
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
                    await _importSyncService.AddSurveyElements(surveyElement, survey.SurveyId);
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
        
        await _importSyncService.AddQuestions(questionsDto, survey.SurveyId);

        
    }
}