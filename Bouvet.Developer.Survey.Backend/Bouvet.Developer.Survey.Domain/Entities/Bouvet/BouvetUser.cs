using Bouvet.Developer.Survey.Domain.Entities.Results;

namespace Bouvet.Developer.Survey.Domain.Entities.Bouvet
{
    public class BouvetUser
    {
        public Guid Id { get; set; }
        public string RespondId { get; set; } = default!;

        public int SurveyId { get; set; }
        public BouvetSurvey Survey { get; set; } = default!;

        public DateTimeOffset CreatedAt { get; set; }
    }

}