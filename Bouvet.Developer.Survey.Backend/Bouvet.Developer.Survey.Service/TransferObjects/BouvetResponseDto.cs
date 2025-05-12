using System.Text.Json.Serialization;

public class BouvetSurveyResponseDto
{
    [JsonPropertyName("respondentId")]
    public string RespondentId { get; set; } = default!;

    [JsonPropertyName("surveyId")]
    public string SurveyExternalId { get; set; } = default!;

    [JsonPropertyName("answers")]
    public List<BouvetAnswerDto> Answers { get; set; } = new();
}

public class BouvetAnswerDto
{
    [JsonPropertyName("questionId")]
    public string QuestionExternalId { get; set; } = default!;

    [JsonPropertyName("optionIds")]
    public List<string> OptionExternalIds { get; set; } = new();
}