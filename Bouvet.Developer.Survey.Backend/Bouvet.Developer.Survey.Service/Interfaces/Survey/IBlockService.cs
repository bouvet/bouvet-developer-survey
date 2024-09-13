using Bouvet.Developer.Survey.Service.TransferObjects.Survey;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey;

public interface IBlockService
{
    public Task<BlockDto> CreateBlockAsync(NewBlockDto newBlockDto);
    public Task<SurveyDto> GetBlocksToSurveyAsync(Guid surveyId);
    public Task<BlockDto> GetBlockAsync(Guid blockId);
    public Task<BlockDto> UpdateBlockAsync(Guid blockId, NewBlockDto newBlockDto);
    public Task DeleteBlockAsync(Guid blockId);
}