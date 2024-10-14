using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface ISurveyBlockService
{
    /// <summary>
    ///   Create a new survey block
    /// </summary>
    /// <param name="newSurveyBlockDto">The new survey block DTO</param>
    /// <returns>The created survey block DTO</returns>
    Task<SurveyElementDto> CreateSurveyBlock(NewSurveyBlockDto newSurveyBlockDto);
    
    /// <summary>
    /// Check if block elements are valid
    /// </summary>
    /// <param name="surveyGuid">GUID of the survey</param>
    /// <param name="block">Survey block DTO</param>
    /// <returns>Task</returns>
    Task CheckSurveyBlockElements(Guid surveyGuid, SurveyElementBlockDto block);

    /// <summary>
    ///  Get survey block by GUID
    /// </summary>
    /// <param name="surveyId">The survey GUID</param>
    /// <returns>Survey block DTO</returns>
    Task<IEnumerable<SurveyElementDto>> GetSurveyBlocks(Guid surveyId);

    /// <summary>
    /// Get survey block by GUID
    /// </summary>
    /// <param name="surveyBlockId">The survey block GUID</param>
    /// <returns>Survey block DTO</returns>
    Task<SurveyElementDto> GetSurveyBlock(Guid surveyBlockId);

    /// <summary>
    /// Update survey block
    /// </summary>
    /// <param name="surveyElementId">The survey block GUID</param>
    /// <param name="updateSurveyBlockDto">The survey block DTO</param>
    /// <returns>Survey block DTO</returns>
    Task<SurveyElementDto> UpdateSurveyElement(Guid surveyElementId, NewSurveyBlockDto updateSurveyBlockDto);

    /// <summary>
    /// Set survey block to deleted
    /// </summary>
    /// <param name="surveyBlockId">The survey block GUID</param>
    /// <returns>Task</returns>
    Task DeleteSurveyBlock(Guid surveyBlockId);
    
   
}