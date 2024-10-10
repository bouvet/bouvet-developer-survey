using System.Text.Json.Serialization;
using Bouvet.Developer.Survey.Service.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Import;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;

public class SurveyElementBlockDto
{
    [JsonPropertyName("SurveyID")]
    public string SurveyId { get; set; } = string.Empty;
    public string Element { get; set; } = string.Empty;
    public string PrimaryAttribute { get; set; } = string.Empty;
    public string SecondaryAttribute { get; set; } = string.Empty;
    public string TertiaryAttribute { get; set; } = string.Empty;
    
    [JsonConverter(typeof(PayloadConverter))]
    public Dictionary<string, DictionaryPayload> Payload { get; set; }
}

public class DictionaryPayload
{
    public string Type { get; set; } = string.Empty;
    public string SubType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("ID")]
    public string Id { get; set; } = string.Empty;
    public List<SurveyBlockElementDto> BlockElements { get; set; } = [];
}

public class SurveyBlockElementDto
{
    public string Type { get; set; } = string.Empty;
    
    [JsonPropertyName("QuestionID")]
    public string QuestionId { get; set; } = string.Empty;
}
