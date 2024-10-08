using System.Text.Json;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Import;

public class ImportSurveyService : IImportSurveyService
{
    private readonly ISurveyService _surveyService;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly IBlockElementService _blockElementService;
    private readonly DeveloperSurveyContext _context;
    
    public ImportSurveyService(ISurveyService surveyService, ISurveyBlockService surveyBlockService, 
        IBlockElementService blockElementService, DeveloperSurveyContext context)
    {
        _surveyService = surveyService;
        _surveyBlockService = surveyBlockService;
        _blockElementService = blockElementService;
        _context = context; 
    }
    
    public async Task<SurveyBlocksDto> UploadSurvey(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var jsonString = await reader.ReadToEndAsync();

        var surveyDto = JsonSerializer.Deserialize<SurveyBlocksDto>(jsonString);

        if (surveyDto == null) throw new BadRequestException("Invalid JSON");

        
        var mapSurvey = await FindSurveyBlocks(surveyDto);
        
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

    public async Task<SurveyQuestionsDto> FindSurveyQuestions(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var jsonString = await reader.ReadToEndAsync();

        var surveyDto = JsonSerializer.Deserialize<SurveyQuestionsDto>(jsonString);

        if (surveyDto == null) throw new BadRequestException("Invalid JSON");

        // Filter out elements where Payload is null or empty
        surveyDto.SurveyElements = (surveyDto.SurveyElements ?? throw new BadRequestException("Invalid JSON"))
            .Where(element => element.Payload != null && element.Payload.Choices.Count > 0)
            .ToArray();

        return surveyDto;
    }
    
    private async Task MapJsonSurveyBlocks(SurveyBlocksDto surveyBlockDto)
    {
        var checkSurveyExists = await 
            _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == surveyBlockDto.SurveyEntry.SurveyId);
        
        if(checkSurveyExists != null) throw new NotFoundException("Survey already exists");
        
        var survey = await _surveyService.CreateSurveyAsync(new NewSurveyDto
        {
            Name = surveyBlockDto.SurveyEntry.SurveyName,
            SurveyId = surveyBlockDto.SurveyEntry.SurveyId,
            Language = surveyBlockDto.SurveyEntry.SurveyLanguage
        });
        
        // Bulk insert survey blocks and block elements
        var blockElements = new List<NewBlockElementDto>();

        if (surveyBlockDto.SurveyElements != null)
            foreach (var surveyElement in surveyBlockDto.SurveyElements)
            {
                foreach (var element in surveyElement.Payload.Values)
                {
                    var surveyBlock = await _surveyBlockService.CreateSurveyBlock(new NewSurveyBlockDto
                    {
                        SurveyId = survey.SurveyId,
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
            }

        await _blockElementService.CreateBlockElement(blockElements); // Bulk insert
    }
}