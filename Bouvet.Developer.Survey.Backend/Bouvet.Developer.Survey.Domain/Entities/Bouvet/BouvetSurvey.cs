
    namespace Bouvet.Developer.Survey.Domain.Entities.Bouvet;
    public class BouvetSurvey
{
    public int Id { get; set; }
    public int Year { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<BouvetQuestion> Questions { get; set; }
}