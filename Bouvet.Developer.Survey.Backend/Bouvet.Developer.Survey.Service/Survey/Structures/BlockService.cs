using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class BlockService : IBlockService
{
    public Task<BlockDto> CreateBlock(NewBlockDto newBlockDto)
    {
        throw new NotImplementedException();
    }

    public Task<BlockDto> GetBlock(Guid blockId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<BlockDto>> GetBlocks(Guid surveyId)
    {
        throw new NotImplementedException();
    }

    public Task<BlockDto> UpdateBlock(Guid blockId, NewBlockDto updateBlockDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBlock(Guid blockId)
    {
        throw new NotImplementedException();
    }
}