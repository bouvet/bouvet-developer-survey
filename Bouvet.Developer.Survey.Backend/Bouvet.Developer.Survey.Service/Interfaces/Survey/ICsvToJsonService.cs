namespace Bouvet.Developer.Survey.Service.Interfaces.Survey;

public interface ICsvToJsonService
{
    string ConvertCsvToJson(Stream csvStream);
}