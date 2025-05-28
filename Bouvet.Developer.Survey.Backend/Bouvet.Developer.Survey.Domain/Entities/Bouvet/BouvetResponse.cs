namespace Bouvet.Developer.Survey.Domain.Entities.Bouvet
{
    public class BouvetResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public BouvetUser User { get; set; } = null!;

        public int QuestionId { get; set; }
        public BouvetQuestion Question { get; set; } = null!;

        public int? OptionId { get; set; } // ✅ Match Option.Id
        public BouvetOption? Option { get; set; }

        public bool? HasWorkedWith { get; set; } // for likert
        public bool? WantsToWorkWith { get; set; } // for likert

        public string? FreeTextAnswer { get; set; } // New property for free-text

        public DateTimeOffset CreatedAt { get; set; }
    }
}
