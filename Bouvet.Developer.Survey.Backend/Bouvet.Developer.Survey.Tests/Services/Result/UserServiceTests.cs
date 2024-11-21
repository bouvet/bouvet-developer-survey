using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Results;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Result;

public class UserServiceTests
{
    private readonly DeveloperSurveyContext _context;
    private readonly IUserService _userService;
    private readonly ISurveyService _surveyService;
    private const string SurveyName = "Test survey";
    private const string SurveyId = "gaf2345";
    private const string SurveyLanguage = "English";
    
    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "UserDb")
            .Options;
        
        _context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _surveyService = new SurveyService(_context);
        _userService = new UserService(_context);
    }
    
    private async Task<SurveyDto> CreateTestSurvey()
    {
        var newSurveyDto = new NewSurveyDto
        {
            Name = SurveyName,
            SurveyId = SurveyId,
            Language = SurveyLanguage
        };

        return await _surveyService.CreateSurveyAsync(newSurveyDto);
    }

    [Fact]
    public async Task Create_User_Batch()
    {
        // Arrange
        var survey = await CreateTestSurvey();
        
        var userBatchDto = new List<NewUserDto>
        {
            new()
            {
                SurveyId = survey.Id,
                RespondId = "123"
            },
            new()
            {
                SurveyId = survey.Id,
                RespondId = "456"
            }
        };
        
        // Act
        var userDto = await _userService.CreateUserBatch(userBatchDto, survey.Id);
        
        // Assert
        Assert.NotNull(userDto);
        Assert.Equal(userBatchDto.First().RespondId, userDto.RespondId);
        
        var users = await _userService.GetUsersBySurveyId(survey.Id);
        
        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.Count());
        
        //Check for error
        var testError = await Assert.ThrowsAsync<NotFoundException>(() => 
            _userService.CreateUserBatch(userBatchDto, Guid.NewGuid()));
        
        Assert.Equal("Survey not found", testError.Message);
    }

    [Fact]
    public async Task Get_User_Response()
    {
        // Arrange
        var survey = await CreateTestSurvey();
        
        var userBatchDto = new List<NewUserDto>
        {
            new()
            {
                SurveyId = survey.Id,
                RespondId = "123"
            },
            new()
            {
                SurveyId = survey.Id,
                RespondId = "456"
            }
        };
        
        // Act
        var userDto = await _userService.CreateUserBatch(userBatchDto, survey.Id);
        
        // Assert
        Assert.NotNull(userDto);
        Assert.Equal(userBatchDto.First().RespondId, userDto.RespondId);
        
        
        // Assert
        var responseUsers = await _userService.GetUserResponses(userDto.Id);
        
        // Assert
        Assert.NotNull(responseUsers);
    }

    [Fact]
    public async Task Connect_User_To_Response()
    {
        // Arrange
        var survey = await CreateTestSurvey();
        
        var userBatchDto = new List<NewUserDto>
        {
            new()
            {
                SurveyId = survey.Id,
                RespondId = "123"
            },
            new()
            {
                SurveyId = survey.Id,
                RespondId = "456"
            }
        };
        
        // Act
        var userDto = await _userService.CreateUserBatch(userBatchDto, survey.Id);
        
        // Assert
        Assert.NotNull(userDto);
        Assert.Equal(userBatchDto.First().RespondId, userDto.RespondId);
        
        var responseUserDto = new List<NewResponseUserDto>
        {
            new()
            {
                UserId = userDto.Id,
                ResponseId = Guid.NewGuid(),
                QuestionId = Guid.NewGuid()
            }
        };
        
        // Act
        await _userService.ConnectResponseToUser(responseUserDto);
    }
    
    [Fact]
    public async Task Delete_user()
    {
        // Arrange
        var survey = await CreateTestSurvey();
        
        var userBatchDto = new List<NewUserDto>
        {
            new()
            {
                SurveyId = survey.Id,
                RespondId = "123"
            },
            new()
            {
                SurveyId = survey.Id,
                RespondId = "456"
            }
        };
        
        // Act
        var userDto = await _userService.CreateUserBatch(userBatchDto, survey.Id);
        
        // Assert
        Assert.NotNull(userDto);
        Assert.Equal(userBatchDto.First().RespondId, userDto.RespondId);
        
        var users = await _userService.GetUsersBySurveyId(survey.Id);
        
        // Assert
        Assert.NotNull(users);
        Assert.Equal(2, users.Count());
        
        // Act
        await _userService.DeleteUser(users.First().Id);
        
        // Assert
        var usersAfterDelete = await _userService.GetUsersBySurveyId(survey.Id);
        Assert.NotNull(usersAfterDelete);
        Assert.Single(usersAfterDelete);
    }
    
}