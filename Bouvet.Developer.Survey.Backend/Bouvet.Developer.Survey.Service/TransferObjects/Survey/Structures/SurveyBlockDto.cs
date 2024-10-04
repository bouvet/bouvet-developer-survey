using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class SurveyBlockDto
{
    public Guid Id { get; set; }
    public string SurveyId { get; set; } = null!;
    public string? Type { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string SurveyBlockId { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? LastSyncedAt { get; set; }
    
    public static SurveyBlockDto CreateFromEntity(SurveyBlock surveyBlock)
    {
        return new SurveyBlockDto
        {
            Id = surveyBlock.Id,
            SurveyId = surveyBlock.SurveyId,
            Type = surveyBlock.Type,
            Description = surveyBlock.Description,
            SurveyBlockId = surveyBlock.SurveyBlockId,
            CreatedAt = surveyBlock.CreatedAt,
            UpdatedAt = surveyBlock.UpdatedAt,
            LastSyncedAt = surveyBlock.LastSyncedAt,
        };
    }
}