using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class SurveyServiceTests
{

    private readonly ISurveyService _surveyService;
    private readonly DeveloperSurveyContext _context;
    private const string SurveyName = "Test survey";
    private const string SurveyId = "gaf2345";
    private const string SurveyLanguage = "English";


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
    
    private async Task DeleteAllSurveys()
    {
        var surveys = await _context.Surveys.ToListAsync();
        _context.Surveys.RemoveRange(surveys);
        await _context.SaveChangesAsync();
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
    public async Task Should_Get_All_Surveys()
    {
        await DeleteAllSurveys();
        
        // Arrange
        await CreateTestSurvey();
        await CreateTestSurvey();
        
        // Act
        var surveys = await _surveyService.GetSurveysAsync();
        
        // Assert
        Assert.NotNull(surveys);
        Assert.Equal(2, surveys.Count());
        
    }
    
    [Fact]
    public async Task Should_Get_Survey_By_Id()
    {
        // Arrange
        var testSurvey = await CreateTestSurvey();
        
        // Act
        var survey = await _surveyService.GetSurveyAsync(testSurvey.Id);
        
        // Assert
        Assert.NotNull(survey);
    }

    [Fact]
    public async Task Should_Update_Survey()
    {
        // Arrange
        var testSurvey = await CreateTestSurvey();
        
        var updatedSurveyDto = new NewSurveyDto
        {
            Name = "Updated survey",
            SurveyId = "updated",
            Language = "Language"
        };
        
        // Act
        var updatedSurvey = await _surveyService.UpdateSurveyAsync(testSurvey.Id, updatedSurveyDto);
        
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
        var testSurvey = await CreateTestSurvey();
        
        // Act
        await _surveyService.DeleteSurveyAsync(testSurvey.Id);
        
        // Assert
        var survey = await Assert.ThrowsAsync<NotFoundException>(() => _surveyService.GetSurveyAsync(testSurvey.Id));
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