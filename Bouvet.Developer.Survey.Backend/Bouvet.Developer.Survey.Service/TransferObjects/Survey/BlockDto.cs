using Bouvet.Developer.Survey.Domain.Entities;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class BlockDto
{
    public Guid Id { get; set; }
    public string Question { get; set; } = null!;
    public string Type { get; set; } = null!;
    public Guid SurveyId { get; set; }
    public ICollection<OptionDto>? Options { get; set; }
    
    public static BlockDto CreateFromEntity(Block block)
    {
        return new BlockDto
        { 
            Id = block.Id,
            Question = block.Question,
            Type = block.Type,
            SurveyId = block.SurveyId,
            Options = block.Options?.Select(OptionDto.CreateFromEntity).ToList()
        };
    }
}

