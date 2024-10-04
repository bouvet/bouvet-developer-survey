using Bouvet.Developer.Survey.Tests.Builders.Entities;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Builders.BuilderTests;

public class SurveyBuilderTest
{
    private readonly SurveyBuilder _surveyBuilder = new();

    [Fact]
    public void SurveyBuilder_ShouldCreateSurvey()
    {
        // Act
        var survey = _surveyBuilder.Build();
        
        // Assert
        Assert.NotNull(survey);
        Assert.Equal(_surveyBuilder.Id, survey.Id);
        Assert.Equal(SurveyBuilder.SurveyId, survey.SurveyId);
        Assert.Equal(SurveyBuilder.Name, survey.Name);
        Assert.Equal(SurveyBuilder.SurveyLanguage, survey.SurveyLanguage);
    }
}