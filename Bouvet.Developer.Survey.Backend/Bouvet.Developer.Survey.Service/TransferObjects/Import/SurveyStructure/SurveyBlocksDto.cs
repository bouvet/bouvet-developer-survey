
namespace Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;

public class SurveyBlocksDto
{
    public required SurveyEntryDto SurveyEntry { get; set; }
    public SurveyElementBlockDto[]? SurveyElements { get; set; }
}