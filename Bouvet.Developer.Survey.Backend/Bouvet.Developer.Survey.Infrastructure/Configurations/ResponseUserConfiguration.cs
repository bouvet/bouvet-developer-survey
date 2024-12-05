using Bouvet.Developer.Survey.Domain.Entities.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class ResponseUserConfiguration : IEntityTypeConfiguration<ResponseUser>
{
    public void Configure(EntityTypeBuilder<ResponseUser> builder)
    {
        builder.ToTable("ResponseUsers");
        
        builder.HasKey(rr => new { rr.ResponseId, rr.UserId });
        
        builder.HasQueryFilter(b => b.DeletedAt == null);
        
        builder.HasOne(rr => rr.Response)
            .WithMany(r => r.ResponseUsers)
            .HasForeignKey(rr => rr.ResponseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(rr => rr.User)
            .WithMany(u => u.ResponseUsers)
            .HasForeignKey(rr => rr.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(rr => rr.Question)
            .WithMany(q => q.ResponseUsers)
            .HasForeignKey(rr => rr.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}