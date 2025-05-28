public class SurveyUploadDto
{
    public string Id { get; set; } = ""; // maps to Survey.ExternalId
    public string Year { get; set; } = "";
    public string Title { get; set; } = "";
    public string StartDate { get; set; } = "";
    public string EndDate { get; set; } = "";
    public List<SectionDto> Sections { get; set; } = new();
    public List<QuestionDto> Questions { get; set; } = new();
}

public class SectionDto
{
    public string Id { get; set; } = ""; // maps to Section.ExternalId
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}

public class QuestionDto
{
    public string Id { get; set; } = ""; // maps to Question.ExternalId
    public string Type { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string? SectionId { get; set; } // maps to Section.ExternalId
    public List<OptionDto>? Options { get; set; }
}

public class OptionDto
{
    public string Id { get; set; } = ""; // maps to Option.ExternalId
    public string Value { get; set; } = "";
}
