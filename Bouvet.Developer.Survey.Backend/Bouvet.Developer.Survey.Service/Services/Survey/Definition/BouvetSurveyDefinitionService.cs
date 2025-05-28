using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Definition;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structure;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.Developer.Survey.Service.Services.Survey.Definition
{
    public class BouvetSurveyDefinitionService : IBouvetSurveyDefinitionService
    {
        private readonly BouvetSurveyContext _context;
        private readonly ISurveyStructureService _surveyStructureService;

        public BouvetSurveyDefinitionService(BouvetSurveyContext context, ISurveyStructureService surveyStructureService)
        {
            _context = context;
            _surveyStructureService = surveyStructureService;
        }

        public async Task<string?> GetSurveyDefinitionJsonAsync(int year)
        {
            var surveyStructure = await _context.BouvetSurveyStructures
                                                .AsNoTracking() // Optimization if not modifying the entity
                                                .FirstOrDefaultAsync(s => s.Year == year);
            return surveyStructure?.StructureJson;
        }

        public async Task CreateOrUpdateSurveyDefinitionAsync(int year, string jsonContent)
        {
            var surveyStructure = await _context.BouvetSurveyStructures
                                                .FirstOrDefaultAsync(s => s.Year == year);

            if (surveyStructure == null)
            {
                surveyStructure = new BouvetSurveyStructure
                {
                    Year = year,
                    StructureJson = jsonContent
                };
                _context.BouvetSurveyStructures.Add(surveyStructure);
            }
            else
            {
                surveyStructure.StructureJson = jsonContent;
                // EF Core tracks changes, so explicit Update might not be needed if surveyStructure is tracked.
                // However, it's safe to leave it or use _context.Entry(surveyStructure).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();

            // After saving the JSON, trigger the unpacking of the survey structure.
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent)))
            {
                // ISurveyStructureService.UnpackSurveyStructureAsync expects a stream.
                // The DTO within the stream should contain necessary identifiers (e.g., year).
                await _surveyStructureService.UnpackSurveyStructureAsync(stream);
            }
        }

        public async Task<bool> DeleteSurveyDefinitionAsync(int year)
        {
            var surveyStructure = await _context.BouvetSurveyStructures
                                                .FirstOrDefaultAsync(s => s.Year == year);

            if (surveyStructure == null)
            {
                return false;
            }

            _context.BouvetSurveyStructures.Remove(surveyStructure);
            var changes = await _context.SaveChangesAsync();
            // Note: This currently only removes the JSON definition.
            // Consider if this should also trigger deletion/deactivation of the unpacked relational structure.
            return changes > 0;
        }
    }
}