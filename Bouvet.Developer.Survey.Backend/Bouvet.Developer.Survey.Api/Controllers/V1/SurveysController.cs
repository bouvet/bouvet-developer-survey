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
    
    public SurveysController(ISurveyService surveyService, ICsvToJsonService csvToJsonService, 
        IImportSurveyService importSurveyService)
    {
        _surveyService = surveyService;
        _csvToJsonService = csvToJsonService;
        _importSurveyService = importSurveyService;
    }
    
    /// <summary>
    /// Get all surveys
    /// </summary>
    /// <returns>A list of all surveys</returns>
    /// <response code="200">Returns a list of all surveys</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
    [HttpGet]
    [SwaggerResponse(200, "Returns a list of all surveys", typeof(IEnumerable<SurveyDto>))]
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
    // [SwaggerResponse(200, "Returns a survey", typeof(SurveyDto))]
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
    [SwaggerResponse(201, "Survey created", typeof(SurveyDto))]
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
    
    // POST: api/file/upload
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded or file is empty.");
        }

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0;
            var survey = await _importSurveyService.UploadSurvey(stream);
            return Ok(survey);
        }
    }
    
    // POST: api/file/upload
    [HttpPost("uploadQuestions")]
    public async Task<IActionResult> UploadQuestions(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded or file is empty.");
        }

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0;
            var survey = await _importSurveyService.FindSurveyQuestions(stream);
            return Ok(survey);
        }
    }
    
    /// <summary>
    /// Import a survey from CSV
    /// </summary>
    [HttpPost("import")]
    public async Task<IActionResult> ImportCsv(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0;
            var json = _csvToJsonService.ConvertCsvToJson(stream);
            return Ok(json);
        }
    }
}