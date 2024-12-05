using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Ai;

public class AiAnalyseDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    
    
    public static AiAnalyseDto? CreateFromEntity(AiAnalyse aiAnalyse)
    {
        return new AiAnalyseDto
        {
            Id = aiAnalyse.Id,
            Text = aiAnalyse.Text
        };
    }
}