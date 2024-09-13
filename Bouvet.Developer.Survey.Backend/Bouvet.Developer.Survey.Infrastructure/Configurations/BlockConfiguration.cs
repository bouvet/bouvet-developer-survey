using Bouvet.Developer.Survey.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.ToTable("Blocks");

        builder.HasKey(b => b.Id);

        builder.HasQueryFilter(b => b.DeletedAt == null);

        builder.HasMany(b => b.Options)
            .WithOne(o => o.Block)
            .HasForeignKey(o => o.BlockId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(b => b.AiAnalyse)
            .WithOne(a => a.Block)
            .HasForeignKey<AiAnalyse>(a => a.BlockId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}