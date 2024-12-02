using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class QuestionOnlyResponseDto
{
    public string QuestionText { get; set; } = null!;
    public string QuestionDescription { get; set; } = null!;
    public virtual ICollection<GetResponseChoiceDto>? Choices { get; set; }
    
    public static QuestionOnlyResponseDto CreateFromEntity(Question question,int questionRespondents)
    {
        return new QuestionOnlyResponseDto
        {
            QuestionText = question.QuestionText,
            QuestionDescription = question.QuestionDescription,
            Choices = question.Choices?
                .OrderBy(c => c.IndexId)
                .Select(choice => GetResponseChoiceDto.CreateFromEntity(choice, questionRespondents))
                .ToList()
        };
    }
}