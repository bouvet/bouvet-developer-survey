using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class SurveyResponseService : ISurveyResponseService
{
    private readonly BouvetSurveyContext _context;

    public SurveyResponseService(BouvetSurveyContext context)
    {
        _context = context;
    }

    public async Task<SurveyExportDto?> GetSurveyStructureRelationalAsync(int year)
    {
        var survey = await _context.Surveys
            .Include(s => s.Sections)
            .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(s => s.StartDate.Year == year);

        if (survey == null)
            return null;

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
                Description = q.Description,
                SectionId = q.Section?.ExternalId,
                Options = q.Options.Select(o => new OptionExportDto
                {
                    Id = o.ExternalId,
                    Value = o.Value
                }).ToList()
            }).ToList()
        };
    }

    public async Task SubmitResponseAsync(BouvetSurveyResponseDto dto)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.ExternalId == dto.SurveyExternalId);
        if (survey == null)
            throw new Exception("Survey not found");

        var user = await _context.Users.FirstOrDefaultAsync(u => u.RespondId == dto.RespondentId);
        if (user == null)
        {
            user = new BouvetUser
            {
                Id = Guid.NewGuid(),
                RespondId = dto.RespondentId,
                Survey = survey,
                CreatedAt = DateTimeOffset.UtcNow
            };
            _context.Users.Add(user);
        }

        var questions = await _context.Questions
            .Include(q => q.Options)
            .Where(q => q.SurveyId == survey.Id)
            .ToListAsync();

        foreach (var answer in dto.Answers)
        {
            var question = questions.FirstOrDefault(q => q.ExternalId == answer.QuestionExternalId);
            if (question == null)
                continue;

            if (question.Type == "likert")
            {
                foreach (var combinedId in answer.OptionExternalIds)
                {
                    var parts = combinedId.Split('-');
                    if (parts.Length != 2)
                        continue;

                    var optionId = parts[0];
                    var column = parts[1];

                    var option = question.Options.FirstOrDefault(o => o.ExternalId == optionId);
                    if (option == null)
                        continue;

                    var response = new BouvetResponse
                    {
                        Id = Guid.NewGuid(),
                        OptionId = option.Id,
                        QuestionId = question.Id,
                        UserId = user.Id,
                        HasWorkedWith = column.Equals("Admired", StringComparison.OrdinalIgnoreCase),
                        WantsToWorkWith = column.Equals("Desired", StringComparison.OrdinalIgnoreCase),
                        CreatedAt = DateTimeOffset.UtcNow
                    };

                    _context.Responses.Add(response);
                }
            }
            else
            {
                foreach (var optionId in answer.OptionExternalIds)
                {
                    var option = question.Options.FirstOrDefault(o => o.ExternalId == optionId);
                    if (option == null)
                        continue;

                    var response = new BouvetResponse
                    {
                        Id = Guid.NewGuid(),
                        OptionId = option.Id,
                        QuestionId = question.Id,
                        UserId = user.Id,
                        CreatedAt = DateTimeOffset.UtcNow
                    };

                    _context.Responses.Add(response);
                }
            }
        }

        await _context.SaveChangesAsync();
    }


}
