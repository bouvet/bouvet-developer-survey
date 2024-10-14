using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey;

public class DynamicFieldsConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        var dynamicFields = new Dictionary<string, string?>();
        foreach (var header in row.HeaderRecord)
        {
            if (!memberMapData.Member.Name.Equals(header) && !row.Context.Reader.HeaderRecord.Contains(header))
            {
                dynamicFields[header] = row.GetField(header);
            }
        }
        return dynamicFields;
    }
}