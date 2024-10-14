using Asp.Versioning;
using Bouvet.Developer.Survey.Api.Constants;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
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
    private readonly ICsvToJsonService _csvToJsonService;
    private readonly IImportSurveyService _importSurveyService;
    private readonly IQuestionService _questionService;
    
    public SurveysController(ISurveyService surveyService, ICsvToJsonService csvToJsonService, 
        IImportSurveyService importSurveyService, IQuestionService questionService)
    {
        _surveyService = surveyService;
        _csvToJsonService = csvToJsonService;
        _importSurveyService = importSurveyService;
        _questionService = questionService;
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
    
    [HttpGet("GetQuestionsSurvey")]
    public async Task<IActionResult> GetQuestionsSurvey(string surveyId)
    {
        // var survey = await _questionService.GetQuestionsBySurveyIdAsync(surveyId);
        var survey = await _csvToJsonService.GetQuestions(surveyId);
        return Ok(survey);
    }
    
    /// <summary>
    /// Import a survey from JSON
    /// </summary>
    /// <param name="file">The file to upload</param>
    [HttpPost("ImportSurvey")]
    [SwaggerResponse(200, "Survey created")]
    public async Task<IActionResult> ImportSurvey(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded or file is empty.");
        }

        try
        {
            using var stream = new MemoryStream();

            await file.CopyToAsync(stream);
            stream.Position = 0;
            await _importSurveyService.UploadSurvey(stream);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    /// Import a survey from CSV
    /// </summary>
    [HttpPost("import")]
    public async Task<IActionResult> ImportCsv(IFormFile file, string surveyId)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0;
            var json = await _csvToJsonService.GetQuestionsFromStream(stream, surveyId);
            return Ok(json);
        }
    }
}