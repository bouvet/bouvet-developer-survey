using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

public class ResponseDto
{
    public Guid Id { get; set; }
    public Guid ChoiceId { get; set; }
    public int Value { get; set; }
    public string FieldName { get; set; } = null!;
    public Guid? AnswerOptionId { get; set; }
    public virtual AnswerOptionDto? AnswerOption { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    
    public static ResponseDto CreateFromEntity(Domain.Entities.Results.Response response)
    {
        return new ResponseDto
        {
            Id = response.Id,
            Value = response.Value,
            FieldName = response.FieldName,
            ChoiceId = response.ChoiceId,
            AnswerOptionId = response.AnswerOptionId,
            CreatedAt = response.CreatedAt,
            AnswerOption = response.AnswerOption != null ? AnswerOptionDto.CreateFromEntity(response.AnswerOption) : null
        };
    }
}