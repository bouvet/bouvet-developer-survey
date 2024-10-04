using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class QuestionService : IQuestionService
{
    public Task<QuestionDto> CreateQuestionAsync(NewQuestionDto newQuestionDto)
    {
        throw new NotImplementedException();
    }

    public Task<QuestionDto> GetQuestionByIdAsync(Guid questionId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<QuestionDto>> GetQuestionsBySurveyBlockIdAsync(Guid surveyBlockId)
    {
        throw new NotImplementedException();
    }

    public Task<QuestionDto> UpdateQuestionAsync(Guid questionId, NewQuestionDto updateQuestionDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteQuestionAsync(Guid questionId)
    {
        throw new NotImplementedException();
    }
}