using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using Microsoft.EntityFrameworkCore;

using SurveyEntity = Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetSurvey;
using QuestionEntity = Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetQuestion;
using OptionEntity = Bouvet.Developer.Survey.Domain.Entities.Bouvet.BouvetOption;

namespace Bouvet.Developer.Survey.Infrastructure.Data
{
    public class BouvetSurveyContext : DbContext
    {
        public BouvetSurveyContext(DbContextOptions<BouvetSurveyContext> options) : base(options)
        {
        }

        public DbSet<BouvetSurveyStructure> BouvetSurveyStructures => Set<BouvetSurveyStructure>();
        public DbSet<BouvetSurvey> Surveys => Set<BouvetSurvey>();
        public DbSet<BouvetQuestion> Questions => Set<BouvetQuestion>();
        public DbSet<BouvetOption> Options => Set<BouvetOption>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SurveyEntity>().ToTable("Survey", "bouvet");
            modelBuilder.Entity<QuestionEntity>().ToTable("Question", "bouvet");
            modelBuilder.Entity<OptionEntity>().ToTable("Option", "bouvet");
            base.OnModelCreating(modelBuilder);
        }

    }
}
