using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface IResponseService
{
    /// <summary>
    /// Create a new response
    /// </summary>
    /// <param name="newResponseDtos">The response to create</param>
    /// <returns>The created response</returns>
    Task<List<ResponseDto>> CreateResponse(List<NewResponseDto> newResponseDtos);
    
    /// <summary>
    /// Get a response by its ID
    /// </summary>
    /// <param name="responseId">The ID of the response</param>
    /// <returns>The response</returns>
    Task<ResponseDto> GetResponse(Guid responseId);
    
    /// <summary>
    /// Get all responses for a choice
    /// </summary>
    /// <param name="choiceId">The ID of the choice</param>
    /// <returns>The responses</returns>
    Task<List<ResponseDto>> GetResponsesByChoiceId(Guid choiceId);
    
    /// <summary>
    /// Delete a response by its ID
    /// </summary>
    /// <param name="responseId">The ID of the response</param>
    /// <returns></returns>
    Task DeleteResponse(Guid responseId);
}