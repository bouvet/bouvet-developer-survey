using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SurveyResponseController : ControllerBase
{
    private readonly ISurveyResponseService _responseService;

    public SurveyResponseController(ISurveyResponseService responseService)
    {
        _responseService = responseService;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitSurveyResponse([FromBody] BouvetSurveyResponseDto dto)
    {
        await _responseService.SubmitResponseAsync(dto);
        return Ok();
    }


}