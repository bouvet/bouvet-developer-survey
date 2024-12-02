using Bouvet.Developer.Survey.Domain.Entities.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class AiAnalyseConfiguration : IEntityTypeConfiguration<AiAnalyse>
{
    public void Configure(EntityTypeBuilder<AiAnalyse> builder)
    {
        builder.ToTable("AiAnalyses");
        
        builder.HasKey(a => a.Id);
        
        builder.HasQueryFilter(b => b.DeletedAt == null);
        
        builder.HasOne(a => a.Question)
            .WithOne(q => q.AiAnalyse)
            .HasForeignKey<AiAnalyse>(a => a.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}