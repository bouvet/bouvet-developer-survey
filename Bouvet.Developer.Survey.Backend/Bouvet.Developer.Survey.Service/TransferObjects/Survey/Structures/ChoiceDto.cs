using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class ChoiceDto
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public string Text { get; set; } = null!;
    public string IndexId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual ICollection<ResponseDto>? Responses { get; set; }
    
    
    public static ChoiceDto CreateFromEntity(Choice choice)
    {
        return new ChoiceDto
        {
            Id = choice.Id,
            QuestionId = choice.QuestionId,
            Text = choice.Text,
            IndexId = choice.IndexId,
            CreatedAt = choice.CreatedAt,
            UpdatedAt = choice.UpdatedAt,
            Responses = choice.Responses?.Select(ResponseDto.CreateFromEntity).ToList()
        };
    }
}