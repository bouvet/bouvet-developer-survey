public class SurveyExportDto
{
    public string Id { get; set; } = ""; // maps from Survey.ExternalId
    public string Title { get; set; } = "";
    public string StartDate { get; set; } = "";
    public string EndDate { get; set; } = "";
    public int Year { get; set; }
    public List<SectionExportDto> Sections { get; set; } = new();
    public List<QuestionExportDto> Questions { get; set; } = new();
}

public class SectionExportDto
{
    public string Id { get; set; } = ""; // maps from Section.ExternalId
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}

public class QuestionExportDto
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Type { get; set; } = "";
    public string Description { get; set; } = "";
    public string? SectionId { get; set; }
    public bool? Required = false;
    public List<OptionExportDto> Options { get; set; } = new();
    public List<string>? Columns { get; set; } // <–– NEW for likert
}


public class OptionExportDto
{
    public string Id { get; set; } = ""; // maps from Option.ExternalId
    public string Value { get; set; } = "";
}
