using System.Globalization;
using System.Text.Json;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Import;
using CsvHelper;

namespace Bouvet.Developer.Survey.Service.Survey;

public class CsvToJsonService : ICsvToJsonService
{
    public string ConvertCsvToJson(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<CsvDtoMap>();
            
            var records = csv.GetRecords<CsvDto>().ToList();
            var json = JsonSerializer.Serialize(records, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            return json;
        }
        
    }
}