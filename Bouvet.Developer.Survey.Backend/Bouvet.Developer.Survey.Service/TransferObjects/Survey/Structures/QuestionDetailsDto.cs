using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class QuestionDetailsDto
{
    public Guid Id { get; set; }
    public Guid BlockElementId { get; set; }
    public string SurveyId { get; set; } = null!;
    public string DataExportTag { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public bool IsMultipleChoice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual ICollection<ChoiceDto>? Choices { get; set; } = new List<ChoiceDto>();
    
    public static QuestionDetailsDto CreateFromEntity(Question question)
    {
        return new QuestionDetailsDto
        {
            Id = question.Id,
            BlockElementId = question.BlockElementId,
            SurveyId = question.SurveyId,
            DataExportTag = question.DataExportTag,
            QuestionText = question.QuestionText,
            QuestionDescription = question.QuestionDescription,
            IsMultipleChoice = question.IsMultipleChoice,
            CreatedAt = question.CreatedAt,
            UpdatedAt = question.UpdatedAt,
            Choices = question.Choices?.Select(ChoiceDto.CreateFromEntity).ToList()
        };
    }
}