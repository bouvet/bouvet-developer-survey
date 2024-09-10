using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Moq;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class SurveyServiceTests
{

    private readonly Mock<ISurveyService> _surveyServiceMock;

    public SurveyServiceTests()
    {
        _surveyServiceMock = new Mock<ISurveyService>();
    }

    [Fact]
    public async Task Should_Create_New_Survey()
    {
        // Arrange
        var newSurveyDto = new NewSurveyDto
        {
            Name = "Test survey",
            SurveyId = "gaf2345",
            SurveyUrl = "https://todo.com"
        };

        var expectedSurvey = new SurveyDto
        {
            Name = newSurveyDto.Name,
        };

        _surveyServiceMock.Setup(service => service.CreateSurveyAsync(newSurveyDto))
            .ReturnsAsync(expectedSurvey);

        // Act
        var newSurvey = await _surveyServiceMock.Object.CreateSurveyAsync(newSurveyDto);

        // Assert
        Assert.NotNull(newSurvey);
        Assert.Equal(newSurveyDto.Name, newSurvey.Name);
    }
}