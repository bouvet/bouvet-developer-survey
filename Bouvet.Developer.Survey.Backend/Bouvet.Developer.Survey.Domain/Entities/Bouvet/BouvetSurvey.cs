namespace Bouvet.Developer.Survey.Domain.Entities.Bouvet;

public class BouvetSurvey
{
    public int Id { get; set; }
    public string ExternalId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int Year { get; set; }

    public ICollection<BouvetSection> Sections { get; set; } = new List<BouvetSection>();

    public ICollection<BouvetQuestion> Questions { get; set; } = new List<BouvetQuestion>();
}
