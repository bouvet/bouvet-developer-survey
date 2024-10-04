using System.Text.Json;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.TransferObjects.Import;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;

namespace Bouvet.Developer.Survey.Service.Import;

public class ImportSurveyService : IImportSurveyService
{
    public async Task<SurveyBlocksDto> FindSurveyBlocks(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var jsonString = await reader.ReadToEndAsync();

        var surveyDto = JsonSerializer.Deserialize<SurveyBlocksDto>(jsonString);

        if (surveyDto == null) throw new BadRequestException("Invalid JSON");

        // Filter out elements where Payload is null or empty
        surveyDto.SurveyElements = (surveyDto.SurveyElements ?? throw new BadRequestException("Invalid JSON"))
            .Where(element => element.Payload != null && element.Payload.Count > 0)
            .ToArray();

        return surveyDto;
    }

    public async Task<SurveyQuestionsDto> FindSurveyQuestions(Stream stream)
    {
        using var reader = new StreamReader(stream);
        var jsonString = await reader.ReadToEndAsync();

        var surveyDto = JsonSerializer.Deserialize<SurveyQuestionsDto>(jsonString);

        if (surveyDto == null) throw new BadRequestException("Invalid JSON");

        // Filter out elements where Payload is null or empty
        surveyDto.SurveyElements = (surveyDto.SurveyElements ?? throw new BadRequestException("Invalid JSON"))
            .Where(element => element.Payload != null && element.Payload.Choices.Count > 0)
            .ToArray();

        return surveyDto;
    }
}