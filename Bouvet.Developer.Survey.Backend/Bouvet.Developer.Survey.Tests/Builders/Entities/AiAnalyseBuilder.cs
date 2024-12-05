using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class AiAnalyseBuilder
{
    private readonly AiAnalyse _aiAnalyse;
    private readonly QuestionBuilder _questionBuilder = new();
    public readonly Guid Id = Guid.Parse("84b2e4c0-f782-459b-ba79-3636763fcc06");
    public const string Text = "Test Ai Analyse";
    
    public AiAnalyseBuilder()
    {
        var question = _questionBuilder.Build();
        
        _aiAnalyse = new AiAnalyse
        {
            Id = Id,
            QuestionId = question.Id,
            Text = Text
        };
    }
    
    public AiAnalyse Build()
    {
        return _aiAnalyse;
    }
}