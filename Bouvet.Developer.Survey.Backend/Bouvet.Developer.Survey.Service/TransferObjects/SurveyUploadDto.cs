public class SurveyUploadDto
{
    public string Title { get; set; }
    public string Id { get; set; }  // Could be year as string
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public List<SectionDto> Sections { get; set; }
}

public class SectionDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<QuestionDto> Questions { get; set; }
}

public class QuestionDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<OptionDto>? Options { get; set; }
}

public class OptionDto
{
    public string Id { get; set; }
    public string Value { get; set; }
}
