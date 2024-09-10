using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class SurveyConfiguration : IEntityTypeConfiguration<Domain.Entities.Survey>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Survey> builder)
    {
        builder.ToTable("Surveys");

        builder.HasKey(s => s.Id);
        
        builder.HasQueryFilter(s => s.DeletedAt == null);
        
        builder.HasMany(s => s.Blocks)
            .WithOne(b => b.Survey)
            .HasForeignKey(b => b.SurveyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}