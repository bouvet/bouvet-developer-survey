using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class ResponseUserBuilder
{
    private readonly ResponseUser _responseUser;
    private readonly UserBuilder _userBuilder = new();
    private readonly ResponseBuilder _responseBuilder = new();
    
    public ResponseUserBuilder()
    {
        var user = _userBuilder.Build();
        var response = _responseBuilder.Build();
        
        _responseUser = new ResponseUser
        {
            UserId = user.Id,
            ResponseId = response.Id
        };
    }
    
    public ResponseUser Build()
    {
        return _responseUser;
    }
    
}