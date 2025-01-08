namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response
{
    public class AnswerDto
    {
        public GetStatsDto Stats { get; set; } = null!;
        public int NumberOfRespondents { get; set; }
        public IEnumerable<GetResponseDto> Responses { get; set; } = null!;
    }
}
