namespace Bouvet.Developer.Survey.Domain.Entities;

public class AiAnalyse
{
  public Guid Id { get; set; }
  public string Text { get; set; } = null!;
  public Guid BlockId { get; set; }
  public Block Block { get; set; } = null!;
  public DateTimeOffset CreatedAt { get; set; }
  public DateTimeOffset? UpdatedAt { get; set; }
  public DateTimeOffset? DeletedAt { get; set; }
}