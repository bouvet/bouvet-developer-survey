using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class QuestionService : IQuestionService
{
    private readonly DeveloperSurveyContext _context;
    private readonly IChoiceService _choiceService;
    
    public QuestionService(DeveloperSurveyContext context, IChoiceService choiceService)
    {
        _context = context;
        _choiceService = choiceService;
    }
    
    public async Task<QuestionDto> CreateQuestionAsync(NewQuestionDto newQuestionDto)
    {
        var blockElement = await _context.BlockElements.FirstOrDefaultAsync(be => be.QuestionId == newQuestionDto.BlockElementId);
        
        if (blockElement == null) throw new NotFoundException("Block element not found");
        
        var question = new Question
        {
            Id = Guid.NewGuid(),
            BlockElementId = blockElement.Id,
            SurveyId = newQuestionDto.SurveyId,
            DateExportTag = newQuestionDto.DateExportTag,
            QuestionText = newQuestionDto.QuestionText,
            QuestionDescription = newQuestionDto.QuestionDescription,
            CreatedAt = DateTimeOffset.Now
        };
        
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
        
        if(newQuestionDto.Choices != null)
            await _choiceService.CreateChoice(newQuestionDto.Choices, question.Id);
        
        var dto = QuestionDto.CreateFromEntity(question);
        
        return dto;
    }

    public async Task<QuestionDto> GetQuestionByIdAsync(Guid questionId)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        
        if (question == null) throw new NotFoundException("Question not found");
        
        var dto = QuestionDto.CreateFromEntity(question);
        
        return dto;
    }

    public async Task<IEnumerable<QuestionDto>> GetQuestionsBySurveyBlockIdAsync(Guid surveyBlockId)
    {
        var questions = await _context.Questions
            .Where(q => q.BlockElementId == surveyBlockId)
            .ToListAsync();
        
        if(questions.Count == 0) throw new NotFoundException("No questions found");
        
        var dtoS = questions.Select(QuestionDto.CreateFromEntity).ToList();
        
        return dtoS;
    }

    public async Task<QuestionDto> UpdateQuestionAsync(Guid questionId, NewQuestionDto updateQuestionDto)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        
        if (question == null) throw new NotFoundException("Question not found");
        
        question.DateExportTag = updateQuestionDto.DateExportTag;
        question.QuestionText = updateQuestionDto.QuestionText;
        question.QuestionDescription = updateQuestionDto.QuestionDescription;
        question.UpdatedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
        
        var dto = QuestionDto.CreateFromEntity(question);
        
        return dto;
    }

    public async Task DeleteQuestionAsync(Guid questionId)
    {
        var questionToBeDeleted = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        
        if (questionToBeDeleted == null) throw new NotFoundException("Question not found");
        
        questionToBeDeleted.DeletedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
    }
}