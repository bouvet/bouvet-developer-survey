
namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface ICsvToJsonService
{
    /// <summary>
    /// Convert a CSV file to JSON
    /// </summary>
    /// <param name="csvStream">The CSV file to convert</param>
    /// <returns>The JSON representation of the CSV file</returns>
    Task<string> ConvertCsvToJson(Stream csvStream);
}