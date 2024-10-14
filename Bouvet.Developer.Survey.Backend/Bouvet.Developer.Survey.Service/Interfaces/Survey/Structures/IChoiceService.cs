using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
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
    Task<List<ChoiceDto>> CreateChoice(List<NewChoiceDto> newChoiceDto, Guid questionId);

    /// <summary>
    /// Check for differences in choices
    /// </summary>
    /// <param name="questionId">The question GUID</param>
    /// <param name="questionsDto">The question DTO</param>
    /// <returns>Task</returns>
    Task CheckForDifferences(Guid questionId, PayloadQuestionDto questionsDto);
    
    /// <summary>
    /// Get choice by GUID
    /// </summary>
    /// <param name="choiceId">Guid of the choice</param>
    /// <returns>Choice DTO</returns>
    Task<ChoiceDto> GetChoice(Guid choiceId);
    
    /// <summary>
    /// Get all choices in a question
    /// </summary>
    /// <param name="questionId">Guid of the question</param>
    /// <returns>A list of available choices</returns>
    Task<IEnumerable<ChoiceDto>> GetChoices(Guid questionId);
    
    /// <summary>
    /// Update choice
    /// </summary>
    /// <param name="choiceId">Guid of the choice</param>
    /// <param name="updateChoiceDto">The updated choice DTO</param>
    /// <returns>Choice DTO</returns>
    Task<ChoiceDto> UpdateChoice(Guid choiceId, NewChoiceDto updateChoiceDto);
    
    /// <summary>
    /// Set choice to deleted
    /// </summary>
    /// <param name="choiceId">The choice GUID</param>
    /// <returns>Task</returns>
    Task DeleteChoice(Guid choiceId);
}