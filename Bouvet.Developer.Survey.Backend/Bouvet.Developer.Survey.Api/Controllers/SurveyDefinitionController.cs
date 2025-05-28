using Bouvet.Developer.Survey.Service.Interfaces.Survey.Definition;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.Json; // For JsonException

namespace Bouvet.Developer.Survey.Api.Controllers
{
    [ApiController]
    [Route("api/survey-definitions")]
    // [Authorize(Roles = "Admin")] // TODO: Add appropriate authorization
    public class SurveyDefinitionController : ControllerBase
    {
        private readonly IBouvetSurveyDefinitionService _surveyDefinitionService;

        public SurveyDefinitionController(IBouvetSurveyDefinitionService surveyDefinitionService)
        {
            _surveyDefinitionService = surveyDefinitionService;
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> GetSurveyDefinition(int year)
        {
            var jsonContent = await _surveyDefinitionService.GetSurveyDefinitionJsonAsync(year);
            if (jsonContent == null)
            {
                return NotFound($"Survey definition for year {year} not found.");
            }
            return Content(jsonContent, "application/json");
        }

        [HttpPut("{year}")]
        public async Task<IActionResult> CreateOrUpdateSurveyDefinition(int year, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("A non-empty JSON file must be uploaded.");
            }

            using var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
            var jsonContent = await reader.ReadToEndAsync();

            try
            {
                await _surveyDefinitionService.CreateOrUpdateSurveyDefinitionAsync(year, jsonContent);
                return NoContent();
            }
            catch (JsonException ex)
            {
                return BadRequest($"Invalid JSON format: {ex.Message}");
            }
        }

        [HttpDelete("{year}")]
        public async Task<IActionResult> DeleteSurveyDefinition(int year)
        {
            var success = await _surveyDefinitionService.DeleteSurveyDefinitionAsync(year);
            if (!success)
            {
                return NotFound($"Survey definition for year {year} not found or could not be deleted.");
            }
            return NoContent();
        }
    }
}