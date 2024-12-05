using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Results;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Result;

public class ResponseServiceTests
{
    private readonly IResponseService _responseService;
    private readonly IQuestionService _questionService;
    private readonly IChoiceService _choiceService;
    private readonly IBlockElementService _blockElementService;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly ISurveyService _surveyService;
    private const string SurveyId = "g_tag";
    private const string SurveyLanguage = "English";
    private const string ElementType = "TEXT";
    private const string ElementQuestionId = "QID2";
    
    public ResponseServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "TestResponsesDatabase")
            .Options;

        var context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _choiceService = new ChoiceService(context);
        _responseService = new ResponseService(context);
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
    
    private async Task<List<ResponseDto>> CreateTestResponse()
    {
        await CreateInitialDataAsync();
        
        var question = await _questionService.CreateQuestionAsync(new NewQuestionDto
        {
            BlockElementId = ElementQuestionId,
            SurveyId = SurveyId,
            QuestionId = "QID2",
            DateExportTag = "2021-09-01",
            QuestionText = "What is your name?",
            QuestionDescription = "Name",
        });

        var choices = new List<NewChoiceDto>
        {
            new NewChoiceDto
            {
                IndexId = "1",
                Text = "Choice 1",
            }
        };
        
        await _choiceService.CreateChoice(choices, question.Id);
        
        var getChoices = await _choiceService.GetChoices(question.Id);
        
        var responseDtoList = new List<NewResponseDto>
        {
            new()
            {
                ChoiceId = getChoices.First().Id,
                Value = 1,
                FieldName = "QID2_1"
            },
            new()
            {
                ChoiceId = getChoices.First().Id,
                Value = 2,
                FieldName = "QID2_2"
            },
        };
        
        
        return await _responseService.CreateResponse(responseDtoList);
    }
    
    [Fact]
    public async Task TestCreateResponse()
    {
        var response = await CreateTestResponse();
        
        Assert.NotNull(response);
    }

    [Fact]
    public async Task TestCreateResponseWithWrongChoiceId()
    {
        var responseDtoList = new List<NewResponseDto>
        {
            new()
            {
                ChoiceId = Guid.NewGuid(),
                Value = 1,
                FieldName = "QID2_1"
            }
        };
        
        var testError = await Assert.ThrowsAsync<NotFoundException>(() => _responseService.CreateResponse(responseDtoList));
        
        Assert.Equal("Choice not found", testError.Message);
    }
    
    [Fact]
    public async Task TestGetResponse()
    {
        var response = await CreateTestResponse();
        
        var getResponse = await _responseService.GetResponse(response.First().Id);
        
        Assert.NotNull(getResponse);
        Assert.Equal(response.First().Id, getResponse.Id);
    }
    
    [Fact]
    public async Task Should_GetResponses_By_Choice_Id()
    {
        var response = await CreateTestResponse();
        
        var getResponses = await _responseService.GetResponsesByChoiceId(response.First().ChoiceId);
        
        Assert.NotNull(getResponses);
    }

    [Fact]
    public async Task Should_GetResponses_With_Wrong_Choice_Id()
    {
        var testError = await Assert.ThrowsAsync<NotFoundException>(() => _responseService.GetResponsesByChoiceId(Guid.NewGuid()));
        
        Assert.Equal("No responses found", testError.Message);
    }
    
    [Fact]
    public async Task Should_delete_response()
    {
        var response = await CreateTestResponse();
        
        await _responseService.DeleteResponse(response.First().Id);
        
        var getResponse = await Assert.ThrowsAsync<NotFoundException>(() => _responseService.GetResponse(response.First().Id));
        
        Assert.Equal("Response not found", getResponse.Message);
        
        var testError = await Assert.ThrowsAsync<NotFoundException>(() => _responseService.DeleteResponse(Guid.NewGuid()));
        
        Assert.Equal("Response not found", testError.Message);
    }
    
}