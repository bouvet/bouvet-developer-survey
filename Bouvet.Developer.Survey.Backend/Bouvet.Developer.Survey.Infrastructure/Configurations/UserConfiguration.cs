using Bouvet.Developer.Survey.Domain.Entities.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bouvet.Developer.Survey.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        
        builder.HasKey(u => u.Id);
        
        builder.HasQueryFilter(b => b.DeletedAt == null);
        
        builder.HasOne(u => u.Survey)
            .WithMany(s => s.Users)
            .HasForeignKey(u => u.SurveyId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(u => u.ResponseUsers)
            .WithOne(rr => rr.User)
            .HasForeignKey(rr => rr.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}