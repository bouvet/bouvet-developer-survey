namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;

public class UserDto
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public string RespondId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    
    public static UserDto CreateFromEntity(Domain.Entities.Results.User user)
    {
        return new UserDto
        {
            Id = user.Id,
            SurveyId = user.SurveyId,
            RespondId = user.RespondId,
            CreatedAt = user.CreatedAt
        };
    }
    
}