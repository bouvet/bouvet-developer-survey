using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class QuestionServiceTest
{
    private readonly IQuestionService _questionService;
    private readonly IChoiceService _choiceService;
    private readonly IBlockElementService _blockElementService;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly ISurveyService _surveyService;
    
    private const string SurveyId = "g_tag";
    private const string SurveyLanguage = "English";
    private const string ElementType = "TEXT";
    private const string ElementQuestionId = "QID2";
    
    public QuestionServiceTest()
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
    }

    private async Task<QuestionDto> CreateTestQuestionAsync()
    {
        await CreateInitialDataAsync();

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
    public async Task TestErrorHandling()
    {
        var newQuestionDto = new NewQuestionDto
        {
            BlockElementId = "1234",
            SurveyId = SurveyId,
            DateExportTag = "2021-09-01",
            QuestionText = "What is your name?",
            QuestionDescription = "Name"
        };
        
        var testError1 = await Assert.ThrowsAsync<NotFoundException>(() => _questionService.CreateQuestionAsync(newQuestionDto));
        
        Assert.Equal("Block element not found", testError1.Message);
        
        var testError2 = await Assert.ThrowsAsync<NotFoundException>(() => _questionService.GetQuestionByIdAsync(Guid.NewGuid()));
        
        Assert.Equal("Question not found", testError2.Message);
        
        var testError3 = await Assert.ThrowsAsync<NotFoundException>(() => _questionService.GetQuestionsBySurveyBlockIdAsync(Guid.NewGuid()));
        
        Assert.Equal("No questions found", testError3.Message);
        
        var testError4 = await Assert.ThrowsAsync<NotFoundException>(() => _questionService.UpdateQuestionAsync(Guid.NewGuid(), new NewQuestionDto
        {
            SurveyId = SurveyId,
            DateExportTag = "2021-09-01",
            QuestionText = "What is your name?",
            QuestionDescription = "Name"
        }));
        
        Assert.Equal("Question not found", testError4.Message);
        
        var testError5 = await Assert.ThrowsAsync<NotFoundException>(() => _questionService.DeleteQuestionAsync(Guid.NewGuid()));
        
        Assert.Equal("Question not found", testError5.Message);
    }
    
    [Fact]
    public async Task Should_Create_Question()
    {
        // Arrange
       var question = await CreateTestQuestionAsync();
       
        // Act
        var result = await _questionService.GetQuestionByIdAsync(question.Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(question.Id, result.Id);
    }
    

    [Fact]
    public async Task Should_Create_Question_With_Choices()
    {
        await CreateInitialDataAsync();
        // Arrange
        var question = new NewQuestionDto
        {
            BlockElementId = ElementQuestionId,
            SurveyId = SurveyId,
            QuestionId = "QID1",
            DateExportTag = "2021-09-01",
            QuestionText = "What is your name?",
            QuestionDescription = "Name",
            Choices = new List<NewChoiceDto>
            {
                new NewChoiceDto
                {
                    IndexId = "1",
                    Text = "Choice 1",
                },
                new NewChoiceDto
                {
                    IndexId = "2",
                    Text = "Choice 2",
                }
            }
        };
        
        // Act
        var result = await _questionService.CreateQuestionAsync(question);
        
        // Assert
        Assert.NotNull(result);
        if (result.Choices != null) Assert.Equal(question.Choices.Count, result.Choices.Count());
        
        var choices = await _choiceService.GetChoices(result.Id);
        
        Assert.NotNull(choices);
        Assert.Equal(question.Choices.Count, choices.Count());
    }
    
    [Fact]
    public async Task Should_Get_Questions_By_Survey_Block_Id()
    {
        // Arrange
        var question = await CreateTestQuestionAsync();
        
        // Act
        var result = await _questionService.GetQuestionsBySurveyBlockIdAsync(question.BlockElementId);
        
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task Should_Update_Question()
    {
        // Arrange
        var question = await CreateTestQuestionAsync();
        
        var updatedQuestionDto = new NewQuestionDto
        { 
            SurveyId = SurveyId,
            DateExportTag = "TAG",
            QuestionText = "What is your name?",
            QuestionDescription = "Name"
        };
        
        // Act
        var updatedQuestion = await _questionService.UpdateQuestionAsync(question.Id, updatedQuestionDto);
        
        // Assert
        Assert.NotNull(updatedQuestion);
        Assert.Equal(updatedQuestionDto.QuestionText, updatedQuestion.QuestionText);
    }
    
    [Fact]
    public async Task Should_Delete_Question()
    {
        // Arrange
        var question = await CreateTestQuestionAsync();
        
        // Act
        await _questionService.DeleteQuestionAsync(question.Id);
        
        // Assert
        var result = await Assert.ThrowsAsync<NotFoundException>(() => _questionService.GetQuestionByIdAsync(question.Id));
        
        Assert.Equal("Question not found", result.Message);
    }
}