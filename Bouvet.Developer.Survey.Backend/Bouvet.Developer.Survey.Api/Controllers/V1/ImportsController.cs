using Asp.Versioning;
using Bouvet.Developer.Survey.Api.Constants;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bouvet.Developer.Survey.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Authorize(Roles = RoleConstants.ReadRole)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ImportsController : ControllerBase
{
    private readonly IImportSurveyService _importSurveyService;
    private readonly ICsvToJsonService _csvToJsonService;
    
    public ImportsController(IImportSurveyService importSurveyService, ICsvToJsonService csvToJsonService)
    {
        _importSurveyService = importSurveyService;
        _csvToJsonService = csvToJsonService;
    }
    
    [HttpPost("csvToJson")]
    public async Task<IActionResult> CsvToJson(IFormFile file)
    {
        if (file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;
        try
        {
            var json = await _csvToJsonService.ConvertCsvToJson(stream);

            // Convert the JSON string to a byte array
            var fileBytes = System.Text.Encoding.UTF8.GetBytes(json);
        
            // Return the JSON as a file download
            return File(fileBytes, "application/json", "result.json");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    
    /// <summary>
    /// Import a survey structure from json
    /// </summary>
    /// <param name="file">The file to upload</param>
    /// <response code="200">Success</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
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
    /// Import survey results from csv
    /// </summary>
    /// <param name="file">The file to upload</param>
    /// <param name="surveyGuid">The survey Guid</param>
    /// <response code="200">Success</response>
    /// <response code="401">If user is not authorized</response>
    /// <response code="403">User not authorized to view</response>
    [HttpPost("import")]
    public async Task<IActionResult> ImportCsv(IFormFile file, Guid surveyGuid)
    {
        if (file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;
        try
        {
            await _importSurveyService.GetQuestionsFromStream(stream, surveyGuid);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}