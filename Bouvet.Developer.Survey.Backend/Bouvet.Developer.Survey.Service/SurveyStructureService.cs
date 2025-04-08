using Bouvet.Developer.Survey.Domain.Entities;
using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structure;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bouvet.Developer.Survey.Service.Services
{
    public class SurveyStructureService : ISurveyStructureService
    {
        private readonly BouvetSurveyContext _context;

        public SurveyStructureService(BouvetSurveyContext context)
        {
            _context = context;
        }

        public async Task<string?> GetSurveyStructureByYearAsync(int year)
        {
            var entry = await _context.BouvetSurveyStructures
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Year == year);

            return entry?.StructureJson;
        }

        public async Task UnpackSurveyStructureAsync(Stream jsonStream)
        {
            // Log that deserialization is starting
            Console.WriteLine("UnpackSurveyStructureAsync started.");

            var dto = await JsonSerializer.DeserializeAsync<SurveyUploadDto>(jsonStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (dto == null)
            {
                Console.WriteLine("Deserialization returned null.");
                throw new Exception("Invalid JSON structure.");
            }

            // Log number of sections/questions from DTO
            Console.WriteLine($"Deserialized: {dto.Sections?.Count ?? 0} sections.");

            var survey = new BouvetSurvey
            {
                Year = int.Parse(dto.Id),
                Title = dto.Title,
                StartDate = DateTime.Parse(dto.StartDate),
                EndDate = DateTime.Parse(dto.EndDate),
                Questions = dto.Sections
                    .SelectMany(section => section.Questions)
                    .Select(q => new BouvetQuestion
                    {
                        // Map using the Title field (since you updated your JSON)
                        // Make sure the DTO has a non-null Title!
                        Type = q.Type,
                        Title = q.Title,
                        Description = q.Description,
                        Options = q.Options?.Select(o => new BouvetOption
                        {
                            Identifier = o.Id,
                            Value = o.Value
                        }).ToList()
                    }).ToList()
            };

            Console.WriteLine($"Built survey entity with {survey.Questions?.Count ?? 0} questions.");

            _context.Surveys.Add(survey);
            var count = await _context.SaveChangesAsync();
            Console.WriteLine($"SaveChangesAsync completed. {count} records affected.");
        }


    }
}
