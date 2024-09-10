using Bouvet.Developer.Survey.Domain.Entities;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class BlockDto
{
    public string Question { get; set; } = null!;
    public string Type { get; set; } = null!;
    public Guid SurveyId { get; set; }
    
    public static Block CreateFromEntity(BlockDto block)
    {
        return new Block
        {
            Question = block.Question,
            Type = block.Type,
            SurveyId = block.SurveyId,
        };
    }
}

