using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;

namespace Bouvet.Developer.Survey.Service.Survey;

public class OptionService : IOptionService
{
    public Task<OptionDto> CreateOptionAsync(NewOptionDto newOptionDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OptionDto>> GetOptionsAsync(Guid blockId)
    {
        throw new NotImplementedException();
    }

    public Task<OptionDto> GetOptionAsync(Guid optionId)
    {
        throw new NotImplementedException();
    }

    public Task<OptionDto> UpdateOptionAsync(Guid optionId, NewOptionDto newOptionDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOptionAsync(Guid optionId)
    {
        throw new NotImplementedException();
    }
}