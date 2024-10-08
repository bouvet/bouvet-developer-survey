using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface IChoiceService
{
    /// <summary>
    ///  Create a new choice
    /// </summary>
    /// <param name="newChoiceDto">List of new choice DTOs</param>
    /// <param name="questionId">Guid of the question</param>
    /// <returns>List of choice DTOs</returns>
    public Task<List<ChoiceDto>> CreateChoice(List<NewChoiceDto> newChoiceDto, Guid questionId);
    
    /// <summary>
    /// Get choice by GUID
    /// </summary>
    /// <param name="choiceId">Guid of the choice</param>
    /// <returns>Choice DTO</returns>
    public Task<ChoiceDto> GetChoice(Guid choiceId);
    
    /// <summary>
    /// Get all choices in a question
    /// </summary>
    /// <param name="questionId">Guid of the question</param>
    /// <returns>A list of available choices</returns>
    public Task<IEnumerable<ChoiceDto>> GetChoices(Guid questionId);
    
    /// <summary>
    /// Update choice
    /// </summary>
    /// <param name="choiceId">Guid of the choice</param>
    /// <param name="updateChoiceDto">The updated choice DTO</param>
    /// <returns>Choice DTO</returns>
    public Task<ChoiceDto> UpdateChoice(Guid choiceId, NewChoiceDto updateChoiceDto);
    
    /// <summary>
    /// Set choice to deleted
    /// </summary>
    /// <param name="choiceId">The choice GUID</param>
    /// <returns>Task</returns>
    public Task DeleteChoice(Guid choiceId);
}