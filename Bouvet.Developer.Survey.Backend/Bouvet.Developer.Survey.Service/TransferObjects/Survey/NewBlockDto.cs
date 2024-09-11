namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class NewBlockDto
{
    public string? Question { get; set; }
    public string? Type { get; set; }
    public Guid SurveyId { get; set; }
}