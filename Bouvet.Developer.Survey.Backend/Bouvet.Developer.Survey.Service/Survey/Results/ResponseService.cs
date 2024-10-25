using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Results;

public class ResponseService : IResponseService
{
    private readonly DeveloperSurveyContext _context;
    private readonly IUserService _userService;
    
    public ResponseService(DeveloperSurveyContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    
    public async Task<List<ResponseDto>> CreateResponse(List<NewResponseDto> newResponseDtos)
    {
        var choiceIds = newResponseDtos.Select(x => x.ChoiceId).ToList();
        var answerOptionIds = newResponseDtos
            .Where(r => r.AnswerOptionId != null)
            .Select(r => r.AnswerOptionId.Value)
            .ToList();
        
        foreach (var newResponseDto in newResponseDtos)
        {
            Console.WriteLine($"Creating response for ResponseId: {newResponseDto.ResponseId}");
        }
        
        // Fetch all required choices and answer options in one query
        var choices = await _context.Choices
            .Where(x => choiceIds.Contains(x.Id))
            .ToListAsync();

        var answerOptions = await _context.AnswerOptions
            .Where(x => answerOptionIds.Contains(x.Id))
            .ToListAsync();
        
        // Validate that all choices exist
        var invalidChoiceIds = choiceIds.Except(choices.Select(c => c.Id)).ToList();
        if (invalidChoiceIds.Any())
        {
            throw new NotFoundException($"Choice not found");
        }
        
        // Prepare the responses for bulk insert
        var responses = new List<Response>();
        foreach (var newResponseDto in newResponseDtos)
        {
            var answerFound = newResponseDto.AnswerOptionId != null &&
                              answerOptions.Any(x => x.Id == newResponseDto.AnswerOptionId);

            var response = new Response
            {
                Id = Guid.NewGuid(),
                ChoiceId = newResponseDto.ChoiceId,
                Value = newResponseDto.Value,
                FieldName = newResponseDto.FieldName,
                AnswerOptionId = answerFound ? newResponseDto.AnswerOptionId : null,
                CreatedAt = DateTimeOffset.Now
            };
            
            responses.Add(response);
        }

        // Bulk insert responses
        await _context.Responses.AddRangeAsync(responses);
        await _context.SaveChangesAsync();

        return responses.Select(ResponseDto.CreateFromEntity).ToList();
    }
    
    // private async Task MapUsers
    
    public async Task<ResponseDto> GetResponse(Guid responseId)
    {
        var response = await _context.Responses.FirstOrDefaultAsync(x => x.Id == responseId);
        
        if (response == null) throw new NotFoundException("Response not found");
        
        return ResponseDto.CreateFromEntity(response);
    }
    
    public async Task<List<ResponseDto>> GetResponsesByChoiceId(Guid choiceId)
    {
        var responses = await _context.Responses.Where(r => r.ChoiceId == choiceId).ToListAsync();
        
        if(responses.Count == 0) throw new NotFoundException("No responses found");
        
        return responses.Select(ResponseDto.CreateFromEntity).ToList();
    }
    
    public async Task DeleteResponse(Guid responseId)
    {
        var response = await _context.Responses.FirstOrDefaultAsync(x => x.Id == responseId);
        
        if (response == null) throw new NotFoundException("Response not found");
        
        response.DeletedAt = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }
}