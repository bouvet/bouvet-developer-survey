using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class SurveyService : ISurveyService
{
    private readonly DeveloperSurveyContext _context;

    public SurveyService(DeveloperSurveyContext context)
    {
        _context = context;
    }

    public async Task<SurveyDto> CreateSurveyAsync(NewSurveyDto newSurveyDto)
    {
        var survey = new Domain.Entities.Survey.Survey
        {
            Id = Guid.NewGuid(),
            Name = newSurveyDto.Name,
            SurveyId = newSurveyDto.SurveyId,
            SurveyLanguage = newSurveyDto.Language,
            CreatedAt = DateTimeOffset.Now
        };

        await _context.Surveys.AddAsync(survey);
        await _context.SaveChangesAsync();

        var dto = SurveyDto.CreateFromEntity(survey);

        return dto;
    }

    public async Task<IEnumerable<SurveysDto>> GetSurveysAsync()
    {
        var surveys = await _context.Surveys.ToListAsync();

        var surveyList = surveys.Select(SurveysDto.CreateFromEntity).ToList();

        return surveyList;
    }

    public async Task<SurveyDto> GetSurveyAsync(Guid surveyId)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);

        if (survey == null) throw new NotFoundException("Survey not found");


        var surveyDto = SurveyDto.CreateFromEntity(survey);

        return surveyDto;
    }

    public async Task<SurveyDto> UpdateSurveyAsync(Guid surveyId, NewSurveyDto newSurveyDto)
    {
        var surveyToBeUpdated = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);

        if (surveyToBeUpdated == null) throw new NotFoundException("Survey not found");

        surveyToBeUpdated.Name = newSurveyDto.Name;
        surveyToBeUpdated.UpdatedAt = DateTimeOffset.Now;

        _context.Surveys.Update(surveyToBeUpdated);
        await _context.SaveChangesAsync();

        var dto = SurveyDto.CreateFromEntity(surveyToBeUpdated);

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
