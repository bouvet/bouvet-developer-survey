using Bouvet.Developer.Survey.Domain.Entities.Bouvet;

public class BouvetOption
{
    public int Id { get; set; }
    public string ExternalId { get; set; } = default!;
    public string Value { get; set; } = default!;

    public int QuestionId { get; set; }
    public BouvetQuestion Question { get; set; } = default!;
}



