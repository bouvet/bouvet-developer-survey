
using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Domain.Entities.Results;

public class ResponseUser
{
    public Guid ResponseId { get; set; }
    public virtual Response Response { get; set; } = null!;
    public Guid QuestionId { get; set; }
    public virtual Question Question { get; set; } = null!;
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}