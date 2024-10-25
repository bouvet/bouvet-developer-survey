namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;

public class NewUserDto
{
    public Guid SurveyId { get; set; }
    public string RespondId { get; set; } = null!;
}