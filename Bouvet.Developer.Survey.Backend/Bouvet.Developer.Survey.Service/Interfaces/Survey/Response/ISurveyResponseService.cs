using Bouvet.Developer.Survey.Service.TransferObjects.Survey; // For SurveyExportDto
using System.Threading.Tasks;

// If ISurveyResponseService is in Bouvet.Developer.Survey.Service.Interfaces.Survey.Response namespace:
namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Response
{
    public interface ISurveyResponseService
    {
        Task<SurveyExportDto?> GetSurveyStructureRelationalAsync(int year);

        // Use the existing BouvetSurveyResponseDto from #file:BouvetResponseDto.cs
        Task SubmitResponseAsync(BouvetSurveyResponseDto dto);
    }
}
