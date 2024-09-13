namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class NewOptionDto
{
    public string Value { get; set; } = null!;
    public Guid BlockId { get; set; }
}