using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class BlockDto
{
    public Guid BlockId { get; set; }
    public string Type { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    
    public static BlockDto CreateFromEntity(BlockElement block)
    {
        return new BlockDto
        {
            BlockId = block.BlockId,
            Type = block.Type,
            QuestionId = block.QuestionId,
            CreatedAt = block.CreatedAt,
            UpdatedAt = block.UpdatedAt,
            LastSyncedAt = block.LastSyncedAt,
        };
    }
}