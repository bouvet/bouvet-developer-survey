using Bouvet.Developer.Survey.Domain.Entities;
using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structure;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Bouvet.Developer.Survey.Service.Services
{
    public class SurveyStructureService : ISurveyStructureService
    {
        private readonly BouvetSurveyContext _context;

        public SurveyStructureService(BouvetSurveyContext context)
        {
            _context = context;
        }

        public async Task<SurveyExportDto?> GetSurveyStructureByYearAsync(int year)
        {
            var survey = await _context.Surveys
                .Include(s => s.Sections)
                .Include(s => s.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(s => s.Year == year);

            if (survey == null) return null;

            return new SurveyExportDto
            {
                Title = survey.Title,
                Id = survey.ExternalId,
                StartDate = survey.StartDate.ToString("yyyy-MM-dd"),
                EndDate = survey.EndDate.ToString("yyyy-MM-dd"),
                Sections = survey.Sections.Select(section => new SectionExportDto
                {
                    Id = section.ExternalId,
                    Title = section.Title,
                    Description = section.Description,
                }).ToList(),

                Questions = survey.Questions.Select(q => new QuestionExportDto
                {
                    Id = q.ExternalId,
                    Title = q.Title,
                    Type = q.Type,
                    Required = q.Required ?? false,
                    Description = q.Description,
                    SectionId = q.Section?.ExternalId,
                    Options = q.Options.Select(o => new OptionExportDto
                    {
                        Id = o.ExternalId,
                        Value = o.Value
                    }).ToList(),
                    Columns = q.Type == "likert" ? new List<string> { "Admired", "Desired" } : null
                }).ToList()
            };
        }


        public async Task UnpackSurveyStructureAsync(Stream jsonStream)
        {
            var dto = await JsonSerializer.DeserializeAsync<SurveyUploadDto>(jsonStream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (dto == null)
                throw new Exception("Invalid JSON structure.");

            var existingSurvey = await _context.Surveys
                .Include(s => s.Questions)
                    .ThenInclude(q => q.Options)
                .Include(s => s.Sections)
                .FirstOrDefaultAsync(s => s.ExternalId == dto.Id);

            if (existingSurvey == null)
            {
                existingSurvey = new BouvetSurvey
                {
                    ExternalId = dto.Id,
                    Title = dto.Title,
                    StartDate = DateTime.Parse(dto.StartDate),
                    EndDate = DateTime.Parse(dto.EndDate),
                    Year = int.Parse(dto.StartDate[..4])
                };

                _context.Surveys.Add(existingSurvey);
            }
            else
            {
                existingSurvey.Title = dto.Title;
                existingSurvey.StartDate = DateTime.Parse(dto.StartDate);
                existingSurvey.EndDate = DateTime.Parse(dto.EndDate);
                // Set year from DTO if it's valid, otherwise fallback to StartDate
                if (int.TryParse(dto.Year, out int parsedYear))
                {
                    existingSurvey.Year = parsedYear;
                }
                else
                {
                    existingSurvey.Year = existingSurvey.StartDate.Year;
                }
            }

            // Add sections
            var sectionMap = new Dictionary<string, BouvetSection>();
            foreach (var section in dto.Sections)
            {
                var sectionEntity = existingSurvey.Sections.FirstOrDefault(s => s.ExternalId == section.Id);

                if (sectionEntity == null)
                {
                    sectionEntity = new BouvetSection
                    {
                        ExternalId = section.Id,
                        Title = section.Title,
                        Description = section.Description,
                        Survey = existingSurvey
                    };

                    _context.Sections.Add(sectionEntity);
                    existingSurvey.Sections.Add(sectionEntity);
                }
                else
                {
                    sectionEntity.Title = section.Title;
                    sectionEntity.Description = section.Description;
                }

                sectionMap[section.Id] = sectionEntity;
            }

            // Add questions
            foreach (var q in dto.Questions)
            {
                var questionExternalId = q.Id;
                var existingQuestion = existingSurvey.Questions.FirstOrDefault(qx => qx.ExternalId == questionExternalId);

                if (existingQuestion == null)
                {
                    existingQuestion = new BouvetQuestion
                    {
                        ExternalId = questionExternalId,
                        Type = q.Type,
                        Title = q.Title,
                        Required = q.Required ?? false,
                        Description = q.Description,
                        Survey = existingSurvey,
                        Section = q.SectionId != null && sectionMap.ContainsKey(q.SectionId)
                            ? sectionMap[q.SectionId]
                            : null
                    };
                    existingSurvey.Questions.Add(existingQuestion);
                }
                else
                {
                    existingQuestion.Type = q.Type;
                    existingQuestion.Title = q.Title;
                    existingQuestion.Required = q.Required;
                    existingQuestion.Description = q.Description;
                    existingQuestion.Section = q.SectionId != null && sectionMap.ContainsKey(q.SectionId)
                        ? sectionMap[q.SectionId]
                        : null;
                }

                if (q.Options != null)
                {
                    foreach (var opt in q.Options)
                    {
                        var existingOption = existingQuestion.Options.FirstOrDefault(o => o.ExternalId == opt.Id);
                        if (existingOption == null)
                        {
                            existingOption = new BouvetOption
                            {
                                ExternalId = opt.Id,
                                Value = opt.Value,
                                Question = existingQuestion
                            };
                            existingQuestion.Options.Add(existingOption);
                        }
                        else
                        {
                            existingOption.Value = opt.Value;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
