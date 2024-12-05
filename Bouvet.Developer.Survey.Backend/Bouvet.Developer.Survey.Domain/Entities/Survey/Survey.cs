using System.ComponentModel.DataAnnotations;
using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class Survey : IEntitiesDateTimeOffsett, ILastSyncedOffset
{
    public Guid Id { get; set; }
    
    [MaxLength(250)]
    public string SurveyId { get; set; } = null!;
    
    [MaxLength(250)]
    public string Name { get; set; } = null!;
    
    [MaxLength(100)]
    public string? SurveyLanguage { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public virtual ICollection<SurveyBlock>? SurveyBlocks { get; set; }
    public virtual ICollection<User>? Users { get; set; }
}