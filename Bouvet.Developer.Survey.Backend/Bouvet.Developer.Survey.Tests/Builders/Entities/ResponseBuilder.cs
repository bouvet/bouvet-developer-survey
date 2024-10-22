using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class ResponseBuilder
{
    private readonly Response _response;
    private readonly ChoiceBuilder _choiceBuilder = new();
    private readonly AnswerOptionsBuilder _answerOptionBuilder = new();
    public readonly Guid Id = Guid.Parse("594886e5-c2fe-49df-ae3c-245b52cba502");
    public const string CreatedAt = "10-Oct-21 10:00:00 +00:00";
    public const string UpdatedAt = "10-Oct-21 10:00:00 +00:00";
    public const string DeletedAt = "10-Oct-21 10:00:00 +00:00";
    
    public ResponseBuilder()
    {
        var choice = _choiceBuilder.Build();
        var answerOption = _answerOptionBuilder.Build();
        
        _response = new Response
        {
            Id = Id,
            ChoiceId = choice.Id,
            Choice = choice,
            AnswerOptionId = answerOption.Id,
            AnswerOption = answerOption,
            CreatedAt = DateTimeOffset.Parse(CreatedAt),
            UpdatedAt = DateTimeOffset.Parse(UpdatedAt),
            DeletedAt = DateTimeOffset.Parse(DeletedAt)
        };
    }
    
    public Response Build()
    {
        return _response;
    }
}