using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;

namespace Bouvet.Developer.Survey.Service.Survey;

public class SurveyService : ISurveyService
{
    private readonly DeveloperSurveyContext _context;
    
    public SurveyService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    
    public async Task<SurveyDto> CreateSurveyAsync(NewSurveyDto newSurveyDto)
    {
        var survey = new Domain.Entities.Survey
        {
            Id = Guid.NewGuid(),
            Name = newSurveyDto.Name,
            SurveyId = newSurveyDto.SurveyId,
            SurveyUrl = newSurveyDto.SurveyUrl,
            CreatedAt = DateTimeOffset.Now
        };
        
        await _context.Surveys.AddAsync(survey);
        await _context.SaveChangesAsync();
        
        
        var surveyDto = new SurveyDto
        {
            Name = survey.Name,
            CreatedAt = survey.CreatedAt,
            UpdatedAt = survey.UpdatedAt,
            LastSyncedAt = survey.LastSyncedAt
        };
        
        return surveyDto;
    }

    public Task<IEnumerable<SurveyDto>> GetSurveysAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SurveyDto> GetSurveyAsync(Guid surveyId)
    {
        throw new NotImplementedException();
    }

    public Task<SurveyDto> UpdateSurveyAsync(Guid surveyId, NewSurveyDto newSurveyDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSurveyAsync(Guid surveyId)
    {
        throw new NotImplementedException();
    }
}