using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Ai;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Ai;
using Bouvet.Developer.Survey.Service.Survey.Results;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Result;

public class ResultServiceTests
{
    private readonly IResultService _resultService;
    private readonly IQuestionService _questionService;
    private readonly IResponseService _responseService;
    private readonly ISurveyService _surveyService;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly IBlockElementService _blockElementService;
    private readonly IImportSurveyService _importSurveyService;
    private readonly IConfiguration _configuration;
    
    
    private const string SurveyId = "g_tag";
    private const string SurveyLanguage = "English";
    private const string ElementType = "TEXT";
    private const string ElementQuestionId = "QID2";
    
    public ResultServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "TestResultsDatabase")
            .Options;

        var context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _questionService = new QuestionService(context, new ChoiceService(context));
        _responseService = new ResponseService(context);
        _surveyService = new SurveyService(context);
        _surveyBlockService = new SurveyBlockService(context);
        _blockElementService = new BlockElementService(context);
        _resultService = new ResultService(context, _questionService, _responseService);
        IChoiceService choiceService = new ChoiceService(context);
        IQuestionService questionService = new QuestionService(context, choiceService);
        IAiService aiService = new AiService(_configuration, questionService, new AiAnalyseService(context), context);
        _importSurveyService = new ImportSurveyService(_surveyService,context,_questionService,_surveyBlockService,
            _blockElementService,_resultService, new CsvToJsonService(), new UserService(context), aiService);
    }
    
    private async Task CreateInitialDataAsync()
    {
        // Create test survey
        await _surveyService.CreateSurveyAsync(new NewSurveyDto
        {
            Name = "Test survey",
            SurveyId = SurveyId,
            Language = SurveyLanguage
        });

        // Create test block
        var surveyBlock = await _surveyBlockService.CreateSurveyBlock(new NewSurveyBlockDto
        {
            Type = "Test survey",
            SurveyId = SurveyId,
            Description = "English",
            SurveyBlockId = "ga_567"
        });

        // Create test block elements
        await _blockElementService.CreateBlockElements([
            new NewBlockElementDto
            {
                QuestionId = ElementQuestionId,
                Type = ElementType,
                BlockId = surveyBlock.Id
            }
        ]);
        
        //Create test Question
        await _questionService.CreateQuestionAsync(new NewQuestionDto
        {
            BlockElementId = ElementQuestionId,
            SurveyId = SurveyId,
            QuestionId = "QID1",
            DateExportTag = "Tag",
            QuestionText = "What is your name?",
            QuestionDescription = "Name",
        });
    }
    
    private Task<List<FieldDto>> CreateTestFieldDto()
    {
        return Task.FromResult(new List<FieldDto>([
            new FieldDto
            {
                ResponseId = "1",
                FieldName = "Tag_1",
                Value = "John Doe"
            },

            new FieldDto
            {
                ResponseId = "2",
                FieldName = "Tag_2",
                Value = "Jane Doe"
            },

            new FieldDto
            {
                ResponseId = "3",
                FieldName = "Tag_3",
                Value = "John Doe"
            }
        ]));
    }
    
    [Fact]
    public async Task Get_Questions_By_SurveyId()
    {
        // Arrange
        await CreateInitialDataAsync();
        
        // Act
        var questions = await _resultService.GetQuestions(SurveyId);
        
        // Assert
        Assert.NotNull(questions);
    }
    
    // [Fact]
    // public async Task Check_For_Differences()
    // {
    //     // Arrange
    //     await CreateInitialDataAsync();
    //     
    //     var fieldDto = await CreateTestFieldDto();
    //     
    //     // Act
    //     await _importSurveyService.MapFieldsToResponse(fieldDto, SurveyId);
    //     
    // }
}