using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Ai;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Ai;
using Bouvet.Developer.Survey.Service.Survey.Results;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Import;

public class ImportServiceTest
{
    private readonly IImportSurveyService _importSurvey;
    private readonly ISurveyService _surveyService;
    private readonly IConfiguration _configuration;

    public ImportServiceTest()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "ImportDatabase2")
            .Options;

        var context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _surveyService = new SurveyService(context);
        IChoiceService choiceService = new ChoiceService(context);
        IAnswerOptionService answerOptionService = new AnswerOptionService(context);
        IQuestionService questionService = new QuestionService(context, choiceService, answerOptionService);
        ISurveyBlockService surveyBlockService = new SurveyBlockService(context);
        IBlockElementService blockElementService = new BlockElementService(context);
        IResultService resultService = new ResultService(context, questionService, new ResponseService(context));
        ICsvToJsonService csvToJsonService = new CsvToJsonService();
        IAiService aiService = new AiService(_configuration, questionService, new AiAnalyseService(context), context);
        IUserService userService = new UserService(context);
        _importSurvey = new ImportSurveyService(_surveyService, context, questionService, surveyBlockService,
            blockElementService, resultService, csvToJsonService, userService, aiService);
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
        
        var surveyQuestionsDto = await TestSurveyQuestions();
        await _importSurvey.FindSurveyQuestions(surveyQuestionsDto);
        
        //Test for survey exist
        var changeDto = await TestDataChangeName();
        var test = await _importSurvey.FindSurveyBlocks(changeDto);
        
        Assert.NotNull(test);
        Assert.Equal(changeDto.SurveyEntry.SurveyId, test.SurveyEntry.SurveyId);
        Assert.Equal(changeDto.SurveyEntry.SurveyName, test.SurveyEntry.SurveyName);
        Assert.Equal(changeDto.SurveyEntry.SurveyLanguage, test.SurveyEntry.SurveyLanguage);
        
        //Test for survey questions change
        var surveyQuestionsDtoChange = await TestSurveyQuestionsChange();
        await _importSurvey.FindSurveyQuestions(surveyQuestionsDtoChange);
        
    }

    private async Task<SurveyBlocksDto> TestDataChangeName()
    {
        var newSurveyBlocksDto = new SurveyBlocksDto
        {
            SurveyEntry = new SurveyEntryDto
            {
                SurveyId = "123qa",
                SurveyName = "Bouvet survey",
                SurveyLanguage = "NO"
            },
            SurveyElements = [await TestSurveyBlocksChange()]
        };
            
        return newSurveyBlocksDto;
    }
    
    private async Task<SurveyBlocksDto> TestData()
    {
        var newSurveyBlocksDto = new SurveyBlocksDto
        {
            SurveyEntry = new SurveyEntryDto
            {
                SurveyId = "123qa",
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
                       BlockElements = new List<SurveyBlockElementDto>
                       { 
                           new SurveyBlockElementDto
                            {
                                 Type = "TEXT",
                                 QuestionId = "QID1"
                            },
                            new SurveyBlockElementDto
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
                        BlockElements = new List<SurveyBlockElementDto>
                        { 
                            new SurveyBlockElementDto
                            {
                                Type = "TEXT",
                                QuestionId = "QID2"
                            },
                            new SurveyBlockElementDto
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
    
    private async Task<SurveyElementBlockDto> TestSurveyBlocksChange()
    {
        var test = new SurveyElementBlockDto
        {
            SurveyId = "123qa",
            Payload = new Dictionary<string, DictionaryPayload>
            {
                {"QID1", new DictionaryPayload
                {
                    Type= "Vad",
                    Description = "What is 1+1?",
                    Id = "QID1",
                    BlockElements = new List<SurveyBlockElementDto>
                    { 
                        new SurveyBlockElementDto
                        {
                            Type = "Text output",
                            QuestionId = "QID1"
                        },
                        new SurveyBlockElementDto
                        {
                            Type = "TEXT",
                            QuestionId = "QID2"
                        }
                    }
                }},
                {"QID3", new DictionaryPayload
                {
                    Type= "TEXT 2",
                    Description = "How old are you?",
                    Id = "QID3",
                    BlockElements = new List<SurveyBlockElementDto>
                    { 
                        new SurveyBlockElementDto
                        {
                            Type = "TEXT 2",
                            QuestionId = "QID3"
                        },
                        new SurveyBlockElementDto
                        {
                            Type = "TEXT 4",
                            QuestionId = "QID3"
                        }
                    }
                }}
            }
        };
       
        return await Task.FromResult(test);
    }

    private async Task<SurveyQuestionsDto> TestSurveyQuestions()
    {
        var testSurveyElements = new SurveyElementQuestionsDto
        {
            SurveyId = "123qa",
            Element = "QID1",
            PrimaryAttribute = "QID1",
            SecondaryAttribute = "QID1",
            TertiaryAttribute = "QID1",
            Payload = new PayloadQuestionDto
            {
                QuestionText = "What is your name?",
                DataExportTag = "Name",
                QuestionDescription = "Name",
                Choices = new Dictionary<string, ChoicesDto>
                {
                    {"1", new ChoicesDto
                    {
                        Display = "Name"
                    }}
                },
                Answers = new Dictionary<string, ChoicesDto>
                {
                    {"1", new ChoicesDto
                    {
                        Display = "Wish"
                    }}
                }
            }
        };
        
        var testSurveyElements2 = new SurveyElementQuestionsDto
        {
            SurveyId = "123qa",
            Element = "QID2",
            PrimaryAttribute = "QID2",
            SecondaryAttribute = "QID2",
            TertiaryAttribute = "QID2",
            Payload = new PayloadQuestionDto
            {
                QuestionText = "What is your name?",
                DataExportTag = "Name",
                QuestionDescription = "Name",
                Choices = new Dictionary<string, ChoicesDto>
                {
                    {"1", new ChoicesDto
                    {
                        Display = "Name"
                    }}
                }
            }
        };
        
        var testParent = new SurveyQuestionsDto
        {
            SurveyElements = [testSurveyElements, testSurveyElements2]
        };
        
        return await Task.FromResult(testParent);
    }
    
    private async Task<SurveyQuestionsDto> TestSurveyQuestionsChange()
    {
        var testSurveyElements2 = new SurveyElementQuestionsDto
        {
            SurveyId = "123qa",
            Element = "QID2",
            PrimaryAttribute = "QID2",
            SecondaryAttribute = "QID2",
            TertiaryAttribute = "QID2",
            Payload = new PayloadQuestionDto
            {
                QuestionText = "What is your name?",
                DataExportTag = "Name",
                QuestionDescription = "Name",
                Choices = new Dictionary<string, ChoicesDto>
                {
                    {"1", new ChoicesDto
                    {
                        Display = "Name"
                    }}
                }
            }
        };
        
        var testParent = new SurveyQuestionsDto
        {
            SurveyElements = [testSurveyElements2]
        };
        
        return await Task.FromResult(testParent);
    }
    
}