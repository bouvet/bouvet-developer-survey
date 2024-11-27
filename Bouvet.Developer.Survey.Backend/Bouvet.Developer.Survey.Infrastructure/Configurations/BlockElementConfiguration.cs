using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class BlockElementConfiguration : IEntityTypeConfiguration<BlockElement>
{
    public void Configure(EntityTypeBuilder<BlockElement> builder)
    {
        builder.ToTable("BlockElements");
        
        builder.HasKey(e => e.Id);
        
        builder.HasQueryFilter(e => e.DeletedAt == null);
        
        builder.HasMany(e => e.Questions)
            .WithOne(c => c.BlockElement)
            .HasForeignKey(c => c.BlockElementId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(e => e.SurveyBlock)
            .WithMany(b => b.BlockElements)
            .HasForeignKey(e => e.SurveyElementGuid)
            .OnDelete(DeleteBehavior.NoAction);
    }
}