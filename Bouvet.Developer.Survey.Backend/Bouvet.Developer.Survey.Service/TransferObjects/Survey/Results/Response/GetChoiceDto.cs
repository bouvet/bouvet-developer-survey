using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class GetChoiceDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public string IndexId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
	public IEnumerable<GetResponseDto> Responses { get; set; } = null!;

    public static GetChoiceDto CreateFromEntity(Choice choice, AnswerDto? tbd)
    {
        return new GetChoiceDto
        {
            Id = choice.Id,
            Text = choice.Text,
            IndexId = choice.IndexId,
            CreatedAt = choice.CreatedAt,
            UpdatedAt = choice.UpdatedAt,
			// value or empty list
			Responses = tbd?.Responses ?? new List<GetResponseDto>()
        };
    }
}
