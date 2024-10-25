
namespace Bouvet.Developer.Survey.Domain.Entities.Results;

public class User
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public virtual Survey.Survey Survey { get; set; } = null!;
    public string RespondId { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    public virtual ICollection<ResponseUser>? ResponseUsers { get; set; }
}