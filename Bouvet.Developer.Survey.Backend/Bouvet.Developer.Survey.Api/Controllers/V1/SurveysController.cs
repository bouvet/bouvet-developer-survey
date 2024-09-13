using Asp.Versioning;
using Bouvet.Developer.Survey.Api.Constants;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bouvet.Developer.Survey.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Authorize(Roles = RoleConstants.ReadRole)]
[Route("api/v{version:apiVersion}/[controller]")]
public class SurveysController : ControllerBase
{
    private readonly ISurveyService _surveyService;
    
    public SurveysController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }
    
    /// <summary>
    /// Get all surveys
    /// </summary>
    /// <returns>A list of all surveys</returns>
    /// <response code="200">Returns a list of all surveys</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
    [HttpGet]
    [SwaggerResponse(200, "Returns a list of all surveys", typeof(IEnumerable<SurveyListDto>))]
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
    /// Create a survey
    /// </summary>
    /// <returns>Survey created</returns>
    /// <response code="200">Survey created</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to write</response>
    [HttpPost]
    [Authorize(Roles = RoleConstants.WriteRole)]
    [SwaggerResponse(201, "Survey created", typeof(SurveyListDto))]
    public async Task<IActionResult> CreateSurvey([FromBody] NewSurveyDto newSurveyDto)
    {
        try
        {
            await _surveyService.CreateSurveyAsync(newSurveyDto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}