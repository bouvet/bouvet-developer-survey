namespace Bouvet.Developer.Survey.Domain.Entities;

public class Block
{
    public Guid Id { get; set; }
    public string Question { get; set; } = null!;
    public string Type { get; set; } = null!;
    public Guid SurveyId { get; set; }
    public Survey Survey { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public ICollection<Option>? Options { get; set; }
    public AiAnalyse? AiAnalyse { get; set; }
}