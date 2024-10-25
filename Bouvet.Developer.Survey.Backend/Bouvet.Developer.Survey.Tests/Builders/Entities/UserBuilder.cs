using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class UserBuilder
{
    private readonly User _user;
    private readonly SurveyBuilder _surveyBuilder = new();
    public readonly Guid Id = Guid.Parse("c0d4a11e-95b7-42d5-ac0b-ec00e722396b");
    public const string RespondId = "TestUser";
    
    public UserBuilder()
    {
        var survey = _surveyBuilder.Build();
        _user = new User
        {
            Id = Id,
            RespondId = RespondId,
            SurveyId = survey.Id
        };
    }
    
    public User Build()
    {
        return _user;
    }
}