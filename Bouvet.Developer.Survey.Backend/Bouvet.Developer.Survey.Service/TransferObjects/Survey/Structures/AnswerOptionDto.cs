using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class AnswerOptionDto
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public string Text { get; set; } = null!;
    public string IndexId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    
    public static AnswerOptionDto CreateFromEntity(AnswerOption answerOption)
    {
        return new AnswerOptionDto
        {
            Id = answerOption.Id,
            SurveyId = answerOption.SurveyId,
            Text = answerOption.Text,
            IndexId = answerOption.IndexId,
            CreatedAt = answerOption.CreatedAt,
            UpdatedAt = answerOption.UpdatedAt
        };
    }
}