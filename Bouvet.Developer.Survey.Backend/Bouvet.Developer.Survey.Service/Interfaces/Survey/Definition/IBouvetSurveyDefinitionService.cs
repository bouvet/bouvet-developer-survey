using System.Threading.Tasks;

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Definition
{
    public interface IBouvetSurveyDefinitionService
    {
        /// <summary>
        /// Retrieves the survey definition JSON for a specific year.
        /// </summary>
        /// <param name="year">The year of the survey definition.</param>
        /// <returns>The JSON string of the survey definition, or null if not found.</returns>
        Task<string?> GetSurveyDefinitionJsonAsync(int year);

        /// <summary>
        /// Creates a new survey definition or updates an existing one for a specific year.
        /// After saving the JSON, it triggers the unpacking of the structure into relational tables.
        /// </summary>
        /// <param name="year">The year of the survey definition.</param>
        /// <param name="jsonContent">The JSON content of the survey definition.</param>
        Task CreateOrUpdateSurveyDefinitionAsync(int year, string jsonContent);

        /// <summary>
        /// Deletes the survey definition JSON for a specific year.
        /// Note: This might or might not delete the unpacked relational survey structure,
        /// depending on desired business logic (for now, assume it only deletes the JSON definition).
        /// </summary>
        /// <param name="year">The year of the survey definition to delete.</param>
        /// <returns>True if deletion was successful, false otherwise.</returns>
        Task<bool> DeleteSurveyDefinitionAsync(int year);
    }
}