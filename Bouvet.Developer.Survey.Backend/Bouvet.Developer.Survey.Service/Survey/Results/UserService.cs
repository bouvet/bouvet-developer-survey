using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Results;

public class UserService : IUserService
{
    private readonly DeveloperSurveyContext _context;
    
    public UserService(DeveloperSurveyContext context)
    {
        _context = context;
    }

    public async Task<UserDto> CreateUserBatch(List<NewUserDto> userDto, Guid surveyId)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);
        
        if (survey == null) throw new NotFoundException("Survey not found");
        
        var users = new List<User>();
        
        foreach (var newUserDto in userDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                SurveyId = surveyId,
                RespondId = newUserDto.RespondId,
                CreatedAt = DateTimeOffset.Now
            };
            users.Add(user);
        }
        
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();
        
        return UserDto.CreateFromEntity(users.First());
    }

    public async Task ConnectResponseToUser(List<NewResponseUserDto> newResponseUserDto)
    {
        var allUsers = await _context.Users.ToListAsync();
        var newResponseDtoList = new List<ResponseUser>();
        
        foreach (var responseUserDto in newResponseUserDto)
        {
            var user = allUsers.FirstOrDefault(u => u.Id == responseUserDto.UserId);
            
            if (user == null) throw new NotFoundException("User not found");
            
            var responseUser = new ResponseUser
            {
                UserId = responseUserDto.UserId,
                ResponseId = responseUserDto.ResponseId,
                QuestionId = responseUserDto.QuestionId,
                CreatedAt = DateTimeOffset.Now
            };
            
            newResponseDtoList.Add(responseUser);
        }
        
        await _context.ResponseUsers.AddRangeAsync(newResponseDtoList);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<UserDto>> GetUsersBySurveyId(Guid surveyId)
    {
        var users = await _context.Users.Where(u => u.SurveyId == surveyId).ToListAsync();
        
        return users.Select(UserDto.CreateFromEntity);
    }
    
    public async Task<UserResponseDto> GetUserResponses(Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        if (user == null) throw new NotFoundException("User not found");
        
        return UserResponseDto.CreateFromEntity(user);
    }

    public async Task DeleteUser(Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        if (user == null) throw new NotFoundException("User not found");
        
        user.DeletedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
    }
}