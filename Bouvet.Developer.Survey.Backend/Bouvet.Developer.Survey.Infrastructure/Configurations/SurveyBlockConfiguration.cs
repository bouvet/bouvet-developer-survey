using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class SurveyBlockConfiguration : IEntityTypeConfiguration<SurveyBlock>
{
    public void Configure(EntityTypeBuilder<SurveyBlock> builder)
    {
        builder.ToTable("SurveyBlocks");

        builder.HasKey(b => b.Id);

        builder.HasQueryFilter(b => b.DeletedAt == null);

        builder.HasMany(b => b.BlockElements)
            .WithOne(e => e.SurveyBlock)
            .HasForeignKey(e => e.SurveyElementGuid)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(b => b.Survey)
            .WithMany(s => s.SurveyBlocks)
            .HasForeignKey(b => b.SurveyGuid)
            .OnDelete(DeleteBehavior.NoAction);
    }
}