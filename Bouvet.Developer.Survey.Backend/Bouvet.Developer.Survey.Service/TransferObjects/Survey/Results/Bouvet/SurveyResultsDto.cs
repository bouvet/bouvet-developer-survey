namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Bouvet
{
    public class SurveyResultsDto
    {
        public string SurveyId { get; set; } = "";
        public string Title { get; set; } = "";
        public List<SectionResultDto> Sections { get; set; } = new();
        public List<QuestionResultDto> StandaloneQuestions { get; set; } = new(); // for questions without section
    }

}
