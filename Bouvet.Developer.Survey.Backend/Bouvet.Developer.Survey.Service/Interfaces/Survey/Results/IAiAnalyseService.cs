using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Ai;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface IAiAnalyseService
{
    /// <summary>
    /// Create a new AiAnalyse
    /// </summary>
    /// <param name="aiAnalyseDto">The AiAnalyse to create </param>
    /// <returns>The created AiAnalyse</returns>
    Task<AiAnalyseDto> CreateAiAnalyse(NewAiAnalyseDto aiAnalyseDto);
    
    /// <summary>
    /// Get AiAnalyse by QuestionId
    /// </summary>
    /// <param name="questionId">The QuestionId to get AiAnalyse by</param>
    /// <returns>The AiAnalyse</returns>
    Task<AiAnalyseDto> GetAiAnalysesByQuestionId(Guid questionId);
    
    /// <summary>
    /// Get AiAnalyse by Id
    /// </summary>
    /// <param name="aiAnalyseId">The AiAnalyseId to get AiAnalyse by</param>
    /// <param name="aiAnalyseDto">The AiAnalyse to update</param>
    /// <returns>The updated AiAnalyse</returns>
    Task<AiAnalyseDto> UpdateAiAnalyse(Guid aiAnalyseId, NewAiAnalyseDto aiAnalyseDto);
    
    /// <summary>
    /// Delete AiAnalyse by Id
    /// </summary>
    /// <param name="aiAnalyseId">The AiAnalyseId to delete AiAnalyse by</param>
    /// <returns>Task</returns>
    Task DeleteAiAnalyse(Guid aiAnalyseId);
}