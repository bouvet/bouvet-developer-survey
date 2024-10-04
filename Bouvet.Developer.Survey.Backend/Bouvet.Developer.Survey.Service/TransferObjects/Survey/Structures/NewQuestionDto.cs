namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class NewQuestionDto
{
    public string BlockElementId { get; set; } = null!;
    public string SurveyId { get; set; } = null!;
    public string DateExportTag { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
}