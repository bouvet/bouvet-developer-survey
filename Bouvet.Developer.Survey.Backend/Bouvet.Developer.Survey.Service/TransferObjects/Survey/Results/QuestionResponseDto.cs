using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

public class QuestionResponseDto
{
    public Guid Id { get; set; }
    public Guid BlockElementId { get; set; }
    public string SurveyId { get; set; } = null!;
    public string DateExportTag { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public bool IsMultipleChoice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual ICollection<ChoiceDto>? Choices { get; set; } = new List<ChoiceDto>();
    
    public static QuestionResponseDto CreateFromEntity(Question question)
    {
        return new QuestionResponseDto
        {
            Id = question.Id,
            BlockElementId = question.BlockElementId,
            SurveyId = question.SurveyId,
            DateExportTag = question.DateExportTag,
            QuestionText = question.QuestionText,
            QuestionDescription = question.QuestionDescription,
            IsMultipleChoice = question.IsMultipleChoice,
            CreatedAt = question.CreatedAt,
            UpdatedAt = question.UpdatedAt,
            Choices = question.Choices?.Select(ChoiceDto.CreateFromEntity).ToList()
        };
    }
}