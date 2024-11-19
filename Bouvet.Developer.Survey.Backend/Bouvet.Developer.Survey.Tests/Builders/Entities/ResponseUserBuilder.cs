using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class ResponseUserBuilder
{
    private readonly ResponseUser _responseUser;
    private readonly UserBuilder _userBuilder = new();
    private readonly ResponseBuilder _responseBuilder = new();
    private readonly QuestionBuilder _questionBuilder = new();
    
    public ResponseUserBuilder()
    {
        var user = _userBuilder.Build();
        var response = _responseBuilder.Build();
        var question = _questionBuilder.Build();
        
        _responseUser = new ResponseUser
        {
            UserId = user.Id,
            ResponseId = response.Id,
            QuestionId = question.Id
        };
    }
    
    public ResponseUser Build()
    {
        return _responseUser;
    }
    
}