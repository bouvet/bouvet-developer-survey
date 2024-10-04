using CsvHelper.Configuration;

namespace Bouvet.Developer.Survey.Service.TransferObjects.Import;

public sealed class CsvDtoMap : ClassMap<CsvDto>
{
    public CsvDtoMap()
    {
        // Map CSV headers to properties
        Map(m => m.StartDate).Name("StartDate");
        Map(m => m.EndDate).Name("EndDate");
        Map(m => m.Status).Name("Status");
        Map(m => m.Progress).Name("Progress");
        Map(m => m.DurationInSeconds).Name("Duration (in seconds)");
        Map(m => m.Finished).Name("Finished");
        Map(m => m.RecordedDate).Name("RecordedDate");
        Map(m => m.ResponseId).Name("ResponseId");
        Map(m => m.DistributionChannel).Name("DistributionChannel");
        Map(m => m.UserLanguage).Name("UserLanguage");
        Map(m => m.Alder).Name("Alder");
        Map(m => m.BouvetLokasjon).Name("Bouvet lokasjon");
        Map(m => m.Enhet).Name("Enhet");
        Map(m => m.Arbeidsrolle).Name("Arbeidsrolle");
        Map(m => m.Arbeidserfaring).Name("Arbeidserfaring");
    }
}