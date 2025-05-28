using Bouvet.Developer.Survey.Service.Interfaces.Survey.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Bouvet.Developer.Survey.Api.Controllers
{
    [ApiController]
    [Route("api/bouvet/survey-responses")]
    public class SurveyResponseController : ControllerBase
    {
        private readonly ISurveyResponseService _surveyResponseService;

        public SurveyResponseController(ISurveyResponseService surveyResponseService)
        {
            _surveyResponseService = surveyResponseService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] BouvetSurveyResponseDto submissionDto)
        {
            if (submissionDto == null)
            {
                return BadRequest(new { message = "Submission data is required." });
            }

            try
            {
                await _surveyResponseService.SubmitResponseAsync(submissionDto);
                return Ok(new { message = "Response submitted successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Consider logging the exception ex
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }

        [HttpGet("structure/{year}")]
        public async Task<IActionResult> GetSurveyStructure(int year)
        {
            try
            {
                var surveyStructure = await _surveyResponseService.GetSurveyStructureRelationalAsync(year);
                if (surveyStructure == null)
                {
                    return NotFound(new { message = $"Survey structure for year {year} not found." });
                }
                return Ok(surveyStructure);
            }
            catch (Exception ex)
            {
                // Consider logging the exception ex
                return StatusCode(500, new { error = "An error occurred while retrieving the survey structure." });
            }
        }
    }
}