namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class SurveyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<BlockDto>? Blocks { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
  
    
    public static SurveyDto CreateFromEntity(Domain.Entities.Survey survey)
    {
        return new SurveyDto
        {
            Id = survey.Id,
            Name = survey.Name,
            CreatedAt = survey.CreatedAt,
            UpdatedAt = survey.UpdatedAt,
            LastSyncedAt = survey.LastSyncedAt,
            Blocks = survey.Blocks?.Select(BlockDto.CreateFromEntity).ToList()
        };
    }
}