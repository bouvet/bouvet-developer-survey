using Bouvet.Developer.Survey.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Infrastructure.Data;

public interface IDeveloperSurveyContext
{
    public DbSet<Domain.Entities.Survey> Surveys { get; }
    public DbSet<Block> Blocks { get; }
    public DbSet<Option> Options { get; }
    public DbSet<AiAnalyse> AiAnalyses { get; }
}