namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;

public class NewResponseUserDto
{
    public Guid ResponseId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid UserId { get; set; }
}