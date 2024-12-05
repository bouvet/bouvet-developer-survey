using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Interfaces;

namespace Bouvet.Developer.Survey.Domain.Entities.Results;

public class Response
{
    public Guid Id { get; set; }
    public Guid ChoiceId { get; set; }
    public virtual Choice Choice { get; set; } = null!;
    public string FieldName { get; set; } = null!;
    public int Value { get; set; }
    public bool HasWorkedWith { get; set; }
    public bool WantsToWorkWith { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    public virtual ICollection<ResponseUser>? ResponseUsers { get; set; }
}