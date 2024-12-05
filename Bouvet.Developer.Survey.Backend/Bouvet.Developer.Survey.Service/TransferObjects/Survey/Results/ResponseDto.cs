

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

public class ResponseDto
{
    public Guid Id { get; set; }
    public Guid ChoiceId { get; set; }
    public int Value { get; set; }
    public string FieldName { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    
    
    public static ResponseDto CreateFromEntity(Domain.Entities.Results.Response response)
    {
        return new ResponseDto
        {
            Id = response.Id,
            Value = response.Value,
            FieldName = response.FieldName,
            ChoiceId = response.ChoiceId,
            CreatedAt = response.CreatedAt,
        };
    }
}