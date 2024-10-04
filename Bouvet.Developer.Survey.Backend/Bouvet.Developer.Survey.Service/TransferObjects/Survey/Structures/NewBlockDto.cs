namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class NewBlockDto
{
    public Guid BlockId { get; set; }
    public string Type { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
}