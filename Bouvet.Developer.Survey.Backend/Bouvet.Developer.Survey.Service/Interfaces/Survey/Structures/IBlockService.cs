using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface IBlockService
{
    /// <summary>
    /// Create a new block
    /// </summary>
    /// <param name="newBlockDto">The new block DTO</param>
    /// <returns>The created block DTO</returns>
    public Task<BlockDto> CreateBlock(NewBlockDto newBlockDto);
    
    /// <summary>
    /// Get block by GUID
    /// </summary>
    /// <param name="blockId">The block GUID</param>
    /// <returns>Block DTO</returns>
    public Task<BlockDto> GetBlock(Guid blockId);
    
    /// <summary>
    /// Get all blocks in a survey
    /// </summary>
    /// <param name="surveyId">The survey GUID</param>
    /// <returns>A list of available blocks</returns>
    public Task<IEnumerable<BlockDto>> GetBlocks(Guid surveyId);
    
    /// <summary>
    /// Update block
    /// </summary>
    /// <param name="blockId">The block GUID</param>
    /// <param name="updateBlockDto">The block DTO</param>
    /// <returns>Block DTO</returns>
    public Task<BlockDto> UpdateBlock(Guid blockId, NewBlockDto updateBlockDto);
    
    /// <summary>
    /// Set block to deleted
    /// </summary>
    /// <param name="blockId">The block GUID</param>
    /// <returns>Task</returns>
    public Task DeleteBlock(Guid blockId);
}