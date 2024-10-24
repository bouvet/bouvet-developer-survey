namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;

public class GetResponseDto
{
    public Guid Id { get; set; }
    public int Value { get; set; }
    public virtual GetAnswerOptionDto? AnswerOption { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    
    public static GetResponseDto CreateFromEntity(Domain.Entities.Results.Response response)
    {
        return new GetResponseDto
        {
            Id = response.Id,
            Value = response.Value,
            CreatedAt = response.CreatedAt,
            AnswerOption = response.AnswerOption != null ? GetAnswerOptionDto.CreateFromEntity(response.AnswerOption) : null
        };
    }
}