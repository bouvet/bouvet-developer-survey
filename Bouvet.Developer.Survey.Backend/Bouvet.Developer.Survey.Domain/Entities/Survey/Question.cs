using System.ComponentModel.DataAnnotations;
using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class Question : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public Guid BlockElementId { get; set; }
    public virtual BlockElement BlockElement { get; set; } = null!;
    
    [MaxLength(250)]
    public string SurveyId { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    
    [MaxLength(250)]
    public string DateExportTag { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public bool IsMultipleChoice { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public virtual AiAnalyse? AiAnalyse { get; set; }
    public virtual ICollection<Choice>? Choices { get; set; }
    public virtual ICollection<ResponseUser>? ResponseUsers { get; set; }
}