using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Ai;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class QuestionResponseDto
{
    public Guid Id { get; set; }
    public string DataExportTag { get; set; } = null!;
    public AiAnalyseDto? AiAnalyse { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public bool IsMultipleChoice { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual ICollection<GetChoiceDto>? Choices { get; set; }
    public int NumberOfRespondents { get; set; }

    public static QuestionResponseDto CreateFromEntity(Question question, ICollection<GetChoiceDto> getChoiceDto)
    {
        return new QuestionResponseDto
        {
            Id = question.Id,
            DataExportTag = question.DateExportTag,
            // AiAnalyse = AiAnalyseDto.CreateFromEntity(question.AiAnalyse ?? null),
            QuestionText = question.QuestionText,
            QuestionDescription = question.QuestionDescription,
            IsMultipleChoice = question.IsMultipleChoice,
            CreatedAt = question.CreatedAt,
            UpdatedAt = question.UpdatedAt,
            Choices = getChoiceDto,
            NumberOfRespondents = question.ResponseUsers != null
            ? question.ResponseUsers.Select(ru => ru.UserId).Distinct().Count()
            : 0,
        };
    }
}
