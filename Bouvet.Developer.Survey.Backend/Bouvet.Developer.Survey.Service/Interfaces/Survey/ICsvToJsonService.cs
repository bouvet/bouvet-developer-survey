using Bouvet.Developer.Survey.Service.Survey;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey;

public interface ICsvToJsonService
{
    Task<string> ConvertCsvToJson(Stream csvStream);
    Task<IEnumerable<ExportTagDto>> GetQuestions(string surveyId);
    Task<IEnumerable<FieldDto>> GetQuestionsFromStream(Stream csvStream, string surveyId);
}