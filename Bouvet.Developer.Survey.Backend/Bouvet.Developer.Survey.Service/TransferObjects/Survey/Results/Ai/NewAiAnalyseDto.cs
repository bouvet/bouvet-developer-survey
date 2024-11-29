namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Ai;

public class NewAiAnalyseDto
{
    public string Text { get; set; } = null!;
    public Guid QuestionId { get; set; }
}