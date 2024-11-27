using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class QuestionResponseDto
{
    public Guid Id { get; set; }
    public string DataExportTag { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public bool IsMultipleChoice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual ICollection<GetChoiceDto>? Choices { get; set; }
    
    public static QuestionResponseDto CreateFromEntity(Question question,int questionRespondents)
    {
        return new QuestionResponseDto
        {
            Id = question.Id,
            DataExportTag = question.DateExportTag,
            QuestionText = question.QuestionText,
            QuestionDescription = question.QuestionDescription,
            IsMultipleChoice = question.IsMultipleChoice,
            CreatedAt = question.CreatedAt,
            UpdatedAt = question.UpdatedAt,
            Choices = question.Choices?
                .OrderBy(c => c.IndexId)
                .Select(choice => GetChoiceDto.CreateFromEntity(choice, questionRespondents))
                .ToList()
        };
    }
}