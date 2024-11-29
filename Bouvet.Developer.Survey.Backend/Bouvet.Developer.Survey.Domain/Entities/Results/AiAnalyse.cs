using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Domain.Entities.Results;

public class AiAnalyse
{
  public Guid Id { get; set; }
  public string Text { get; set; } = null!;
  public Guid QuestionId { get; set; }
  public virtual Question Question { get; set; } = null!;
  public DateTimeOffset CreatedAt { get; set; }
  public DateTimeOffset? UpdatedAt { get; set; }
  public DateTimeOffset? DeletedAt { get; set; }
}