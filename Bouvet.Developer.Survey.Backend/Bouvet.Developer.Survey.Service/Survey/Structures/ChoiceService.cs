using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class ChoiceService : IChoiceService
{
    public Task<ChoiceDto> CreateChoice(NewChoiceDto newChoiceDto)
    {
        throw new NotImplementedException();
    }

    public Task<ChoiceDto> GetChoice(Guid choiceId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ChoiceDto>> GetChoices(Guid questionId)
    {
        throw new NotImplementedException();
    }

    public Task<ChoiceDto> UpdateChoice(Guid choiceId, NewChoiceDto updateChoiceDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteChoice(Guid choiceId)
    {
        throw new NotImplementedException();
    }
}