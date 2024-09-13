using Bouvet.Developer.Survey.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Infrastructure.Data;

public class DeveloperSurveyContext : DbContext, IDeveloperSurveyContext
{
    public DeveloperSurveyContext(DbContextOptions<DeveloperSurveyContext> options) : base(options)
    {
    }
    
    public DbSet<Domain.Entities.Survey> Surveys => Set<Domain.Entities.Survey>();
    public DbSet<Block> Blocks => Set<Block>();
    public DbSet<Option> Options => Set<Option>();
    public DbSet<AiAnalyse> AiAnalyses => Set<AiAnalyse>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeveloperSurveyContext).Assembly, type => 
            type.Namespace != null && type.Namespace.Contains("Bouvet.Developer.Survey.Infrastructure.Configurations"));
    }
}