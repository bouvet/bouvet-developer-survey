namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class NewAnswerOptionDto
{
    public Guid SurveyId { get; set; }
    public string Text { get; set; } = null!;
    public string IndexId { get; set; } = null!;
}