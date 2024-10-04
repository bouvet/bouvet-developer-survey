using Bouvet.Developer.Survey.Service.TransferObjects.Import;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey;

public interface ISurveyService
{
   
    public Task<SurveyListDto> CreateSurveyAsync(NewSurveyDto newSurveyDto);
    public Task<IEnumerable<SurveyListDto>> GetSurveysAsync();
    public Task<TransferObjects.Survey.SurveyDto> GetSurveyAsync(Guid surveyId);
    public Task<TransferObjects.Survey.SurveyDto> UpdateSurveyAsync(Guid surveyId, NewSurveyDto newSurveyDto);
    public Task DeleteSurveyAsync(Guid surveyId);
}