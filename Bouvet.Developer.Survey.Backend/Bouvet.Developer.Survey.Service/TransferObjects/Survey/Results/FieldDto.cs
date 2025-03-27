namespace Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;

public class ExportTagDto
{
    public string? DataExportTag { get; set; }
}

public class FieldDto
{
    public string? ResponseId { get; set; }
    public string? FieldName { get; set; }
    public string? Value { get; set; }
}