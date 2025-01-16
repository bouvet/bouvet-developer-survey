namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class SurveysDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Year { get; set; } = null!;
    public string SurveyId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    public static SurveysDto CreateFromEntity(Domain.Entities.Survey.Survey survey)
    {
        return new SurveysDto
        {
            Id = survey.Id,
            Name = survey.Name,
            Year = survey.Year,
            SurveyId = survey.SurveyId,
            CreatedAt = survey.CreatedAt,
            UpdatedAt = survey.UpdatedAt,
            LastSyncedAt = survey.LastSyncedAt,
        };
    }
}
