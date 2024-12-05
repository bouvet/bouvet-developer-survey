using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Ai;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Ai;
using Bouvet.Developer.Survey.Service.Survey.Results;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Ai;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Result;

public class AiAnalyseServiceTests
{
    private readonly IQuestionService _questionService;
    private readonly IChoiceService _choiceService;
    private readonly IBlockElementService _blockElementService;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly ISurveyService _surveyService;
    private readonly IAiAnalyseService _aiAnalyseService;
    
    private const string SurveyId = "g_tag";
    private const string SurveyLanguage = "English";
    private const string ElementType = "TEXT";
    private const string ElementQuestionId = "QID2";
    
    public AiAnalyseServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "TestChoicesDatabase")
            .Options;

        var context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _choiceService = new ChoiceService(context);
        _blockElementService = new BlockElementService(context);
        _surveyBlockService = new SurveyBlockService(context);
        _surveyService = new SurveyService(context);
        _questionService = new QuestionService(context, _choiceService);
        _aiAnalyseService = new AiAnalyseService(context);
    }
    
    private async Task<QuestionDetailsDto> CreateInitialDataAsync()
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
        
        return await _questionService.CreateQuestionAsync(new NewQuestionDto
        {
            BlockElementId = ElementQuestionId,
            SurveyId = SurveyId,
            QuestionId = "QID1",
            DateExportTag = "2021-09-01",
            QuestionText = "What is your name?",
            QuestionDescription = "Name",
        });
    }

    [Fact]
    public async Task Create_Ai_Analyse()
    {
        var question = await CreateInitialDataAsync();
        
        var aiAnalyse = await _aiAnalyseService.CreateAiAnalyse(new NewAiAnalyseDto
        {
            QuestionId = question.Id,
            Text = "Test Ai Analyse"
        });
        
        Assert.NotNull(aiAnalyse);
        Assert.Equal("Test Ai Analyse", aiAnalyse.Text);
        
        var aiAnalyseFromDb = await _aiAnalyseService.GetAiAnalysesByQuestionId(question.Id);
        
        Assert.NotNull(aiAnalyseFromDb);
        Assert.Equal("Test Ai Analyse", aiAnalyse.Text);
        
        var testErrorHandling = new NewAiAnalyseDto
        {
            QuestionId = new Guid(),
            Text = "Test Ai Analyse"
        };
        
        var error = await Assert.ThrowsAsync<NotFoundException>(() => _aiAnalyseService.CreateAiAnalyse(testErrorHandling));
        
        Assert.Equal("Question not found", error.Message);
        
        var testFromDbError = await _aiAnalyseService.GetAiAnalysesByQuestionId(new Guid());
        
        Assert.Null(testFromDbError);
    }
    
    [Fact]
    public async Task Update_Ai_Analyse()
    {
        var question = await CreateInitialDataAsync();
        
        var aiAnalyse = await _aiAnalyseService.CreateAiAnalyse(new NewAiAnalyseDto
        {
            QuestionId = question.Id,
            Text = "Test Ai Analyse"
        });
        
        var updatedAiAnalyse = await _aiAnalyseService.UpdateAiAnalyse(aiAnalyse.Id, new NewAiAnalyseDto
        {
            Text = "Updated Ai Analyse"
        });
        
        Assert.NotNull(updatedAiAnalyse);
        Assert.Equal("Updated Ai Analyse", updatedAiAnalyse.Text);
        
        var testErrorHandling = new NewAiAnalyseDto
        {
            Text = "Updated Ai Analyse"
        };
        
        var error = await Assert.ThrowsAsync<NotFoundException>(() => _aiAnalyseService.UpdateAiAnalyse(new Guid(), testErrorHandling));
        
        Assert.Equal("AiAnalyse not found", error.Message);
    }
    
    [Fact]
    public async Task Delete_Ai_Analyse()
    {
        var question = await CreateInitialDataAsync();
        
        var aiAnalyse = await _aiAnalyseService.CreateAiAnalyse(new NewAiAnalyseDto
        {
            QuestionId = question.Id,
            Text = "Test Ai Analyse"
        });
        
        await _aiAnalyseService.DeleteAiAnalyse(aiAnalyse.Id);
        
        var testFromDbError = await _aiAnalyseService.GetAiAnalysesByQuestionId(new Guid());
        
        Assert.Null(testFromDbError);
        
        var testErrorHandling = await Assert.ThrowsAsync<NotFoundException>(() => _aiAnalyseService.DeleteAiAnalyse(new Guid()));
        
        Assert.Equal("AiAnalyse not found", testErrorHandling.Message);
    }
}