using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class ChoiceService : IChoiceService
{
    private readonly DeveloperSurveyContext _context;
    
    public ChoiceService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    
    public async Task CheckForDifferences(Guid questionId, PayloadQuestionDto questionsDto)
    {
        var existingQuestion = await _context.Questions
            .Where(q => q.Id == questionId)
            .Include(q => q.Choices)
            .FirstOrDefaultAsync();
        
        var newChoices = questionsDto.Choices.Values.Select(c => new Choice
        {
            Id = Guid.NewGuid(),
            QuestionId = questionId,
            Text = c.Display,
            CreatedAt = DateTimeOffset.Now
        }).ToList();
        
        if (existingQuestion?.Choices == null) return;
        
        await AddNewChoices(existingQuestion.Choices, newChoices);
        await RemoveOldChoices(existingQuestion.Choices, newChoices);
        await UpdateExistingChoices(existingQuestion.Choices, newChoices);

        await _context.SaveChangesAsync();
    }
    
    private async Task AddNewChoices(ICollection<Choice> existingChoices, List<Choice> newChoices)
    {
        foreach (var choice in newChoices)
        {
            if (existingChoices == null || !existingChoices.Any(c => c.Text == choice.Text))
            {
                Console.WriteLine("Adding new choice");
                await _context.Choices.AddAsync(choice);
            }
        }
    }

    private async Task RemoveOldChoices(ICollection<Choice> existingChoices, List<Choice> newChoices)
    {
        foreach (var choice in existingChoices)
        {
            if (newChoices.All(c => c.Text != choice.Text))
            {
                Console.WriteLine("Removing old choice");
                await DeleteChoice(choice.Id);
            }
        }
    }

    private async Task UpdateExistingChoices(ICollection<Choice> existingChoices, List<Choice> newChoices)
    {
        foreach (var choice in existingChoices)
        {
            var newChoice = newChoices.FirstOrDefault(c => c.Text == choice.Text);
            if (newChoice != null && choice.Text != newChoice.Text)
            {
                await UpdateChoice(choice.Id, new NewChoiceDto { Text = newChoice.Text });
            }
        }
    }

    public async Task<List<ChoiceDto>> CreateChoice(List<NewChoiceDto> newChoiceDto, Guid questionId)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == questionId);
        
        if (question == null) throw new NotFoundException("Question not found");
        
        var choices = newChoiceDto.Select(dto => new Choice
        {
            Id = Guid.NewGuid(),
            QuestionId = questionId,
            Text = dto.Text,
            IndexId = dto.IndexId,
            CreatedAt = DateTimeOffset.Now
        }).ToList();
        
        await _context.Choices.AddRangeAsync(choices);
        await _context.SaveChangesAsync();
        
        var dtoS = choices.Select(ChoiceDto.CreateFromEntity).ToList();
        
        return dtoS;
    }

    public async Task<ChoiceDto> GetChoice(Guid choiceId)
    {
        var choice = await _context.Choices.FirstOrDefaultAsync(c => c.Id == choiceId);
        
        if (choice == null) throw new NotFoundException("Choice not found");
        
        var dto = ChoiceDto.CreateFromEntity(choice);
        
        return dto;
    }

    public async Task<IEnumerable<ChoiceDto>> GetChoices(Guid questionId)
    {
        var choices = await _context.Choices.Where(c => c.QuestionId == questionId).ToListAsync();
        
        if(choices.Count == 0) throw new NotFoundException("No choices found");
        
        var dtoS = choices.Select(ChoiceDto.CreateFromEntity).ToList();
        
        return dtoS;
    }

    public async Task<ChoiceDto> UpdateChoice(Guid choiceId, NewChoiceDto updateChoiceDto)
    {
        var choice = await _context.Choices.FirstOrDefaultAsync(c => c.Id == choiceId);
        
        if (choice == null) throw new NotFoundException("Choice not found");
        
        choice.Text = updateChoiceDto.Text;
        choice.UpdatedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
        
        var dto = ChoiceDto.CreateFromEntity(choice);
        
        return dto;
    }

    public async Task DeleteChoice(Guid choiceId)
    {
        var choice = await _context.Choices.FirstOrDefaultAsync(c => c.Id == choiceId);
        
        if (choice == null) throw new NotFoundException("Choice not found");
        
        choice.DeletedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
    }
}