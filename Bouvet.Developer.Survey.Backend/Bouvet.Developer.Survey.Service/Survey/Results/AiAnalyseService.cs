using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Ai;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Results;

public class AiAnalyseService : IAiAnalyseService
{
    private readonly DeveloperSurveyContext _context;
    
    public AiAnalyseService(DeveloperSurveyContext context)
    {
        _context = context;
    }

    public async Task<AiAnalyseDto> CreateAiAnalyse(NewAiAnalyseDto aiAnalyseDto)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == aiAnalyseDto.QuestionId);
        
        if (question == null) throw new NotFoundException("Question not found");
        
        var aiAnalyse = new AiAnalyse
        {
            Id = Guid.NewGuid(),
            Text = aiAnalyseDto.Text,
            QuestionId = aiAnalyseDto.QuestionId,
            CreatedAt = DateTimeOffset.Now
        };
        
        await _context.AiAnalyses.AddAsync(aiAnalyse);
        await _context.SaveChangesAsync();
        
        return AiAnalyseDto.CreateFromEntity(aiAnalyse);
    }

    public async Task<AiAnalyseDto> GetAiAnalysesByQuestionId(Guid questionId)
    {
        var aiAnalyse = await _context.AiAnalyses.FirstOrDefaultAsync(a => a.QuestionId == questionId);
        
        if (aiAnalyse == null) throw new NotFoundException("AiAnalyse not found");
        
        return AiAnalyseDto.CreateFromEntity(aiAnalyse);
    }

    public async Task<AiAnalyseDto> UpdateAiAnalyse(Guid aiAnalyseId, NewAiAnalyseDto aiAnalyseDto)
    {
        var aiAnalyse = await _context.AiAnalyses.FirstOrDefaultAsync(a => a.Id == aiAnalyseId);
        
        if (aiAnalyse == null) throw new NotFoundException("AiAnalyse not found");
        
        aiAnalyse.Text = aiAnalyseDto.Text;
        aiAnalyse.UpdatedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
        
        return AiAnalyseDto.CreateFromEntity(aiAnalyse);
    }

    public async Task DeleteAiAnalyse(Guid aiAnalyseId)
    {
        var aiAnalyse = await _context.AiAnalyses.FirstOrDefaultAsync(a => a.Id == aiAnalyseId);
        
        if (aiAnalyse == null) throw new NotFoundException("AiAnalyse not found");
        
        aiAnalyse.DeletedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
    }
}