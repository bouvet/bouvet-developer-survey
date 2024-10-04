using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class Survey : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public string SurveyId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? SurveyLanguage { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public virtual ICollection<SurveyBlock>? SurveyBlocks { get; set; }
}