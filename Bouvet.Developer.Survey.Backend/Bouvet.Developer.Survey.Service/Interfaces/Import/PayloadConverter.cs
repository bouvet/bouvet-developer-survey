using System.Text.Json;
using System.Text.Json.Serialization;
using Bouvet.Developer.Survey.Service.TransferObjects.Import;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;

namespace Bouvet.Developer.Survey.Service.Interfaces.Import;
public class PayloadConverter : JsonConverter<Dictionary<string, DictionaryPayload>>
{
    public override Dictionary<string, DictionaryPayload>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token for Payload.");
        }

        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;

        // Check if the object contains keys that are numeric strings like "1", "2", etc.
        var isValidPayload = root.EnumerateObject().All(p => int.TryParse(p.Name, out _));

        // If valid, deserialize to the expected Dictionary
        if (!isValidPayload) return null;
        
        
        return JsonSerializer.Deserialize<Dictionary<string, DictionaryPayload>>(root.GetRawText(), options);
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<string, DictionaryPayload> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }
}