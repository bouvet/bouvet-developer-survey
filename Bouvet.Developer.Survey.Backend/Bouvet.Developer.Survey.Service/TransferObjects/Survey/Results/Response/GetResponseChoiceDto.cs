using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class GetResponseChoiceDto
{
    public string Text { get; set; } = null!;
    public virtual ICollection<GetResponseDto>? Responses { get; set; }
    
    
    public static GetResponseChoiceDto CreateFromEntity(Choice choice, int respondents)
    {
        return new GetResponseChoiceDto
        {
            Text = choice.Text,
            Responses = choice.Responses?
                .Select(response => GetResponseDto.CreateFromEntity(response, respondents))
                .ToList()
        };
    }
}