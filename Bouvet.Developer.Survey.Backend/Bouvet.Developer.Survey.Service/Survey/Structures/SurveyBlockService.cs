using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class SurveyBlockService : ISurveyBlockService
{
    public Task<SurveyBlockDto> CreateSurveyBlock(NewSurveyBlockDto newSurveyBlockDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SurveyBlockDto>> GetSurveyBlocks(Guid surveyId)
    {
        throw new NotImplementedException();
    }

    public Task<SurveyBlockDto> GetSurveyBlock(Guid surveyBlockId)
    {
        throw new NotImplementedException();
    }

    public Task<SurveyBlockDto> UpdateSurveyElement(Guid surveyElementId, NewSurveyBlockDto updateSurveyBlockDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSurveyBlock(Guid surveyBlockId)
    {
        throw new NotImplementedException();
    }
}