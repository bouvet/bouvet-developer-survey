// using Asp.Versioning;
// using Bouvet.Developer.Survey.Api.Constants;
// using Bouvet.Developer.Survey.Service.Interfaces.Survey;
// using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace Bouvet.Developer.Survey.Api.Controllers.V1;
//
// [ApiController]
// [ApiVersion("1.0")]
// [Authorize(Roles = RoleConstants.ReadRole)]
// [Route("api/v{version:apiVersion}/[controller]")]
// public class OptionsController : ControllerBase
// {
//     private readonly IOptionService _optionService;
//     
//     public OptionsController(IOptionService optionService)
//     {
//         _optionService = optionService;
//     }
//     
//     
//     /// <summary>
//     /// Get options to a block
//     /// </summary>
//     /// <returns>Options to a block</returns>
//     /// <response code="200">Returns options to a block</response>
//     /// <response code="401">If user is not authorized</response>
//     /// <response code="403">User not authorized to view</response>
//     [HttpGet("{blockId:guid}")]
//     [SwaggerResponse(200, "", typeof(BlockDto))]
//     public async Task<IActionResult> GetOptions(Guid blockId)
//     {
//         var option = await _optionService.GetOptionsToBlockAsync(blockId);
//         return Ok(option);
//     }
// }