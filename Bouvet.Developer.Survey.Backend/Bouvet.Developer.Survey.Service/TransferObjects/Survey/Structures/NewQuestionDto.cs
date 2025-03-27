namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class NewQuestionDto
{
    public string BlockElementId { get; set; } = null!;
    public string SurveyId { get; set; } = null!;
    public string DataExportTag { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public bool IsMultipleChoice { get; set; }
    public string QuestionDescription { get; set; } = null!;
    
    public List<NewChoiceDto>? Choices { get; set; }
}