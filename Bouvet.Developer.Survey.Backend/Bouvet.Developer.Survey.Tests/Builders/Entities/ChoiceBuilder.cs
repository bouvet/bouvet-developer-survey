using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class ChoiceBuilder
{
    private readonly Choice _choice;
    private readonly QuestionBuilder _questionBuilder = new();
    public readonly Guid Id = Guid.Parse("c0d4a11e-95b7-42d5-ac0b-ec00e722396e");
    public const string Text = "Test Choice";
    
    public ChoiceBuilder()
    {
        var question = _questionBuilder.Build();
        
        _choice = new Choice
        {
            Id = Id,
            QuestionId = question.Id,
            Text = Text
        };
    }
    
    public Choice Build()
    {
        return _choice;
    }
}