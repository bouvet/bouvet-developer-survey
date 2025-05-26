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
        public async Task<IActionResult> CreateOrUpdateSurveyDefinition(int year)
        {
            string jsonContent;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                jsonContent = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrWhiteSpace(jsonContent))
            {
                return BadRequest("JSON content is required in the request body.");
            }

            try
            {
                await _surveyDefinitionService.CreateOrUpdateSurveyDefinitionAsync(year, jsonContent);
                return NoContent();
            }
            catch (JsonException jsonEx)
            {
                return BadRequest($"Invalid JSON format: {jsonEx.Message}");
            }
            catch (System.Exception ex)
            {
                // TODO: Log the exception ex
                return StatusCode(500, "An error occurred while processing the survey definition.");
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