namespace Bouvet.Developer.Survey.Domain.Interfaces;

public interface ILastSyncedOffset
{
    public DateTimeOffset? LastSyncedAt { get; set; }
}