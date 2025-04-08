namespace Bouvet.Developer.Survey.Domain.Entities.Bouvet;
public class BouvetOption
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public BouvetQuestion Question { get; set; }

    public string Value { get; set; }
    public string Identifier { get; set; }
}
