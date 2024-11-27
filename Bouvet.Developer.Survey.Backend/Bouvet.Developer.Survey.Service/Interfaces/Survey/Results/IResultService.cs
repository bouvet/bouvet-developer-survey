using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface IResultService
{
    /// <summary>
    /// Check for differences from response csv
    /// </summary>
    /// <param name="fieldDto">List of fields</param>
    /// <param name="questions">List of questions</param>
    /// <param name="survey">The survey</param>
    /// <returns>Task</returns>
    Task CheckForDifferences(List<FieldDto> fieldDto, List<QuestionDetailsDto> questions,
        Domain.Entities.Survey.Survey survey);
    
    /// <summary>
    /// Get all questions for a survey
    /// </summary>
    /// <param name="surveyId">Survey ID</param>
    /// <returns>Returns questions ExportTag</returns>
    Task<IEnumerable<ExportTagDto>> GetQuestions(string surveyId);
}