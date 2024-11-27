using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class GetAnswerOptionDto
{
    public string Text { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    
    
    public static GetAnswerOptionDto CreateFromEntity(AnswerOption answerOption)
    {
        return new GetAnswerOptionDto
        {
            Text = answerOption.Text,
            CreatedAt = answerOption.CreatedAt,
            UpdatedAt = answerOption.UpdatedAt
        };
    }
}