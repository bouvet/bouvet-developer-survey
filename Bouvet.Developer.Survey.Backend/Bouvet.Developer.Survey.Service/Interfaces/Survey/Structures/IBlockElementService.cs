using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface IBlockElementService
{
    /// <summary>
    /// Create a new block
    /// </summary>
    /// <param name="newBlockElementDtos">List of block elements to create</param>
    /// <returns>List of created block elements</returns>
    Task<List<BlockElementDto>> CreateBlockElements(List<NewBlockElementDto> newBlockElementDtos);
    
    /// <summary>
    /// Get block by GUID
    /// </summary>
    /// <param name="blockElementId">The block GUID</param>
    /// <returns>Block DTO</returns>
    Task<BlockElementDto> GetBlockElementById(Guid blockElementId);
    
    /// <summary>
    /// Get all blocks in a survey
    /// </summary>
    /// <param name="blockId">The survey GUID</param>
    /// <returns>A list of available blocks</returns>
    Task<IEnumerable<BlockElementDto>> GetBlockElementsByBlockId(Guid blockId);
    
    /// <summary>
    /// Update block
    /// </summary>
    /// <param name="blockElementId">The block GUID</param>
    /// <param name="updateBlockElementDto">The block DTO</param>
    /// <returns>Block DTO</returns>
    Task<BlockElementDto> UpdateBlockElement(Guid blockElementId, NewBlockElementDto updateBlockElementDto);
    
    /// <summary>
    /// Set block to deleted
    /// </summary>
    /// <param name="blockId">The block GUID</param>
    /// <returns>Task</returns>
    Task DeleteBlockElement(Guid blockId);

    /// <summary>
    /// Check if block elements are valid
    /// </summary>
    /// <param name="surveyGuid">GUID of the survey</param>
    /// <param name="block">Survey block DTO</param>
    /// <returns>Task</returns>
    Task CheckBlockElements(Guid surveyGuid, SurveyElementBlockDto block);
}