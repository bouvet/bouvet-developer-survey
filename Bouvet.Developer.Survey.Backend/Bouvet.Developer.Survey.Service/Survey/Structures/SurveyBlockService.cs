using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class SurveyBlockService : ISurveyBlockService
{
    private readonly DeveloperSurveyContext _context;
    
    public SurveyBlockService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    
    public async Task<SurveyElementDto> CreateSurveyBlock(NewSurveyBlockDto newSurveyBlockDto)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == newSurveyBlockDto.SurveyId);
        
        if (survey == null) throw new NotFoundException("Survey not found");
        
        var surveyBlock = new SurveyBlock
        {
            Id = Guid.NewGuid(),
            Survey = survey,
            Type = newSurveyBlockDto.Type,
            Description = newSurveyBlockDto.Description,
            SurveyBlockId = newSurveyBlockDto.SurveyBlockId,
            CreatedAt = DateTimeOffset.Now
        };
        
        await _context.SurveyBlocks.AddAsync(surveyBlock);
        await _context.SaveChangesAsync();
        
        var dto = SurveyElementDto.CreateFromEntity(surveyBlock);
        
        return dto;
    }

    public async Task<IEnumerable<SurveyElementDto>> GetSurveyBlocks(Guid surveyId)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);
        
        if (survey == null) throw new NotFoundException("Survey not found");
        
        var surveyBlocks = await _context.SurveyBlocks.Where(sb => sb.Survey == survey).ToListAsync();
        
        var surveyBlockList = surveyBlocks.Select(SurveyElementDto.CreateFromEntity).ToList();
        
        return surveyBlockList;
    }

    public async Task<SurveyElementDto> GetSurveyBlock(Guid surveyBlockId)
    {
        var surveyBlock = await _context.SurveyBlocks.FirstOrDefaultAsync(sb => sb.Id == surveyBlockId);
        
        if (surveyBlock == null) throw new NotFoundException("Survey block not found");
        
        var dto = SurveyElementDto.CreateFromEntity(surveyBlock);
        
        return dto;
    }

    public async Task<SurveyElementDto> UpdateSurveyElement(Guid surveyElementId, NewSurveyBlockDto updateSurveyBlockDto)
    {
       var surveyBlock = await _context.SurveyBlocks.FirstOrDefaultAsync(sb => sb.Id == surveyElementId);
       
       if (surveyBlock == null) throw new NotFoundException("Survey block not found");
       
        surveyBlock.Type = updateSurveyBlockDto.Type;
        surveyBlock.Description = updateSurveyBlockDto.Description;
        surveyBlock.UpdatedAt = DateTimeOffset.Now;
         
        _context.SurveyBlocks.Update(surveyBlock);
        await _context.SaveChangesAsync();
        
        var dto = SurveyElementDto.CreateFromEntity(surveyBlock);
        
        return dto;
    }

    public async Task DeleteSurveyBlock(Guid surveyBlockId)
    {
        var surveyBlock = await _context.SurveyBlocks.FirstOrDefaultAsync(sb => sb.Id == surveyBlockId);
        
        if (surveyBlock == null) throw new NotFoundException("Survey block not found");
        
        surveyBlock.DeletedAt = DateTimeOffset.Now;
        _context.SurveyBlocks.Update(surveyBlock);
        await _context.SaveChangesAsync();
    }
}