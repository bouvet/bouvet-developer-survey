using System.Globalization;
using System.Text.Json;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using CsvHelper;

namespace Bouvet.Developer.Survey.Service.Survey.Results;


public class CsvToJsonService : ICsvToJsonService
{
    public async Task<string> ConvertCsvToJson(Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        await csv.ReadAsync();
        csv.ReadHeader();
        
        // Read CSV records into a list of dictionaries
        var records = new List<Dictionary<string, object>>();
        while (await csv.ReadAsync())
        {
            var record = new Dictionary<string, object>();
            foreach (var header in csv.HeaderRecord)
            {
                record[header] = csv.GetField(header); // Get the field value by header
            }
            records.Add(record);
        }
        
        // Serialize the records to JSON
        var json = JsonSerializer.Serialize(records, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        return json;
    }
}