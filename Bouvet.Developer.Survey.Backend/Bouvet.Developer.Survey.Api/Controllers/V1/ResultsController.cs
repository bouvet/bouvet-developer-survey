using Asp.Versioning;
using Bouvet.Developer.Survey.Api.Constants;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Ai;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Response;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bouvet.Developer.Survey.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
// If you want to restrict access to a specific role, uncomment the following line and replace "YourRoleName" with the role name
// [Authorize(Roles = RoleConstants.ReadRole)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly ISurveyService _surveyService;
    private readonly IQuestionService _questionService;
    private readonly IUserService _userService;

    public ResultsController(ISurveyService surveyService, IQuestionService questionService, IUserService userService)
    {
        _surveyService = surveyService;
        _questionService = questionService;
        _userService = userService;
    }

    /// <summary>
    /// Get all surveys
    /// </summary>
    /// <returns>A list of all surveys</returns>
    /// <response code="200">Returns a list of all surveys</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
    [HttpGet]
    [SwaggerResponse(200, "Returns a list of all surveys", typeof(IEnumerable<SurveysDto>))]
    public async Task<IActionResult> GetSurveys()
    {
        var surveys = await _surveyService.GetSurveysAsync();
        return Ok(surveys);
    }

    /// <summary>
    /// Get a survey by id
    /// </summary>
    /// <returns>A survey</returns>
    /// <response code="200">Returns a surveys</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
    [HttpGet("{surveyId:guid}")]
    [SwaggerResponse(200, "Returns a survey", typeof(SurveyDto))]
    public async Task<IActionResult> GetSurvey(Guid surveyId)
    {
        var survey = await _surveyService.GetSurveyAsync(surveyId);
        return Ok(survey);
    }

    /// <summary>
    ///  Get survey question by id and their responses
    /// </summary>
    /// <param name="questionId">The question guid</param>
    /// <returns>A question and their responses</returns>
    /// <response code="200">Returns a question and their responses</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
    [HttpGet("GetQuestionById/{questionId:guid}")]
    [SwaggerResponse(200, "Returns a question and their responses", typeof(QuestionResponseDto))]
    public async Task<IActionResult> GetQuestionById(Guid questionId)
    {
        var question = await _questionService.GetQuestionByIdAsync(questionId);
        return Ok(question);
    }

    /// <summary>
    /// Get a survey by year
    /// </summary>
    /// <returns>First survey for that year survey</returns>
    /// <response code="200">Returns a surveys</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
    [HttpGet("{year:int}")]
    [SwaggerResponse(200, "Returns a survey", typeof(SurveyDto))]
    public async Task<IActionResult> GetSurveyByYear(int year)
    {
        var survey = await _surveyService.GetSurveyByYearAsync(year);
        return Ok(survey);
    }

    /// <summary>
    /// Get all response to a user to a survey
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <returns>A user response</returns>
    /// <response code="200">Returns a user response</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
    [HttpGet("GetUserResponse/{userId:guid}")]
    [SwaggerResponse(200, "Returns a user response", typeof(UserResponseDto))]
    public async Task<IActionResult> GetUserResponse(Guid userId)
    {
        var userResponse = await _userService.GetUserResponses(userId);
        return Ok(userResponse);
    }
}
