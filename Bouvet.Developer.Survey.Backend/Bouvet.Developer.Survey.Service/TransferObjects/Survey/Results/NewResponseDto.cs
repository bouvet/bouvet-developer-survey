namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

public class NewResponseDto
{
    public int Value { get; set; }
    public string FieldName { get; set; } = null!;
    public string? FieldValue { get; set; }
    public bool HasWorkedWith { get; set; }
    public bool WantsToWorkWith { get; set; }
    public Guid ChoiceId { get; set; }
    public Guid? AnswerOptionId { get; set; }
}