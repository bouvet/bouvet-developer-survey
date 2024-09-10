using Bouvet.Developer.Survey.Service.TransferObjects.Survey;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey;

public interface ISurveyService
{
    public Task<SurveyDto> CreateSurveyAsync(NewSurveyDto newSurveyDto);
    public Task<IEnumerable<SurveyDto>> GetSurveysAsync();
    public Task<SurveyDto> GetSurveyAsync(Guid surveyId);
    public Task<SurveyDto> UpdateSurveyAsync(Guid surveyId, NewSurveyDto newSurveyDto);
    public Task DeleteSurveyAsync(Guid surveyId);
}