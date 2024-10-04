using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Infrastructure.Data;

public interface IDeveloperSurveyContext
{
    public DbSet<Domain.Entities.Survey.Survey> Surveys { get; }
    public DbSet<SurveyBlock> SurveyBlocks { get; }
    public DbSet<Question> Questions { get; }
    public DbSet<Choice> Choices { get; }
    public DbSet<BlockElement> BlockElements { get; }
}