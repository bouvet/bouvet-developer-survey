using System.Text.Json.Serialization;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Import;

public class CsvDto
{
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Status { get; set; }
    public string? Progress { get; set; }
    public string? DurationInSeconds { get; set; } 
    public string? Finished { get; set; }
    public string? RecordedDate { get; set; }
    public string? ResponseId { get; set; }
    public string? DistributionChannel { get; set; }
    public string? UserLanguage { get; set; }
    public string? Alder { get; set; }
    public string? BouvetLokasjon { get; set; }
    public string? Enhet { get; set; }
    public string? Arbeidsrolle { get; set; }
    public string? Arbeidserfaring { get; set; }
}
