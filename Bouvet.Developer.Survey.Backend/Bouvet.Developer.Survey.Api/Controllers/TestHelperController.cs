using Bouvet.Developer.Survey.Service.Interfaces.Survey.Definition;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json;


namespace Bouvet.Developer.Survey.Api.Controllers
{
    [ApiController]
    [Route("api/test-helper")]
    public class TestHelperController : ControllerBase
    {
        private readonly IBouvetSurveyDefinitionService _surveyDefinitionService;
        private readonly ISurveyResponseService _surveyResponseService;

        // Hardcoded sample survey definition JSON
        private const string SampleSurveyDefinitionJson = @"
        {
          ""title"": ""Developer Tools Survey 2025"",
          ""id"": ""devtools-2025"",
          ""startDate"": ""2025-01-01"",
          ""endDate"": ""2025-12-31"",
          ""year"": ""2028"",
          ""sections"": [
            { ""id"": ""tools"", ""title"": ""Tools"", ""description"": ""Preferred tools and environments"" },
            { ""id"": ""languages"", ""title"": ""Languages"", ""description"": ""Languages you use regularly"" },
            { ""id"": ""feedback_section"", ""title"": ""General Feedback"", ""description"": ""Your overall thoughts"" }
          ],
          ""questions"": [
            {
              ""id"": ""standalone-q"", ""type"": ""single-choice"", ""title"": ""Do you like remote work?"", ""description"": ""Standalone question without section"", ""sectionId"": null,
              ""options"": [ { ""id"": ""yes"", ""value"": ""Yes"" }, { ""id"": ""no"", ""value"": ""No"" } ]
            },
            {
              ""id"": ""fav-editor"", ""type"": ""single-choice"", ""title"": ""What is your favorite code editor?"", ""description"": ""Choose the one you use most often"", ""sectionId"": ""tools"",
              ""options"": [ { ""id"": ""vscode"", ""value"": ""VS Code"" }, { ""id"": ""jetbrains"", ""value"": ""JetBrains IDE"" }, { ""id"": ""vim"", ""value"": ""Vim"" } ]
            },
            {
              ""id"": ""language-preference"", ""type"": ""multiple-choice"", ""title"": ""Which programming languages do you use regularly?"", ""description"": ""You can select more than one"", ""sectionId"": ""languages"",
              ""options"": [ { ""id"": ""csharp"", ""value"": ""C#"" }, { ""id"": ""js"", ""value"": ""JavaScript"" }, { ""id"": ""py"", ""value"": ""Python"" } ]
            },
            {
              ""id"": ""language-likert"", ""type"": ""likert"", ""title"": ""Languages you admire vs want to use"", ""description"": ""Mark which languages you've worked with and which you want to work with"", ""sectionId"": ""languages"",
              ""options"": [
                { ""id"": ""csharp"", ""value"": ""C#"" }, { ""id"": ""js"", ""value"": ""JavaScript"" },
                { ""id"": ""py"", ""value"": ""Python"" }, { ""id"": ""go"", ""value"": ""Go"" }
              ]
            },
            {
              ""id"": ""Q_FEEDBACK"", ""type"": ""free-text"", ""title"": ""Any other comments?"", ""description"": ""Please provide any additional feedback you have."", ""sectionId"": ""feedback_section""
            }
          ]
        }";

        // Hardcoded sample survey response JSON
        private const string SampleSurveyResponseJson = @"
        {
          ""respondentId"": ""test-dev-001"",
          ""surveyId"": ""devtools-2025"",
          ""answers"": [
            { ""questionId"": ""standalone-q"", ""optionIds"": [""yes""] },
            { ""questionId"": ""fav-editor"", ""optionIds"": [""vscode""] },
            { ""questionId"": ""language-preference"", ""optionIds"": [""csharp"", ""js""] },
            {
              ""questionId"": ""language-likert"",
              ""optionIds"": [ ""csharp-Admired"", ""js-Admired"", ""py-Desired"", ""go-Desired"" ]
            },
            {
              ""questionId"": ""Q_FEEDBACK"",
              ""freeTextAnswer"": ""This is a test free-text answer. I hope it works!""
            }
          ]
        }";


        public TestHelperController(
            IBouvetSurveyDefinitionService surveyDefinitionService,
            ISurveyResponseService surveyResponseService)
        {
            _surveyDefinitionService = surveyDefinitionService;
            _surveyResponseService = surveyResponseService;
        }

        /// <summary>
        /// Loads the hardcoded sample survey definition for year 2028.
        /// This will create/update BouvetSurveyStructure and trigger unpacking.
        /// </summary>
        [HttpPost("load-sample-survey")]
        public async Task<IActionResult> LoadSampleSurvey()
        {
            try
            {
                // The year from the sample JSON is "2028"
                await _surveyDefinitionService.CreateOrUpdateSurveyDefinitionAsync(2028, SampleSurveyDefinitionJson);
                return Ok("Sample survey definition loaded successfully for year 2028. Unpacking process initiated.");
            }
            catch (System.Exception ex)
            {
                // Consider logging the exception with ILogger
                return StatusCode(500, $"Error loading sample survey: {ex.Message}");
            }
        }

        /// <summary>
        /// Submits a hardcoded sample response for the 'devtools-2025' survey.
        /// Assumes the survey definition has already been loaded.
        /// </summary>
        [HttpPost("submit-sample-response")]
        public async Task<IActionResult> SubmitSampleResponse()
        {
            try
            {
                var responseDto = JsonSerializer.Deserialize<BouvetSurveyResponseDto>(SampleSurveyResponseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (responseDto == null)
                {
                    return BadRequest("Failed to deserialize sample response JSON.");
                }

                await _surveyResponseService.SubmitResponseAsync(responseDto);
                return Ok("Sample survey response submitted successfully.");
            }
            catch (JsonException jsonEx)
            {
                 // Consider logging the exception with ILogger
                 return StatusCode(500, $"Error deserializing sample response JSON: {jsonEx.Message}");
            }
            catch (System.Exception ex)
            {
                // Consider logging the exception with ILogger
                return StatusCode(500, $"Error submitting sample response: {ex.Message}");
            }
        }
    }
}