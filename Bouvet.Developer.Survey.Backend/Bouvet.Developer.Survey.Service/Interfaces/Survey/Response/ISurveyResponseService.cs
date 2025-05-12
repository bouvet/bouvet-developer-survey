public interface ISurveyResponseService
{
    Task<SurveyExportDto?> GetSurveyStructureRelationalAsync(int year);

    Task SubmitResponseAsync(BouvetSurveyResponseDto dto);
}
