using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Infrastructure.Data
{
    public class BouvetSurveyContext : DbContext
    {
        public BouvetSurveyContext(DbContextOptions<BouvetSurveyContext> options) : base(options) { }

        public DbSet<BouvetSurveyStructure> BouvetSurveyStructures => Set<BouvetSurveyStructure>();
        public DbSet<BouvetSurvey> Surveys => Set<BouvetSurvey>();
        public DbSet<BouvetQuestion> Questions => Set<BouvetQuestion>();
        public DbSet<BouvetOption> Options => Set<BouvetOption>();
        public DbSet<BouvetUser> Users => Set<BouvetUser>();
        public DbSet<BouvetResponse> Responses { get; set; } = null!;
        public DbSet<BouvetSection> Sections => Set<BouvetSection>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BouvetSurvey>().ToTable("Survey", "bouvet");
            modelBuilder.Entity<BouvetQuestion>().ToTable("Question", "bouvet");
            modelBuilder.Entity<BouvetOption>().ToTable("Option", "bouvet");
            modelBuilder.Entity<BouvetSection>().ToTable("Section", "bouvet");
            modelBuilder.Entity<BouvetUser>().ToTable("User", "bouvet");
            modelBuilder.Entity<BouvetResponse>().ToTable("Response", "bouvet");

            modelBuilder.Entity<BouvetSurvey>()
                .HasIndex(s => s.ExternalId)
                .IsUnique();

            modelBuilder.Entity<BouvetSection>()
                .HasIndex(s => new { s.SurveyId, s.ExternalId })
                .IsUnique();

            modelBuilder.Entity<BouvetSection>()
                .HasOne(s => s.Survey)
                .WithMany(s => s.Sections)
                .HasForeignKey(s => s.SurveyId);

            modelBuilder.Entity<BouvetQuestion>()
                .HasOne(q => q.Survey)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SurveyId);

            modelBuilder.Entity<BouvetQuestion>()
                .HasOne(q => q.Section)
                .WithMany()
                .HasForeignKey(q => q.SectionId)
                .IsRequired(false);

            modelBuilder.Entity<BouvetOption>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<BouvetOption>()
                .HasOne(o => o.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(o => o.QuestionId);

            modelBuilder.Entity<BouvetResponse>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<BouvetResponse>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Restrict); // prevent cascade

            modelBuilder.Entity<BouvetResponse>()
                .HasOne(r => r.Question)
                .WithMany(q => q.Responses)
                .HasForeignKey(r => r.QuestionId)
                .HasPrincipalKey(q => q.Id)
                .OnDelete(DeleteBehavior.Cascade); // allow cascade

            modelBuilder.Entity<BouvetResponse>()
                .HasOne(r => r.Option)
                .WithMany()
                .HasForeignKey(r => r.OptionId)
                .HasPrincipalKey(o => o.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); // prevent cascade

            modelBuilder.Entity<BouvetUser>()
                .HasOne(u => u.Survey)
                .WithMany()
                .HasForeignKey(u => u.SurveyId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

