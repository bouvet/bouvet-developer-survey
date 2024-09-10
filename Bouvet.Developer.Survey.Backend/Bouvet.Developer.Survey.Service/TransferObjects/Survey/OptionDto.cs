namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class OptionDto
{
    public string Value { get; set; } = null!;
    
    public static Domain.Entities.Option CreateFromEntity(OptionDto option)
    {
        return new Domain.Entities.Option
        {
            Value = option.Value,
        };
    }
}