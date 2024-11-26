using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

namespace Bouvet.Developer.Survey.Service.Interfaces.Import;

public interface IImportSurveyService
{
    /// <summary>
    /// Upload a survey from a stream of JSON
    /// </summary>
    /// <param name="stream">The stream to upload</param>
    /// <returns>The survey dto</returns>
    Task<SurveyBlocksDto> UploadSurvey(Stream stream);
    
    /// <summary>
    /// Find survey blocks
    /// </summary>
    /// <param name="surveyDto">The survey dto</param>
    /// <returns>The survey dto</returns>
    Task<SurveyBlocksDto> FindSurveyBlocks(SurveyBlocksDto surveyDto);
    
    /// <summary>
    /// Find survey questions
    /// </summary>
    /// <param name="surveyQuestionsDto">The survey questions dto</param>
    /// <returns>Task</returns>
    Task FindSurveyQuestions(SurveyQuestionsDto surveyQuestionsDto);
    
    /// <summary>
    /// Get questions from a stream of CSV
    /// </summary>
    /// <param name="csvStream">The stream to upload</param>
    /// <param name="surveyId">The survey id</param>
    /// <returns>Task</returns>
    Task GetQuestionsFromStream(Stream csvStream, Guid surveyId);
    
    /// <summary>
    /// Map fields to response
    /// </summary>
    /// <param name="fieldDto">The list of fields</param>
    /// <param name="surveyId">The survey id</param>
    /// <returns></returns>
    Task MapFieldsToResponse(List<FieldDto> fieldDto, string surveyId);
}