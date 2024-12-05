using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
{
    public void Configure(EntityTypeBuilder<Choice> builder)
    {
        builder.ToTable("Choices");
        
        builder.HasKey(c => c.Id);
        
        builder.HasQueryFilter(c => c.DeletedAt == null);
        
        builder.HasOne(c => c.Question)
            .WithMany(q => q.Choices)
            .HasForeignKey(c => c.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(c => c.Responses)
            .WithOne(r => r.Choice)
            .HasForeignKey(r => r.ChoiceId)
            .IsRequired(false);
    }
}