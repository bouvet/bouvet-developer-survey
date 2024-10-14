using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface ISurveyService
{
   /// <summary>
   /// Create a new survey
   /// </summary>
   /// <param name="newSurveyDto">The new survey DTO</param>
   /// <returns>The created survey DTO</returns>
    public Task<SurveyDto> CreateSurveyAsync(NewSurveyDto newSurveyDto);
   
   /// <summary>
   ///  Get all surveys
   /// </summary>
   /// <returns>A list of available survey</returns>
    public Task<IEnumerable<SurveysDto>> GetSurveysAsync();
   
   /// <summary>
   ///  Get survey by GUID
   /// </summary>
   /// <param name="surveyId">The survey GUID</param>
   /// <returns>Survey DTO</returns>
    public Task<SurveyDto> GetSurveyAsync(Guid surveyId);
   
   /// <summary>
   /// Update survey
   /// </summary>
   /// <param name="surveyId">The survey GUID</param>
   /// <param name="updateSurveyDto">The survey DTO</param>
   /// <returns>Survey DTO</returns>
    public Task<SurveyDto> UpdateSurveyAsync(Guid surveyId, NewSurveyDto updateSurveyDto);
   
   /// <summary>
   /// Set survey do deleted
   /// </summary>
   /// <param name="surveyId">The survey GUID</param>
    public Task DeleteSurveyAsync(Guid surveyId);
}