using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface ICsvToJsonService
{
    Task<string> ConvertCsvToJson(Stream csvStream);
}