namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class NewSurveyDto
{
    public string SurveyId { get; set; } = null!;
    public int? Year { get; set; }
    public string Name { get; set; } = null!;
    public string Language { get; set; } = null!;
}
