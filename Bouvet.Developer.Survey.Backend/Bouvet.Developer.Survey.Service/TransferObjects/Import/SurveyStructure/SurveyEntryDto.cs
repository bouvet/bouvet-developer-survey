using System.Text.Json.Serialization;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;

public class SurveyEntryDto
{
    [JsonPropertyName("SurveyID")]
    public string SurveyId { get; set; } = string.Empty;
    public string SurveyName { get; set; } = string.Empty;
    public string SurveyLanguage { get; set; } = string.Empty;
    public string? SurveyCreationDate { get; set; }
    public string? LastModified { get; set; }
    public bool? Deleted { get; set; } 
}