using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface ISurveyService
{
   /// <summary>
   /// Create a new survey
   /// </summary>
   /// <param name="newSurveyDto">The new survey DTO</param>
   /// <returns>The created survey DTO</returns>
   Task<SurveyDto> CreateSurveyAsync(NewSurveyDto newSurveyDto);

   /// <summary>
   ///  Get all surveys
   /// </summary>
   /// <returns>A list of available survey</returns>
   Task<IEnumerable<SurveysDto>> GetSurveysAsync();

   /// <summary>
   ///  Get survey by GUID
   /// </summary>
   /// <param name="surveyId">The survey GUID</param>
   /// <returns>Survey DTO</returns>
   Task<SurveyDto> GetSurveyAsync(Guid surveyId);

   /// <summary>
   ///  Get survey by year
   /// </summary>
   /// <param name="year">The survey year</param>
   /// <returns>Survey DTO</returns>
   Task<SurveyDto> GetSurveyByYearAsync(int year);

   /// <summary>
   /// Update survey
   /// </summary>
   /// <param name="surveyId">The survey GUID</param>
   /// <param name="updateSurveyDto">The survey DTO</param>
   /// <returns>Survey DTO</returns>
   Task<SurveyDto> UpdateSurveyAsync(Guid surveyId, NewSurveyDto updateSurveyDto);

   /// <summary>
   /// Set survey do deleted
   /// </summary>
   /// <param name="surveyId">The survey GUID</param>
   Task DeleteSurveyAsync(Guid surveyId);
}
