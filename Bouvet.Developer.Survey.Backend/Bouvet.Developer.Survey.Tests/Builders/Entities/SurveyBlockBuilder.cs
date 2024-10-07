using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class SurveyBlockBuilder
{
    private readonly SurveyBlock _surveyBlock;
    private readonly SurveyBuilder _surveyBuilder = new();
    public readonly Guid Id = Guid.Parse("c0d4a11e-95b7-42d5-ac0b-ec00e722396b");
    public const string Type = "Test Survey Block";
    public const string Description = "Test Survey Block Description";
    public const string SurveyBlockId = "block-123457";
    
    public SurveyBlockBuilder()
    {
        var survey = _surveyBuilder.Build();
        
        _surveyBlock = new SurveyBlock
        {
            Id = Id,
            SurveyGuid = survey.Id,
            Type = Type,
            Description = Description,
            SurveyBlockId = SurveyBlockId
        };
    }
    
    public SurveyBlock Build()
    {
        return _surveyBlock;
    }
}