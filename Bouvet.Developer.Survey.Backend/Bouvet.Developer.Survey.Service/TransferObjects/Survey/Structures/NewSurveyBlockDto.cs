namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class NewSurveyBlockDto
{
    public string SurveyId { get; set; } = null!;
    public string? Type { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string SurveyBlockId { get; set; } = null!;
}