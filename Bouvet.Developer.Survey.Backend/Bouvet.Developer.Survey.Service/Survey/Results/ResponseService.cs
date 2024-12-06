using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Results;

public class ResponseService : IResponseService
{
    private readonly DeveloperSurveyContext _context;
    
    public ResponseService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    
    public async Task<List<ResponseDto>> CreateResponse(List<NewResponseDto> newResponseDtos)
    {
        var choiceIds = newResponseDtos.Select(x => x.ChoiceId).ToList();
        
        var allResponses = await _context.Responses
            .Where(r => choiceIds.Contains(r.ChoiceId))
            .ToListAsync();

        foreach (var response in allResponses)
        {
            var responseUser = await _context.ResponseUsers
                .FirstOrDefaultAsync(x => x.ResponseId == response.Id);

            if (responseUser != null)
            {
                _context.ResponseUsers.Remove(responseUser);
                await _context.SaveChangesAsync();
            }
        }
        
        //Delete existing responses
        if (allResponses.Count > 0)
        {
            Console.WriteLine($"Deleting {allResponses.Count} existing responses");
            _context.Responses.RemoveRange(allResponses);
            await _context.SaveChangesAsync();
        }
        
        // Fetch all required choices and answer options in one query
        var choices = await _context.Choices
            .Where(x => choiceIds.Contains(x.Id))
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
            var response = new Response
            {
                Id = Guid.NewGuid(),
                ChoiceId = newResponseDto.ChoiceId,
                Value = newResponseDto.Value,
                FieldName = newResponseDto.FieldName,
                HasWorkedWith = newResponseDto.HasWorkedWith,
                WantsToWorkWith = newResponseDto.WantsToWorkWith,
                CreatedAt = DateTimeOffset.Now
            };
            
            responses.Add(response);
        }

        // Bulk insert responses
        await _context.Responses.AddRangeAsync(responses);
        await _context.SaveChangesAsync();

            
        return responses.Select(ResponseDto.CreateFromEntity).ToList();
    }
    
    
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
        
        _context.Responses.RemoveRange(response);
        await _context.SaveChangesAsync();
    }
}