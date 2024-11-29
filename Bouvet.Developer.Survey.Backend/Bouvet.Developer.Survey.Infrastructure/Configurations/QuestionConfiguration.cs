using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");
        
        builder.HasKey(q => q.Id);
        
        builder.HasQueryFilter(q => q.DeletedAt == null);
        
        builder.HasMany(q => q.Choices)
            .WithOne(o => o.Question)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(q => q.BlockElement)
            .WithMany(e => e.Questions)
            .HasForeignKey(q => q.BlockElementId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(q => q.ResponseUsers)
            .WithOne(rr => rr.Question)
            .HasForeignKey(rr => rr.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(q => q.AiAnalyse)
            .WithOne(a => a.Question)
            .HasForeignKey<AiAnalyse>(a => a.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}