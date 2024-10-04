using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class Choice : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public string QuestionId { get; set; } = null!;
    public virtual Question Question { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}