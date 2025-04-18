using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class QuestionDto
{
    public Guid Id { get; set; }
    public Guid BlockElementId { get; set; }
    public string DataExportTag { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public bool IsMultipleChoice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? AiAnalyseText { get; set; }
    
    public static QuestionDto CreateFromEntity(Question question)
    {
        return new QuestionDto
        {
            Id = question.Id,
            BlockElementId = question.BlockElementId,
            DataExportTag = question.DataExportTag,
            QuestionText = question.QuestionText,
            QuestionDescription = question.QuestionDescription,
            IsMultipleChoice = question.IsMultipleChoice,
            CreatedAt = question.CreatedAt,
            UpdatedAt = question.UpdatedAt,
            AiAnalyseText = question.AiAnalyse?.Text,
        };
    }
}