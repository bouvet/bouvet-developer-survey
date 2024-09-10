using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;

namespace Bouvet.Developer.Survey.Service.Survey;

public class BlockService : IBlockService
{
    public Task<BlockDto> CreateBlockAsync(NewBlockDto newBlockDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BlockDto>> GetBlocksAsync(Guid surveyId)
    {
        throw new NotImplementedException();
    }

    public Task<BlockDto> GetBlockAsync(Guid blockId)
    {
        throw new NotImplementedException();
    }

    public Task<BlockDto> UpdateBlockAsync(Guid blockId, NewBlockDto newBlockDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBlockAsync(Guid blockId)
    {
        throw new NotImplementedException();
    }
}