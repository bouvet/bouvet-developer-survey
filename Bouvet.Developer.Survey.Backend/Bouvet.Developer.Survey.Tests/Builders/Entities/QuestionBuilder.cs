using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Tests.Builders.Entities;

public class QuestionBuilder
{
    private readonly Question _question;
    private readonly SurveyBlockBuilder _surveyBlockBuilder = new();
    public readonly Guid Id = Guid.Parse("c0d4a11e-95b7-42d5-ac0b-ec00e722396a");
    public const string QuestionText = "Test Question";
    public const string QuestionDescription = "Test Question Description";
    public const string SurveyId = "survey-123456";
    public const string DateExportTag = "date-123456";
        
    
    public QuestionBuilder()
    {
        var surveyBlock = _surveyBlockBuilder.Build();
        
        _question = new Question
        {
            Id = Id,
            BlockElementId = surveyBlock.Id,
            SurveyId = SurveyId,
            DateExportTag = DateExportTag,
            QuestionText = QuestionText,
            QuestionDescription = QuestionDescription
        };
    }
    
    public Question Build()
    {
        return _question;
    }
}