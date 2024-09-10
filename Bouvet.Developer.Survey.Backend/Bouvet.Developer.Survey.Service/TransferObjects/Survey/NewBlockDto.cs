namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class NewBlockDto
{
    public string Question { get; set; } = null!;
    public string Type { get; set; } = null!;
    public Guid SurveyId { get; set; }
}