using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface IUserService
{
    /// <summary>
    /// Create a new user batch
    /// </summary>
    /// <param name="userDto">List of users to create</param>
    /// <param name="surveyId">The survey ID</param>
    /// <returns>The first created user</returns>
    Task<UserDto> CreateUserBatch(List<NewUserDto> userDto, Guid surveyId);
    
    /// <summary>
    /// Get all users for a survey
    /// </summary>
    /// <param name="surveyId">Guid of the survey</param>
    /// <returns>List of users</returns>
    Task<IEnumerable<UserDto>> GetUsersBySurveyId(Guid surveyId);
    
    /// <summary>
    /// Connect a response to a user batch
    /// </summary>
    /// <param name="newResponseUserDto">List of responses to connect</param>
    /// <returns>Task</returns>
    Task ConnectResponseToUser(List<NewResponseUserDto> newResponseUserDto);
    
    /// <summary>
    /// Get a user by its ID
    /// </summary>
    /// <param name="userId">The ID of the user</param>
    /// <returns>The user responses</returns>
    Task<UserResponseDto> GetUserResponses(Guid userId);
    
    /// <summary>
    /// Delete a user by its ID
    /// </summary>
    /// <param name="userId">Guid of the user</param>
    /// <returns>Task</returns>
    Task DeleteUser(Guid userId);
}