namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey;

public class SurveyListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public static SurveyListDto CreateFromEntity(Domain.Entities.Survey survey)
    {
        return new SurveyListDto
        {
            Id = survey.Id,
            Name = survey.Name,
        };
    }
}