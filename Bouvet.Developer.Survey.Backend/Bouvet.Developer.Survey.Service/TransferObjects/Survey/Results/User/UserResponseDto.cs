using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public string RespondId { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    
    public virtual ICollection<ResponseUserDto>? ResponseUsers { get; set; }
    
    public static UserResponseDto CreateFromEntity(Domain.Entities.Results.User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            SurveyId = user.SurveyId,
            RespondId = user.RespondId,
            CreatedAt = user.CreatedAt,
            DeletedAt = user.DeletedAt,
            ResponseUsers = user.ResponseUsers?.Select(ResponseUserDto.CreateFromEntity).ToList()
        };
    }
}

public class ResponseUserDto
{
    public Guid ResponseId { get; set; }
    public virtual ResponseDto Response { get; set; } = null!;
    
    public static ResponseUserDto CreateFromEntity(ResponseUser response)
    {
        return new ResponseUserDto
        {
            ResponseId = response.ResponseId,
            Response = ResponseDto.CreateFromEntity(response.Response)
        };
    }
}