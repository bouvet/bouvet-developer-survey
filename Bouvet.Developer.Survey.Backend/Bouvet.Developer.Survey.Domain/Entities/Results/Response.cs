using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Results;

public class Response : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public Guid ChoiceId { get; set; }
    public virtual Choice Choice { get; set; } = null!;
    public string FieldName { get; set; } = null!;
    public int Value { get; set; } 
    public Guid? AnswerOptionId { get; set; }
    public virtual AnswerOption? AnswerOption { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}