using System.ComponentModel.DataAnnotations;
using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class SurveyBlock : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public Guid SurveyGuid { get; set; }
    public virtual Survey Survey { get; set; } = null!;
    
    [MaxLength(100)]
    public string? Type { get; set; }
    public string? Description { get; set; } = string.Empty;
    
    [MaxLength(250)]
    public string SurveyBlockId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public virtual ICollection<BlockElement>? BlockElements { get; set; }
}