namespace Bouvet.Developer.Survey.Domain.Entities.Bouvet;
public class BouvetQuestion
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public BouvetSurvey Survey { get; set; }

    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public ICollection<BouvetOption> Options { get; set; }
}
