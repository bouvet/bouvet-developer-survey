using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;

public interface IResultService
{
    Task<List<NewResponseDto>> SummarizeFields(List<FieldDto> fields, IEnumerable<QuestionDetailsDto> questions,
        Domain.Entities.Survey.Survey survey);
    Task<IEnumerable<ExportTagDto>> GetQuestions(string surveyId);
}