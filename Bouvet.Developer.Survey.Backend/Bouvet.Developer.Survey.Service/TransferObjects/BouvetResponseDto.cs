using System.Text.Json.Serialization;
using System.Collections.Generic;

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

    [JsonPropertyName("freeTextAnswer")]
    public string? FreeTextAnswer { get; set; }

    [JsonPropertyName("likertAnswers")]
    public List<LikertAnswerDto>? LikertAnswers { get; set; }
}

public class LikertAnswerDto
{
    [JsonPropertyName("optionId")]
    public string OptionId { get; set; } = default!;

    [JsonPropertyName("admired")]
    public bool Admired { get; set; }

    [JsonPropertyName("desired")]
    public bool Desired { get; set; }
}