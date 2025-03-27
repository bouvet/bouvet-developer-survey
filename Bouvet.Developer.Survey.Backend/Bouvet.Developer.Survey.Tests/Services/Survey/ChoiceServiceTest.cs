using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class ChoiceServiceTest
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
    
    public ChoiceServiceTest()
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

    private async Task<List<ChoiceDto>> CreateTestChoices()
    {
        await CreateInitialDataAsync();
        
        var question = await _questionService.CreateQuestionAsync(new NewQuestionDto
        {
            BlockElementId = ElementQuestionId,
            SurveyId = SurveyId,
            QuestionId = "QID2",
            DataExportTag = "2021-09-01",
            QuestionText = "What is your name?",
            QuestionDescription = "Name",
        });

        var choices = new List<NewChoiceDto>
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
        };
        
        return await _choiceService.CreateChoice(choices, question.Id);
    }

    [Fact]
    public async Task CreateChoice_ThrowsNotFoundException_WhenQuestionDoesNotExist()
    {
        var createChoiceError = await Assert.ThrowsAsync<NotFoundException>(() => _choiceService.CreateChoice(new List<NewChoiceDto>
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
        }, new Guid()));

        Assert.Equal("Question not found", createChoiceError.Message);
    }

    [Fact]
    public async Task GetChoice_ThrowsNotFoundException_WhenChoiceDoesNotExist()
    {
        var getChoiceError = await Assert.ThrowsAsync<NotFoundException>(() => _choiceService.GetChoice(new Guid()));

        Assert.Equal("Choice not found", getChoiceError.Message);
    }

    [Fact]
    public async Task GetChoices_ThrowsNotFoundException_WhenNoChoicesExist()
    {
        var getChoicesError = await Assert.ThrowsAsync<NotFoundException>(() => _choiceService.GetChoices(new Guid()));

        Assert.Equal("No choices found", getChoicesError.Message);
    }

    [Fact]
    public async Task UpdateChoice_ThrowsNotFoundException_WhenChoiceDoesNotExist()
    {
        var updateChoiceError = await Assert.ThrowsAsync<NotFoundException>(() => _choiceService.UpdateChoice(new Guid(), new NewChoiceDto
        {
            Text = "Updated choice"
        }));

        Assert.Equal("Choice not found", updateChoiceError.Message);
    }

    [Fact]
    public async Task DeleteChoice_ThrowsNotFoundException_WhenChoiceDoesNotExist()
    {
        var deleteChoiceError = await Assert.ThrowsAsync<NotFoundException>(() => _choiceService.DeleteChoice(new Guid()));

        Assert.Equal("Choice not found", deleteChoiceError.Message);
    }


    [Fact]
    public async Task Should_Create_Choice()
    {
        // Arrange
        var choices = await CreateTestChoices();
        
        // Act
        var result = await _choiceService.GetChoice(choices.First().Id);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal("Choice 1", result.Text);
    }
    
    [Fact]
    public async Task Should_Update_Choice()
    {
        // Arrange
        var choices = await CreateTestChoices();
        
        // Act
        var updatedChoice = await _choiceService.UpdateChoice(choices.First().Id, new NewChoiceDto
        {
            Text = "Updated choice"
        });
        
        // Assert
        Assert.NotNull(updatedChoice);
        Assert.Equal("Updated choice", updatedChoice.Text);
    }
    
    [Fact]
    public async Task Should_Delete_Choice()
    {
        // Arrange
        var choices = await CreateTestChoices();
        
        // Act
        await _choiceService.DeleteChoice(choices.First().Id);
        
        // Assert
        var choice = await Assert.ThrowsAsync<NotFoundException>(() => _choiceService.GetChoice(choices.First().Id));
        
        Assert.Equal("Choice not found", choice.Message);
    }

}