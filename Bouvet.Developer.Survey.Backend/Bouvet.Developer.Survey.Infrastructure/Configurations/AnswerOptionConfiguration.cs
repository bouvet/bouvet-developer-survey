using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> builder)
    {
        builder.ToTable("AnswerOptions");

        builder.HasKey(a => a.Id);

        builder.HasQueryFilter(a => a.DeletedAt == null);

        builder.HasOne(a => a.Survey)
            .WithMany(s => s.AnswerOptions)
            .HasForeignKey(a => a.SurveyId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(a => a.Responses)
            .WithOne(r => r.AnswerOption)
            .HasForeignKey(r => r.AnswerOptionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}