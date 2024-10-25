using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface IUserService
{
    Task<UserDto> CreateUserBatch(List<NewUserDto> userDto, Guid surveyId);
    Task<IEnumerable<UserDto>> GetUsersBySurveyId(Guid surveyId);
    Task ConnectResponseToUser(List<NewResponseUserDto> newResponseUserDto);
    
    Task<UserResponseDto> GetUserResponses(Guid userId);
    
    Task DeleteUser(Guid userId);
}