using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface IResultService
{
    /// <summary>
    /// Summarize fields for a survey
    /// </summary>
    /// <param name="fields">List of fields</param>
    /// <param name="questions">List of questions</param>
    /// <param name="survey">The survey</param>
    /// <returns>List of summarized fields</returns>
    Task<List<NewResponseDto>> SummarizeFields(List<FieldDto> fields, IEnumerable<QuestionDetailsDto> questions,
        Domain.Entities.Survey.Survey survey);
    
    /// <summary>
    /// Get all questions for a survey
    /// </summary>
    /// <param name="surveyId">Survey ID</param>
    /// <returns>Returns questions ExportTag</returns>
    Task<IEnumerable<ExportTagDto>> GetQuestions(string surveyId);
}