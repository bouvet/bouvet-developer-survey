

namespace Bouvet.Developer.Survey.Service.Interfaces.Survey.Ai;

public interface IAiService
{
    /// <summary>
    /// Check for difference in AI analysis for a survey question
    /// </summary>
    /// <param name="surveyId">The survey id</param>
    /// <returns></returns>
    Task CheckForDifferenceAsync(string surveyId);
}