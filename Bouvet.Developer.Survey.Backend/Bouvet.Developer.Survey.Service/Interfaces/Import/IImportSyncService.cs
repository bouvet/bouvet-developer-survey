using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;

namespace Bouvet.Developer.Survey.Service.Interfaces.Import;

public interface IImportSyncService
{
    Task AddSurveyElements(SurveyElementBlockDto surveyBlockDto, string surveyId);
    Task AddQuestions(SurveyQuestionsDto questionsDto, string surveyId);
    Task CheckForDifference(SurveyBlocksDto surveyBlockDto, Domain.Entities.Survey.Survey survey);
}