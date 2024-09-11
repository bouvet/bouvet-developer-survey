using Asp.Versioning;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bouvet.Developer.Survey.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class OptionController : ControllerBase
{
    private readonly IOptionService _optionService;
    
    public OptionController(IOptionService optionService)
    {
        _optionService = optionService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(OptionDto), 200)]
    [SwaggerResponse(200, "Block created", typeof(BlockDto))]
    public async Task<IActionResult> CreateBlock([FromBody] NewOptionDto newOptionDto)
    {
        var block = await _optionService.CreateOptionAsync(newOptionDto);
        return Ok(block);
    }
}