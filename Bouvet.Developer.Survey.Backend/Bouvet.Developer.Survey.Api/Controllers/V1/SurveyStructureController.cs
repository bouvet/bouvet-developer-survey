using Asp.Versioning;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bouvet.Developer.Survey.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SurveyStructureController : ControllerBase
{
    private readonly ISurveyStructureService _surveyStructureService;

    public SurveyStructureController(ISurveyStructureService surveyStructureService)
    {
        _surveyStructureService = surveyStructureService;
    }

    /// <summary>
    /// Import a new internal survey structure from JSON
    /// </summary>
    /// <param name="file">The uploaded JSON file</param>
    /// <param name="year">The year for which the survey is imported</param>
    /// <returns></returns>
    [HttpPost("import/relational")]
    [SwaggerResponse(200, "Survey structure unpacked")]
    [SwaggerResponse(400, "Invalid JSON or unpack failed")]
    public async Task<IActionResult> ImportAndUnpackSurveyStructure(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded or file is empty.");

        try
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            await _surveyStructureService.UnpackSurveyStructureAsync(stream);
            return Ok("Survey structure unpacked and stored relationally.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error unpacking structure: {ex.Message}");
        }
    }

    /// <summary>
    /// Get the survey structure for a specific year
    /// </summary>
    /// <param name="year">The survey year</param>
    /// <returns>The survey JSON structure</returns>
    [HttpGet("{year}")]
    [SwaggerResponse(200, "Survey structure retrieved")]
    [SwaggerResponse(404, "Survey structure not found")]
    public async Task<IActionResult> GetSurveyStructure(int year)
    {
        var structure = await _surveyStructureService.GetSurveyStructureByYearAsync(year);

        if (structure == null)
            return NotFound("No structure found for that year.");

        return Ok(structure);
    }
}
