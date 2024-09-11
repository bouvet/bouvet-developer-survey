using Bouvet.Developer.Survey.Domain.Entities;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class OptionDto
{
    public Guid Id { get; set; }
    public string Value { get; set; } = null!;
    public Guid BlockId { get; set; }
    
    public static OptionDto CreateFromEntity(Option option)
    {
        return new OptionDto
        {
            Id = option.Id,
            Value = option.Value,
            BlockId = option.BlockId,
        };
    }
}