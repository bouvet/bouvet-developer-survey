using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class Choice : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string IndexId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public virtual ICollection<Response>? Responses { get; set; }
}