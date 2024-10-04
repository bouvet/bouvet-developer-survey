using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class ChoiceDto
{
    public string QuestionId { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    
    
    public static ChoiceDto CreateFromEntity(Choice choice)
    {
        return new ChoiceDto
        {
            QuestionId = choice.QuestionId,
            Text = choice.Text,
            CreatedAt = choice.CreatedAt,
            UpdatedAt = choice.UpdatedAt,
            LastSyncedAt = choice.LastSyncedAt,
        };
    }
}