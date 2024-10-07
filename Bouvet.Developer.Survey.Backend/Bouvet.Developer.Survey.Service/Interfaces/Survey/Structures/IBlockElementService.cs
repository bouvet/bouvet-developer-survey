using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface IBlockElementService
{
    /// <summary>
    /// Create a new block
    /// </summary>
    /// <param name="newBlockElementDto">The new block DTO</param>
    /// <returns>The created block DTO</returns>
    public Task<BlockElementDto> CreateBlockElement(NewBlockElementDto newBlockElementDto);
    
    /// <summary>
    /// Get block by GUID
    /// </summary>
    /// <param name="blockElementId">The block GUID</param>
    /// <returns>Block DTO</returns>
    public Task<BlockElementDto> GetBlockElementById(Guid blockElementId);
    
    /// <summary>
    /// Get all blocks in a survey
    /// </summary>
    /// <param name="blockId">The survey GUID</param>
    /// <returns>A list of available blocks</returns>
    public Task<IEnumerable<BlockElementDto>> GetBlockElementsByBlockId(Guid blockId);
    
    /// <summary>
    /// Update block
    /// </summary>
    /// <param name="blockElementId">The block GUID</param>
    /// <param name="updateBlockElementDto">The block DTO</param>
    /// <returns>Block DTO</returns>
    public Task<BlockElementDto> UpdateBlockElement(Guid blockElementId, NewBlockElementDto updateBlockElementDto);
    
    /// <summary>
    /// Set block to deleted
    /// </summary>
    /// <param name="blockId">The block GUID</param>
    /// <returns>Task</returns>
    public Task DeleteBlockElement(Guid blockId);
}