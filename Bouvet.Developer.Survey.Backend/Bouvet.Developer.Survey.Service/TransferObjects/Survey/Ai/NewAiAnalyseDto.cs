namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Ai;

public class NewAiAnalyseDto
{
    public string Text { get; set; } = null!;
    public Guid QuestionId { get; set; }
}