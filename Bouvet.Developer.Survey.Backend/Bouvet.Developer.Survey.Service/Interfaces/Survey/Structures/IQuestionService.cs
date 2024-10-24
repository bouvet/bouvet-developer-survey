using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;

public interface IQuestionService
{
    /// <summary>
    /// Create a new question
    /// </summary>
    /// <param name="newQuestionDto">New question dto</param>
    /// <returns>Question dto</returns>
    Task<QuestionDetailsDto> CreateQuestionAsync(NewQuestionDto newQuestionDto);
    
    /// <summary>
    /// Get question by id
    /// </summary>
    /// <param name="questionId">The question id</param>
    /// <returns>Question dto</returns>
    Task<QuestionResponseDto> GetQuestionByIdAsync(Guid questionId);

    /// <summary>
    /// Get a list of questions by survey id
    /// </summary>
    /// <param name="surveyId">String of survey id </param>
    /// <returns></returns>
    Task<IEnumerable<QuestionDetailsDto>> GetQuestionsBySurveyIdAsync(string surveyId);

    /// <summary>
    /// Check for differences in questions
    /// </summary>
    /// <param name="surveyQuestionsDto">The survey questions dto</param>
    /// <param name="survey">The survey</param>
    /// <returns>Task</returns>
    Task CheckForDifference(SurveyQuestionsDto surveyQuestionsDto, Domain.Entities.Survey.Survey survey);
    
    /// <summary>
    /// Get all questions in a survey block
    /// </summary>
    /// <param name="surveyBlockId">The survey block id</param>
    /// <returns>A list of available questions</returns>
    Task<IEnumerable<QuestionDetailsDto>> GetQuestionsBySurveyBlockIdAsync(Guid surveyBlockId);
    
    /// <summary>
    /// Update question
    /// </summary>
    /// <param name="questionId">Guid of the question</param>
    /// <param name="updateQuestionDto">The updated question dto</param>
    /// <returns>Question dto</returns>
    Task<QuestionDetailsDto> UpdateQuestionAsync(Guid questionId, NewQuestionDto updateQuestionDto);
    
    /// <summary>
    /// Set question to deleted
    /// </summary>
    /// <param name="questionId">The question id</param>
    /// <returns>Task</returns>
    Task DeleteQuestionAsync(Guid questionId);
}