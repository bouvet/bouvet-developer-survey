namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class SurveyBuilder
{
    private readonly Domain.Entities.Survey.Survey _survey;
    public readonly Guid Id = Guid.Parse("e8d01793-09ce-40df-99ca-490202d6ec87");
    public const string SurveyId = "ga-123456";
    public const string Name = "Test Survey";
    public const string SurveyLanguage = "NO";

    public SurveyBuilder()
    {
        _survey = new Domain.Entities.Survey.Survey
        {
            Id = Id,
            SurveyId = SurveyId,
            Name = Name,
            SurveyLanguage = SurveyLanguage
        };
    }
    
    public Domain.Entities.Survey.Survey Build()
    {
        return _survey;
    }
    
}