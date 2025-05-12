using System.IO;
using System.Threading.Tasks;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structure
{
    public interface ISurveyStructureService
    {
        Task<SurveyExportDto?> GetSurveyStructureByYearAsync(int year);
        Task UnpackSurveyStructureAsync(Stream jsonStream);
    }
}
