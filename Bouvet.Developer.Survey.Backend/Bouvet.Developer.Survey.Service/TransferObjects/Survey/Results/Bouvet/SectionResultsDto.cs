namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Bouvet
{
    public class SectionResultDto
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public List<QuestionResultDto> Questions { get; set; } = new();
    }
}
