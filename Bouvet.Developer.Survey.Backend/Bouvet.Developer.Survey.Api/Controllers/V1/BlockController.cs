using Asp.Versioning;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bouvet.Developer.Survey.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BlockController : ControllerBase
{
   private readonly IBlockService _blockService;
   
   public BlockController(IBlockService blockService)
   {
       _blockService = blockService;
   }
   
   [HttpPost]
   [ProducesResponseType(typeof(BlockDto), 200)]
   [SwaggerResponse(200, "Block created", typeof(BlockDto))]
   public async Task<IActionResult> CreateBlock([FromBody] NewBlockDto newBlockDto)
   {
       var block = await _blockService.CreateBlockAsync(newBlockDto);
       return Ok(block);
   }
}