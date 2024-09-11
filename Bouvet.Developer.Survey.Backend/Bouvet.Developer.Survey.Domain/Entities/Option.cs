namespace Bouvet.Developer.Survey.Domain.Entities;

public class Option
{
    public Guid Id { get; set; }
    public string Value { get; set; } = null!;
    public Guid BlockId { get; set; }
    public virtual Block Block { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}