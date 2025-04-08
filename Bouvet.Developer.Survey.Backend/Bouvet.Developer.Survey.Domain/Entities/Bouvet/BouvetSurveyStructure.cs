namespace Bouvet.Developer.Survey.Domain.Entities.Bouvet
{
    public class BouvetSurveyStructure
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string StructureJson { get; set; } = string.Empty;
    }
}
