using Bouvet.Developer.Survey.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class AiAnalyseConfiguration : IEntityTypeConfiguration<AiAnalyse>
{
    public void Configure(EntityTypeBuilder<AiAnalyse> builder)
    {
        builder.ToTable("AiAnalyses");

        builder.HasKey(a => a.Id);

        builder.HasQueryFilter(a => a.DeletedAt == null);
    }
}