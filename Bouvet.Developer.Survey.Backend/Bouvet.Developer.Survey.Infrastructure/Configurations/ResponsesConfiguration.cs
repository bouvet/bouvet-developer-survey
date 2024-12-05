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

        builder.HasOne(r => r.Choice)
            .WithMany(c => c.Responses)
            .HasForeignKey(r => r.ChoiceId);

        builder.HasMany(r => r.ResponseUsers)
            .WithOne(rr => rr.Response)
            .HasForeignKey(rr => rr.ResponseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}