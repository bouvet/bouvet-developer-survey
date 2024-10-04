namespace Bouvet.Developer.Survey.Domain.Interfaces;

public interface IEntitiesDateTimeOffsett
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}