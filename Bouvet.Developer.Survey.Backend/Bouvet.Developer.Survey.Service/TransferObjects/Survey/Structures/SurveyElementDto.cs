using Bouvet.Developer.Survey.Domain.Entities.Survey;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

public class SurveyElementDto
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public virtual ICollection<BlockElementDto>? BlockElements { get; set; }
    
    public static SurveyElementDto CreateFromEntity(SurveyBlock surveyBlock)
    {
        return new SurveyElementDto
        {
            Id = surveyBlock.Id,
            SurveyId = surveyBlock.SurveyGuid,
            Type = surveyBlock.Type,
            Description = surveyBlock.Description,
            CreatedAt = surveyBlock.CreatedAt,
            UpdatedAt = surveyBlock.UpdatedAt,
            BlockElements = surveyBlock.BlockElements?.Select(BlockElementDto.CreateFromEntity).ToList()
        };
    }
}