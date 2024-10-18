using Bouvet.Developer.Survey.Domain.Entities.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class ResponsesConfiguration : IEntityTypeConfiguration<Response>
{
    public void Configure(EntityTypeBuilder<Response> builder)
    {
        builder.ToTable("Responses");

        builder.HasKey(r => r.Id);

        builder.HasQueryFilter(r => r.DeletedAt == null);

        builder.HasOne(r => r.Choice)
            .WithMany(c => c.Responses)
            .HasForeignKey(r => r.ChoiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(r => r.AnswerOption)
            .WithMany(a => a.Responses)
            .HasForeignKey(r => r.AnswerOptionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}