namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class SurveyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string SurveyId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    
    public virtual ICollection<SurveyElementDto>? SurveyBlocks { get; set; }
  
    
    public static SurveyDto CreateFromEntity(Domain.Entities.Survey.Survey survey)
    {
        return new SurveyDto
        {
            Id = survey.Id,
            Name = survey.Name,
            SurveyId = survey.SurveyId,
            CreatedAt = survey.CreatedAt,
            UpdatedAt = survey.UpdatedAt,
            LastSyncedAt = survey.LastSyncedAt,
            SurveyBlocks = survey.SurveyBlocks?.Select(SurveyElementDto.CreateFromEntity).ToList()
        };
    }
}