using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class AnswerOptionService : IAnswerOptionService
{
    private readonly DeveloperSurveyContext _context;
    
    public AnswerOptionService(DeveloperSurveyContext context)
    {
        _context = context;
    }


    public async Task AddAnswerFromDto(Guid surveyId, PayloadQuestionDto questionDto)
    {
        var survey = await _context.Surveys.FindAsync(surveyId);
        
        if (survey == null) throw new NotFoundException("Survey not found");
        
        if(questionDto.Answers == null) return;
        
        var currentAnswersOption = await _context.AnswerOptions.Where(a => a.SurveyId == surveyId).ToListAsync();
        
        foreach (var answerOption in questionDto.Answers)
        {
            var answer = currentAnswersOption.FirstOrDefault(a => a.Text == answerOption.Value.Display);
            
            if (answer == null)
            {
                var newAnswerOption = new NewAnswerOptionDto
                {
                    SurveyId = surveyId,
                    Text = answerOption.Value.Display,
                    IndexId = answerOption.Key
                };
                
                await CreateAnswerOption(newAnswerOption);
            }
        }
    }
    
    public async Task<AnswerOptionDto> CreateAnswerOption(NewAnswerOptionDto newAnswerOptionDto)
    {
        var survey = await _context.Surveys.FindAsync(newAnswerOptionDto.SurveyId);
        
        if (survey == null) throw new NotFoundException("Survey not found");
        
        var answerOption = new AnswerOption
        {
            Id = Guid.NewGuid(),
            SurveyId = newAnswerOptionDto.SurveyId,
            Text = newAnswerOptionDto.Text,
            IndexId = newAnswerOptionDto.IndexId,
            CreatedAt = DateTimeOffset.Now
        };
        
        await _context.AnswerOptions.AddAsync(answerOption);
        await _context.SaveChangesAsync();
        
        return AnswerOptionDto.CreateFromEntity(answerOption);
    }
    
    public async Task<AnswerOptionDto> UpdateAnswerOption(Guid answerOptionId, NewAnswerOptionDto newAnswerOptionDto)
    {
        var answerOption = await _context.AnswerOptions.FindAsync(answerOptionId);
        
        if (answerOption == null) throw new NotFoundException("Answer option not found");
        
        if(answerOption.Text == newAnswerOptionDto.Text && answerOption.IndexId == newAnswerOptionDto.IndexId) 
            return AnswerOptionDto.CreateFromEntity(answerOption);
        
        answerOption.Text = newAnswerOptionDto.Text;
        answerOption.IndexId = newAnswerOptionDto.IndexId;
        answerOption.UpdatedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
        
        return AnswerOptionDto.CreateFromEntity(answerOption);
    }
    
    public async Task DeleteAnswerOption(Guid answerOptionId)
    {
        var answerOption = await _context.AnswerOptions.FindAsync(answerOptionId);
        
        if (answerOption == null) throw new NotFoundException("Answer option not found");
        
        _context.AnswerOptions.Remove(answerOption);
        await _context.SaveChangesAsync();
    }
}