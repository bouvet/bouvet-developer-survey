using Asp.Versioning;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results.Bouvet;
using Microsoft.AspNetCore.Mvc;

namespace Bouvet.Developer.Survey.Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BouvetResultsController : ControllerBase
{
    private readonly ISurveyResultsService _resultsService;

    public BouvetResultsController(ISurveyResultsService resultsService)
    {
        _resultsService = resultsService;
    }

    [HttpGet("{year}")]
    public async Task<IActionResult> GetResultsForYear(int year)
    {
        var result = await _resultsService.GetSurveyResultsByYearAsync(year);

        if (result == null)
            return NotFound($"No survey results found for year {year}");

        return Ok(result);
    }
}
