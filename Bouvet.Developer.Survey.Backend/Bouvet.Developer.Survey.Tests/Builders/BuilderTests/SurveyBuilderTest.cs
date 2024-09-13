using Bouvet.Developer.Survey.Tests.Builders.Entities;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Builders.BuilderTests;

public class SurveyBuilderTest
{
    private readonly SurveyBuilder _surveyBuilder = new();
    private readonly BlockBuilder _blockBuilder = new();

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
        Assert.Equal(SurveyBuilder.SurveyUrl, survey.SurveyUrl);
    }
    
    [Fact]
    public void BlockBuilder_ShouldCreateBlock()
    {
        // Act
        var block = _blockBuilder.Build();
        
        // Assert
        Assert.NotNull(block);
        Assert.Equal(_blockBuilder.Id, block.Id);
        Assert.Equal(BlockBuilder.Type, block.Type);
        Assert.Equal(_surveyBuilder.Id, block.SurveyId);
        Assert.Equal(BlockBuilder.Question, block.Question);
    }
}