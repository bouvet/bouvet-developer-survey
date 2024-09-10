using Bouvet.Developer.Survey.Domain.Entities;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class SurveyDto
{
    public string Name { get; set; } = null!;
    // public ICollection<Block>? Blocks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    
    public static Domain.Entities.Survey CreateFromEntity(SurveyDto survey)
    {
        return new Domain.Entities.Survey
        {
            Name = survey.Name,
            CreatedAt = survey.CreatedAt,
            UpdatedAt = survey.UpdatedAt,
            LastSyncedAt = survey.LastSyncedAt,
            // Blocks = survey.Blocks?.Select(BlockDto.CreateFromEntity).ToList(),
        };
    }
}