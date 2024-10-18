using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Survey;

public class AnswerOption : IEntitiesDateTimeOffsett
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public virtual Survey Survey { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string IndexId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public virtual ICollection<Response>? Responses { get; set; }
}