using Bouvet.Developer.Survey.Domain.Entities;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class BlockBuilder
{
    private readonly SurveyBuilder _surveyBuilder = new();
    private readonly Block _block;
    public readonly Guid Id = Guid.Parse("5f821873-e019-4cf7-b089-7731d986b0f8");
    public const string Type = "Question";
    public const string Question = "What is 1 + 1?";
    
    public BlockBuilder()
    {
        var survey = _surveyBuilder.Build();
        
        _block = new Block
        {
            Id = Id,
            Type = Type,
            SurveyId = survey.Id,
            Question = Question
        };
    }
    
    public Block Build()
    {
        return _block;
    }
    
}