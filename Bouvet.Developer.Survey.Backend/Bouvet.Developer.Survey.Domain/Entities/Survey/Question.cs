using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class Question : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public string BlockElementId { get; set; } = null!;
    public virtual BlockElement BlockElement { get; set; } = null!;
    public string SurveyId { get; set; } = null!;
    public string DateExportTag { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    public virtual ICollection<Choice>? Choices { get; set; }
}