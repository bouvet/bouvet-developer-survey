namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Bouvet
{
    public class QuestionResultDto
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Type { get; set; } = ""; // "single-choice", "multiple-choice", or "likert"
        public List<OptionResultDto> Options { get; set; } = new();
        public LikertStatsDto? LikertStats { get; set; } // for dumbbell graph only if likert
    }

}
