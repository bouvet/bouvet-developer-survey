using Bouvet.Developer.Survey.Domain.Entities;
using Bouvet.Developer.Survey.Domain.Entities.Results;
using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Infrastructure.Data;

public class DeveloperSurveyContext : DbContext, IDeveloperSurveyContext
{
    public DeveloperSurveyContext(DbContextOptions<DeveloperSurveyContext> options) : base(options)
    {
    }
    
    public DbSet<Domain.Entities.Survey.Survey> Surveys => Set<Domain.Entities.Survey.Survey>();
    public DbSet<SurveyBlock> SurveyBlocks => Set<SurveyBlock>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Choice> Choices => Set<Choice>();
    public DbSet<BlockElement> BlockElements => Set<BlockElement>();
    public DbSet<AnswerOption> AnswerOptions => Set<AnswerOption>();
    public DbSet<Response> Responses => Set<Response>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeveloperSurveyContext).Assembly, type => 
            type.Namespace != null && type.Namespace.Contains("Bouvet.Developer.Survey.Infrastructure.Configurations"));
    }
}