namespace Bouvet.Developer.Survey.Domain.Entities;

public class Survey
{
    public Guid Id { get; set; }
    public string SurveyId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string SurveyUrl { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public ICollection<Block>? Blocks { get; set; }
}