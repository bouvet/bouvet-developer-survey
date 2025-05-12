using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Bouvet;


namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results.Bouvet
{
    public interface ISurveyResultsService
    {
        Task<SurveyResultsDto> GetSurveyResultsByYearAsync(int surveyId);
    }
}
