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
    
    //TODO check for differences from response csv
    public async Task CheckForDifferences(List<NewResponseDto> newResponseDtos)
    {
        var existingResponses  = await _context.Responses
            .Where(r => newResponseDtos.Select(x => x.FieldName).Contains(r.FieldName))
            .ToListAsync();
        
        // Count the responses to compare the number of records
        var existingResponsesCount = existingResponses.Count;
        var newResponsesCount = newResponseDtos.Count;
        
        var newResponsesList = new List<NewResponseDto>();
        var deletedResponsesList = new List<Response>();
        
        if (existingResponsesCount != newResponsesCount)
        {
            Console.WriteLine($"Mismatch in number of records. Database has {existingResponsesCount} rows, and the incoming list has {newResponsesCount} rows.");
        
            // Check which responses need to be added or updated based on the data
            foreach (var newResponseDto in newResponseDtos)
            {
                //Delete responses with the same field name and choice id and create new ones
                var existingResponse = existingResponses
                    .FirstOrDefault(x => x.FieldName == newResponseDto.FieldName &&
                                         x.ChoiceId == newResponseDto.ChoiceId);

                if (existingResponse != null)
                {
                    Console.WriteLine($"Deleting response with field name {newResponseDto.FieldName} and ChoiceId {newResponseDto.ChoiceId}");
                    deletedResponsesList.Add(existingResponse);
                }
               
                // Console.WriteLine(
                //     $"Creating new response for field name {newResponseDto.FieldName}, ChoiceId {newResponseDto.ChoiceId}");
                newResponsesList.Add(newResponseDto);
                
            }
        }
        else
        {
            // Counts match, proceed with checking for changes
            foreach (var newResponseDto in newResponseDtos)
            {
                var existingResponse = existingResponses
                    .FirstOrDefault(x => x.FieldName == newResponseDto.FieldName &&
                                         x.ChoiceId == newResponseDto.ChoiceId);

                if (existingResponse == null)
                {
                    // If no match, create a new response
                    // Console.WriteLine($"Creating new response for field name {newResponseDto.FieldName}, ChoiceId {newResponseDto.ChoiceId}");
                    newResponsesList.Add(newResponseDto);
                }
                else
                {
                    // If the existing response's values have changed, update them
                    if (existingResponse.Value != newResponseDto.Value ||
                        existingResponse.HasWorkedWith != newResponseDto.HasWorkedWith ||
                        existingResponse.WantsToWorkWith != newResponseDto.WantsToWorkWith)
                    {
                        // Console.WriteLine($"Response with field name {newResponseDto.FieldName} and ChoiceId {newResponseDto.ChoiceId} has changed");

                        // Update the existing response with the new value
                        existingResponse.Value = newResponseDto.Value;
                        existingResponse.HasWorkedWith = newResponseDto.HasWorkedWith;
                        existingResponse.WantsToWorkWith = newResponseDto.WantsToWorkWith;
                        existingResponse.UpdatedAt = DateTimeOffset.Now;

                        // Save changes to the database
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Console.WriteLine($"No changes to response with field name {newResponseDto.FieldName} and ChoiceId {newResponseDto.ChoiceId}");
                    }
                }
            }
        }
        
        // If there are responses to delete, delete them
        if (deletedResponsesList.Count > 0)
        {
            foreach (var response in deletedResponsesList)
            {
                await DeleteResponse(response.Id);
            }
        }

        // If there are new responses to create, create them
        await CreateResponse(newResponsesList);
        
    }
    
    public async Task<List<ResponseDto>> CreateResponse(List<NewResponseDto> newResponseDtos)
    {
        var choiceIds = newResponseDtos.Select(x => x.ChoiceId).ToList();
        
        var allResponses = await _context.Responses
            .Where(r => choiceIds.Contains(r.ChoiceId))
            .ToListAsync();
        
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
        
        if (response == null) throw new NotFoundException("Response not found for deletion with id " + responseId);
        
        _context.Responses.RemoveRange(response);
        await _context.SaveChangesAsync();
    }
}