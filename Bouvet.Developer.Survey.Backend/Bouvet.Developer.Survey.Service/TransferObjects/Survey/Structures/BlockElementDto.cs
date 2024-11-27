using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class BlockElementDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual ICollection<QuestionDto>? Questions { get; set; }
    
    public static BlockElementDto CreateFromEntity(BlockElement block)
    {
        return new BlockElementDto
        {
            Id = block.Id,
            Type = block.Type,
            CreatedAt = block.CreatedAt,
            UpdatedAt = block.UpdatedAt,
            Questions = block.Questions?.Select(QuestionDto.CreateFromEntity).ToList()
        };
    }
}