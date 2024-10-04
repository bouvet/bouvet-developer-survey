using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey;

public class SurveyService : ISurveyService
{
    private readonly DeveloperSurveyContext _context;
    
    public SurveyService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    
    public async Task<SurveyListDto> CreateSurveyAsync(NewSurveyDto newSurveyDto)
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
        
        var dto = SurveyListDto.CreateFromEntity(survey);
        
        return dto;
    }

    public async Task<IEnumerable<SurveyListDto>> GetSurveysAsync()
    {
        var surveys = await _context.Surveys.ToListAsync();

        var surveyList = surveys.Select(SurveyListDto.CreateFromEntity).ToList();
        
        return surveyList;
    }

    public async Task<TransferObjects.Survey.SurveyDto> GetSurveyAsync(Guid surveyId)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);

        if (survey == null) throw new NotFoundException("Survey not found");
        

        var surveyDto = TransferObjects.Survey.SurveyDto.CreateFromEntity(survey);

        return surveyDto;
    }

    public async Task<TransferObjects.Survey.SurveyDto> UpdateSurveyAsync(Guid surveyId, NewSurveyDto newSurveyDto)
    {
        var surveyToBeUpdated = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);
            
        if (surveyToBeUpdated == null) throw new NotFoundException("Survey not found");
            
        surveyToBeUpdated.Name = newSurveyDto.Name;
        surveyToBeUpdated.SurveyId = newSurveyDto.SurveyId;
        surveyToBeUpdated.SurveyUrl = newSurveyDto.SurveyUrl;
        surveyToBeUpdated.UpdatedAt = DateTimeOffset.Now;
            
        _context.Surveys.Update(surveyToBeUpdated);
        await _context.SaveChangesAsync();
            
        var dto = TransferObjects.Survey.SurveyDto.CreateFromEntity(surveyToBeUpdated);
            
        return dto;
    }

    public async Task DeleteSurveyAsync(Guid surveyId)
    {
        var surveyToBeDeleted = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);
        
        if (surveyToBeDeleted == null) throw new NotFoundException("Survey not found");
        
        surveyToBeDeleted.DeletedAt = DateTimeOffset.Now;
        _context.Surveys.Update(surveyToBeDeleted);
        await _context.SaveChangesAsync();
        
    }
}