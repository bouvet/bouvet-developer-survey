
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface IAnswerOptionService
{
    /// <summary>
    /// Create a new answer option
    /// </summary>
    /// <param name="newAnswerOptionDto">The new answer option dto</param>
    /// <returns>Answer option dto</returns>
    Task<AnswerOptionDto> CreateAnswerOption(NewAnswerOptionDto newAnswerOptionDto);
    
    /// <summary>
    /// Create a new answer option
    /// </summary>
    /// <param name="surveyId">The survey id</param>
    /// <param name="questionDto">The question dto</param>
    /// <returns></returns>
    Task AddAnswerFromDto(Guid surveyId, PayloadQuestionDto questionDto);
    
    /// <summary>
    /// Update answer option
    /// </summary>
    /// <param name="answerOptionId">Guid of the answer option</param>
    /// <param name="newAnswerOptionDto">The updated answer option dto</param>
    /// <returns>Answer option dto</returns>
    Task<AnswerOptionDto> UpdateAnswerOption(Guid answerOptionId, NewAnswerOptionDto newAnswerOptionDto);
    
    /// <summary>
    /// Set answer option to deleted
    /// </summary>
    /// <param name="answerOptionId">The answer option id</param>
    /// <returns>Task</returns>
    Task DeleteAnswerOption(Guid answerOptionId);
}