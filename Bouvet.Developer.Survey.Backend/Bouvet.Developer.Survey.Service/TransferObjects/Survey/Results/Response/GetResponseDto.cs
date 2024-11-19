namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class GetResponseDto
{
    public Guid Id { get; set; }
    public float Percentage { get; set; }
    public virtual GetAnswerOptionDto? AnswerOption { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    
    public static GetResponseDto CreateFromEntity(Domain.Entities.Results.Response response, int respondents)
    {
        return new GetResponseDto
        {
            Id = response.Id,
            Percentage = (float)response.Value / respondents * 100,
            CreatedAt = response.CreatedAt,
            AnswerOption = response.AnswerOption != null ? GetAnswerOptionDto.CreateFromEntity(response.AnswerOption) : null
        };
    }
}