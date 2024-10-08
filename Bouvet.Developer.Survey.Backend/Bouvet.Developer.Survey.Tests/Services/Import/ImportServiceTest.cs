using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Import;

public class ImportServiceTest
{
    private readonly IImportSurveyService _importSurvey;
    private readonly ISurveyService _surveyService;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly IBlockElementService _blockElementService;
    private readonly DeveloperSurveyContext _context;
    
    public ImportServiceTest()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "ImportDatabase")
            .Options;

        _context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _surveyService = new SurveyService(_context);
        _surveyBlockService = new SurveyBlockService(_context);
        _blockElementService = new BlockElementService(_context);
        _importSurvey = new ImportSurveyService(_surveyService, _surveyBlockService, _blockElementService,_context);
    }

    [Fact]
    public async Task TestFindSurveyBlocks()
    {
        // Arrange
        var surveyBlocksDto = await TestData();
        
        // Act
        var result = await _importSurvey.FindSurveyBlocks(surveyBlocksDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(surveyBlocksDto.SurveyEntry.SurveyId, result.SurveyEntry.SurveyId);
        Assert.Equal(surveyBlocksDto.SurveyEntry.SurveyName, result.SurveyEntry.SurveyName);
        Assert.Equal(surveyBlocksDto.SurveyEntry.SurveyLanguage, result.SurveyEntry.SurveyLanguage);
        
        // Act
        var surveys = await _surveyService.GetSurveysAsync();
        
        // Assert
        Assert.NotNull(surveys);
    }


    private async Task<SurveyBlocksDto> TestData()
    {
        var newSurveyBlocksDto = new SurveyBlocksDto
        {
            SurveyEntry = new SurveyEntryDto
            {
                SurveyId = "123",
                SurveyName = "Test survey",
                SurveyLanguage = "English"
            },
            SurveyElements = [await TestSurveyBlocks()]
        };
            
        return newSurveyBlocksDto;
    }
    
    private async Task<SurveyElementBlockDto> TestSurveyBlocks()
    {
       var test = new SurveyElementBlockDto
       {
              Payload = new Dictionary<string, DictionaryPayload>
                {
                    {"QID1", new DictionaryPayload
                    {
                       Type= "TEXT",
                       Description = "What is your name?",
                       Id = "QID1",
                       BlockElements = new List<BlockElementDto>
                       { 
                           new BlockElementDto
                            {
                                 Type = "TEXT",
                                 QuestionId = "QID1"
                            },
                            new BlockElementDto
                            {
                                Type = "TEXT",
                                QuestionId = "QID2"
                            }
                       }
                    }},
                    {"QID2", new DictionaryPayload
                    {
                        Type= "TEXT 2",
                        Description = "How old are you?",
                        Id = "QID2",
                        BlockElements = new List<BlockElementDto>
                        { 
                            new BlockElementDto
                            {
                                Type = "TEXT",
                                QuestionId = "QID2"
                            },
                            new BlockElementDto
                            {
                                Type = "TEXT",
                                QuestionId = "QID2"
                            }
                        }
                    }}
                }
         };
       
       return await Task.FromResult(test);
    }
}