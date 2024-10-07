using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class BlockElementDto
{
    public Guid Id { get; set; }
    public Guid BlockId { get; set; }
    public string Type { get; set; } = null!;
    public string QuestionId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    
    public static BlockElementDto CreateFromEntity(BlockElement block)
    {
        return new BlockElementDto
        {
            Id = block.Id,
            BlockId = block.SurveyElementGuid,
            Type = block.Type,
            QuestionId = block.QuestionId,
            CreatedAt = block.CreatedAt,
            UpdatedAt = block.UpdatedAt,
            LastSyncedAt = block.LastSyncedAt,
        };
    }
}