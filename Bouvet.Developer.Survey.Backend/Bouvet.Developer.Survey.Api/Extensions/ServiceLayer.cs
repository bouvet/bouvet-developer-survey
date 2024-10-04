using Bouvet.Developer.Survey.Service.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Survey;

namespace Bouvet.Developer.Survey.Api.Extensions;

public static class ServiceLayer
{
    //All services are added here to the service collection
    public static void AddServices(this IServiceCollection services)
    {
        // services.AddTransient<ISurveyService, SurveyService>();
        // services.AddTransient<IBlockService, BlockService>();
        // services.AddTransient<IOptionService, OptionService>();
        services.AddTransient<ICsvToJsonService, CsvToJsonService>();
        services.AddTransient<IImportSurveyService, ImportSurveyService>();
    }
}