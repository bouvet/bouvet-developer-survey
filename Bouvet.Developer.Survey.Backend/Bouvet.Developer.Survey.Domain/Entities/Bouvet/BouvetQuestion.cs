namespace Bouvet.Developer.Survey.Domain.Entities.Bouvet;

public class BouvetQuestion
{
    public int Id { get; set; }
    public string ExternalId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Type { get; set; } = default!;
    public string Description { get; set; } = default!;

    public int SurveyId { get; set; }
    public BouvetSurvey Survey { get; set; } = default!;

    public int? SectionId { get; set; }
    public BouvetSection? Section { get; set; }

    public ICollection<BouvetOption> Options { get; set; } = new List<BouvetOption>();
    public ICollection<BouvetResponse> Responses { get; set; } = new List<BouvetResponse>();
}



