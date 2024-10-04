

using System.Text.Json.Serialization;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;

public class SurveyQuestionsDto
{
    public SurveyElementQuestionsDto[]? SurveyElements { get; set; }
}

public class SurveyElementQuestionsDto
{
    [JsonPropertyName("SurveyID")] 
    public string SurveyId { get; set; } = string.Empty;
    
    public string Element { get; set; } = string.Empty;
    public string PrimaryAttribute { get; set; } = string.Empty;
    public string SecondaryAttribute { get; set; } = string.Empty;
    public string TertiaryAttribute { get; set; } = string.Empty;
    
    public PayloadQuestionDto? Payload { get; set; }
}

public class PayloadQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;
    public string DataExportingTag { get; set; } = string.Empty;
    public string QuestionDescription { get; set; } = string.Empty;
    public Dictionary<string, ChoicesDto> Choices { get; set; } = new();
}

public class ChoicesDto
{
    public string Display { get; set; } = string.Empty;
}