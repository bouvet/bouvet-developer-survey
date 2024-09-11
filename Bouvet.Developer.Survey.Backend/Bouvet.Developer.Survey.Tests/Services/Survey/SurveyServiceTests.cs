using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class SurveyServiceTests
{

    private readonly ISurveyService _surveyService;
    private readonly DeveloperSurveyContext _context;
    private const string SurveyName = "Test survey";
    private const string SurveyId = "gaf2345";
    private const string SurveyUrl = "https://todo.com";


    public SurveyServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _surveyService = new SurveyService(_context);
    }
    
    [Fact]
    public async Task Should_Get_All_Surveys()
    {
        // Arrange
        var newSurveyDto1 = new NewSurveyDto
        {
            Name = SurveyName,
            SurveyId = SurveyId,
            SurveyUrl = SurveyUrl
        };
    
        var newSurveyDto2 = new NewSurveyDto
        {
            Name = "Test",
            SurveyId = "2345",
            SurveyUrl = "https://vg.com"
        };
    
        await _surveyService.CreateSurveyAsync(newSurveyDto1);
        await _surveyService.CreateSurveyAsync(newSurveyDto2);
        
        // Act
        var surveys = await _surveyService.GetSurveysAsync();
        
        // Assert
        Assert.NotNull(surveys);
        
    }
    
    [Fact]
    public async Task Should_Get_Survey_By_Id()
    {
        // Arrange
        
        var newSurveyDto = new NewSurveyDto
        {
            Name = SurveyName,
            SurveyId = SurveyId,
            SurveyUrl = SurveyUrl
        };
        
        var newSurvey = await _surveyService.CreateSurveyAsync(newSurveyDto);
        
        // Act
        var survey = await _surveyService.GetSurveyAsync(newSurvey.Id);
        
        // Assert
        Assert.NotNull(survey);
    }

    [Fact]
    public async Task Should_Update_Survey()
    {
        // Arrange
        var newSurveyDto = new NewSurveyDto
        {
            Name = SurveyName,
            SurveyId = SurveyId,
            SurveyUrl = SurveyUrl
        };
        
        var newSurvey = await _surveyService.CreateSurveyAsync(newSurveyDto);
        
        var updatedSurveyDto = new NewSurveyDto
        {
            Name = "Updated survey",
            SurveyId = "updated",
            SurveyUrl = "https://updated.com"
        };
        
        // Act
        var updatedSurvey = await _surveyService.UpdateSurveyAsync(newSurvey.Id, updatedSurveyDto);
        
        // Assert
        Assert.NotNull(updatedSurvey);
        Assert.Equal(updatedSurveyDto.Name, updatedSurvey.Name);
    }
    
    [Fact]
    public async Task Should_ThrowError_When_Updating_NonExistingSurvey()
    {
        // Act
        var updatedSurvey = await Assert.ThrowsAsync<NotFoundException>(() => 
            _surveyService.UpdateSurveyAsync(Guid.NewGuid(), new NewSurveyDto()));
        
        // Assert
        Assert.Equal("Survey not found", updatedSurvey.Message);
    }
    
    [Fact]
    public async Task Should_Delete_Survey_And_ThrowError_When_Getting_DeletedSurvey()
    {
        // Arrange
        var newSurveyDto = new NewSurveyDto
        {
            Name = SurveyName,
            SurveyId = SurveyId,
            SurveyUrl = SurveyUrl
        };
        
        var newSurvey = await _surveyService.CreateSurveyAsync(newSurveyDto);
        
        // Act
        await _surveyService.DeleteSurveyAsync(newSurvey.Id);
        
        // Assert
        var survey = await Assert.ThrowsAsync<NotFoundException>(() => _surveyService.GetSurveyAsync(newSurvey.Id));
        Assert.Equal("Survey not found", survey.Message);
    }
    
    [Fact]
    public async Task Should_ThrowError_When_Deleting_NonExistingSurvey()
    {
        // Act
        var deletedSurvey = await Assert.ThrowsAsync<NotFoundException>(() => _surveyService.DeleteSurveyAsync(Guid.NewGuid()));
        
        // Assert
        Assert.Equal("Survey not found", deletedSurvey.Message);
    }
}