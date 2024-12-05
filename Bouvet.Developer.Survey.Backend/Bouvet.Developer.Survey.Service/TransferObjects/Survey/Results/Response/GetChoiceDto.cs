using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class GetChoiceDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public string IndexId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual GetStatsDto? Statistics { get; set; }
    
    
    public static GetChoiceDto CreateFromEntity(Choice choice,GetStatsDto? response)
    {
        return new GetChoiceDto
        {
            Id = choice.Id,
            Text = choice.Text,
            IndexId = choice.IndexId,
            CreatedAt = choice.CreatedAt,
            UpdatedAt = choice.UpdatedAt,
            Statistics = response
        };
    }
}