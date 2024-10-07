using System.ComponentModel.DataAnnotations;
using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class BlockElement : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public Guid SurveyElementGuid { get; set; }
    public virtual SurveyBlock SurveyBlock { get; set; } = null!;
    
    [MaxLength(250)]
    public string Type { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public virtual ICollection<Question>? Questions { get; set; }
}