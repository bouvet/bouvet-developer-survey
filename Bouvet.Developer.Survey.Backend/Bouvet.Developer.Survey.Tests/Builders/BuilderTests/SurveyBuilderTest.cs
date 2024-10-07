using Bouvet.Developer.Survey.Tests.Builders.Entities;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Builders.BuilderTests;

public class SurveyBuilderTest
{
    private readonly SurveyBuilder _surveyBuilder = new();
    private readonly SurveyBlockBuilder _surveyBlockBuilder = new();
    private readonly BlockElementBuilder _blockElementBuilder = new();

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
    
    [Fact]
    public void SurveyBuilder_ShouldCreateSurveyWithSurveyBlock()
    {
        // Arrange
        var surveyBlock = _surveyBlockBuilder.Build();
        
        // Assert
        Assert.NotNull(surveyBlock);
        Assert.Equal(_surveyBlockBuilder.Id, surveyBlock.Id);
        Assert.Equal(SurveyBlockBuilder.Type, surveyBlock.Type);
        Assert.Equal(SurveyBlockBuilder.Description, surveyBlock.Description);
        Assert.Equal(SurveyBlockBuilder.SurveyBlockId, surveyBlock.SurveyBlockId);
        Assert.Equal(_surveyBuilder.Id, surveyBlock.SurveyGuid);
    }
    
    [Fact]
    public void SurveyBuilder_ShouldCreateSurveyWithSurveyBlockWithBlockElement()
    {
        // Arrange
        var blockElement = _blockElementBuilder.Build();
        
        // Assert
        Assert.NotNull(blockElement);
        Assert.Equal(_blockElementBuilder.Id, blockElement.Id);
        Assert.Equal(BlockElementBuilder.Type, blockElement.Type);
        Assert.Equal(BlockElementBuilder.QuestionId, blockElement.QuestionId);
        Assert.Equal(_surveyBlockBuilder.Id, blockElement.SurveyElementGuid);
    }
}