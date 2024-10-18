using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class AnswerOptionsBuilder
{
    private readonly AnswerOption _answerOption;
    private readonly SurveyBuilder _surveyBuilder = new();
    public readonly Guid Id = Guid.Parse("84b2e4c0-f782-459b-ba79-3636763fcc06");
    public const string Text = "Test Answer Option";
    public const string IndexId = "1";
    
    public AnswerOptionsBuilder()
    {
        var survey = _surveyBuilder.Build();
        
        _answerOption = new AnswerOption
        {
            Id = Id,
            SurveyId = survey.Id,
            Text = Text,
            IndexId = IndexId
        };
    }
    
    public AnswerOption Build()
    {
        return _answerOption;
    }
}