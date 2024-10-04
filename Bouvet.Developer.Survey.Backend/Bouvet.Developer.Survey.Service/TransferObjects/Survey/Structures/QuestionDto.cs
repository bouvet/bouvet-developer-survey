using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class QuestionDto
{
    public string BlockElementId { get; set; } = null!;
    public string SurveyId { get; set; } = null!;
    public string DateExportTag { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    
    public static QuestionDto CreateFromEntity(Question question)
    {
        return new QuestionDto
        {
            BlockElementId = question.BlockElementId,
            SurveyId = question.SurveyId,
            DateExportTag = question.DateExportTag,
            QuestionText = question.QuestionText,
            QuestionDescription = question.QuestionDescription,
            CreatedAt = question.CreatedAt,
            UpdatedAt = question.UpdatedAt,
            LastSyncedAt = question.LastSyncedAt,
        };
    }
}