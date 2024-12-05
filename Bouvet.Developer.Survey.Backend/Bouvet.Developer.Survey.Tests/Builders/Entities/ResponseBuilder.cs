using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class ResponseBuilder
{
    private readonly Response _response;
    private readonly ChoiceBuilder _choiceBuilder = new();
    public readonly Guid Id = Guid.Parse("594886e5-c2fe-49df-ae3c-245b52cba502");
    public const string CreatedAt = "10-Oct-21 10:00:00 +00:00";
    public const string UpdatedAt = "10-Oct-21 10:00:00 +00:00";
    public const string DeletedAt = "10-Oct-21 10:00:00 +00:00";
    
    public ResponseBuilder()
    {
        var choice = _choiceBuilder.Build();
        
        _response = new Response
        {
            Id = Id,
            ChoiceId = choice.Id,
            Choice = choice,
            CreatedAt = DateTimeOffset.Parse(CreatedAt),
            UpdatedAt = DateTimeOffset.Parse(UpdatedAt),
        };
    }
    
    public Response Build()
    {
        return _response;
    }
}