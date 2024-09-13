using Bouvet.Developer.Survey.Service.TransferObjects.Survey;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey;

public interface IOptionService
{
    public Task<OptionDto> CreateOptionAsync(NewOptionDto newOptionDto);
    public Task<BlockOptionListDto> GetOptionsToBlockAsync(Guid blockId);
    public Task<OptionDto> GetOptionAsync(Guid optionId);
    public Task<OptionDto> UpdateOptionAsync(Guid optionId, NewOptionDto newOptionDto);
    public Task DeleteOptionAsync(Guid optionId);
}