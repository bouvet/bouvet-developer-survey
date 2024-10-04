using Bouvet.Developer.Survey.Service.TransferObjects.Import;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;

namespace Bouvet.Developer.Survey.Service.Interfaces.Import;

public interface IImportSurveyService
{
    Task<SurveyBlocksDto> FindSurveyBlocks(Stream stream);
    Task<SurveyQuestionsDto> FindSurveyQuestions(Stream stream);
}