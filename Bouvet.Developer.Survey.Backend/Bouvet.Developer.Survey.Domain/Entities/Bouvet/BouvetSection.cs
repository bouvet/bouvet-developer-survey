using Bouvet.Developer.Survey.Domain.Entities.Bouvet;

public class BouvetSection
{
    public int Id { get; set; }

    public string ExternalId { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public int SurveyId { get; set; }
    public BouvetSurvey Survey { get; set; } = default!;
}

