using Bouvet.Developer.Survey.Domain.Entities;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class BlockOptionListDto
{
    public Guid Id { get; set; }
    public string Question { get; set; } = null!;
    public string Type { get; set; } = null!;
    public Guid SurveyId { get; set; }
    public ICollection<OptionDto>? Options { get; set; }
    
    public static BlockOptionListDto CreateFromEntity(Block block)
    {
        return new BlockOptionListDto
        { 
            Id = block.Id,
            Question = block.Question,
            Type = block.Type,
            SurveyId = block.SurveyId,
            Options = block.Options?.Select(OptionDto.CreateFromEntity).ToList()
        };
    }
}