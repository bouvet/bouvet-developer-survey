using Bouvet.Developer.Survey.Service.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey;
using Bouvet.Developer.Survey.Service.Survey.Results;
using Bouvet.Developer.Survey.Service.Survey.Structures;

namespace Bouvet.Developer.Survey.Api.Extensions;

public static class ServiceLayer
{
    //All services are added here to the service collection
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<ICsvToJsonService, CsvToJsonService>();
        services.AddTransient<IImportSurveyService, ImportSurveyService>();
        services.AddTransient<ISurveyService, SurveyService>();
        services.AddTransient<ISurveyBlockService, SurveyBlockService>();
        services.AddTransient<IQuestionService, QuestionService>();
        services.AddTransient<IChoiceService, ChoiceService>();
        services.AddTransient<IBlockElementService, BlockElementService>();
        services.AddTransient<IAnswerOptionService, AnswerOptionService>();
        services.AddTransient<IResponseService, ResponseService>();
        services.AddTransient<IResultService, ResultService>();
        services.AddTransient<IUserService, UserService>();
    }
}