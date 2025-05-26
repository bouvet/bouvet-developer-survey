using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using System.Threading.Tasks;

namespace Bouvet.Developer.Survey.Service.Interfaces.User
{
    public interface IBouvetUserService
    {
        /// <summary>
        /// Gets an existing BouvetUser based on a unique respondent identifier and survey ID,
        /// or creates a new one if not found.
        /// </summary>
        /// <param name="respondId">A unique identifier for the respondent for this survey submission.</param>
        /// <param name="bouvetSurveyId">The ID of the BouvetSurvey the user is responding to.</param>
        /// <returns>The existing or newly created BouvetUser.</returns>
        Task<BouvetUser> GetOrCreateUserAsync(string respondId, int bouvetSurveyId);
    }
}