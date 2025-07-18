﻿using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results.Bouvet;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.Bouvet;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Results;

public class SurveyResultsService : ISurveyResultsService
{
    private readonly BouvetSurveyContext _context;

    public SurveyResultsService(BouvetSurveyContext context)
    {
        _context = context;
    }

    public async Task<SurveyResultsDto?> GetSurveyResultsByYearAsync(int year)
    {
        var survey = await _context.Surveys
            .Include(s => s.Sections)
            .Include(s => s.Questions)
                .ThenInclude(q => q.Options)
            .Include(s => s.Questions)
                .ThenInclude(q => q.Responses)
            .FirstOrDefaultAsync(s => s.Year == year);

        if (survey == null)
            return null;

        var result = new SurveyResultsDto
        {
            SurveyId = survey.ExternalId,
            Title = survey.Title,
            Sections = new(),
            StandaloneQuestions = new()
        };

        // Group questions by section
        foreach (var section in survey.Sections)
        {
            var sectionDto = new SectionResultDto
            {
                Id = section.ExternalId,
                Title = section.Title,
                Description = section.Description,
                Questions = new()
            };

            var sectionQuestions = survey.Questions.Where(q => q.SectionId == section.Id);
            foreach (var question in sectionQuestions)
                sectionDto.Questions.Add(CreateQuestionDto(question));

            result.Sections.Add(sectionDto);
        }

        // Add standalone questions
        var standaloneQuestions = survey.Questions.Where(q => q.SectionId == null);
        foreach (var question in standaloneQuestions)
            result.StandaloneQuestions.Add(CreateQuestionDto(question));

        return result;
    }

    // Extracted helper
    private static QuestionResultDto CreateQuestionDto(BouvetQuestion question)
    {
        var dto = new QuestionResultDto
        {
            Id = question.ExternalId,
            Title = question.Title,
            Type = question.Type,
            Options = new()
        };

        if (question.Type == "likert")
        {
            var total = question.Responses.Count;
            var admired = question.Responses.Count(r => r.HasWorkedWith == true);
            var desired = question.Responses.Count(r => r.WantsToWorkWith == true);

            dto.LikertStats = new LikertStatsDto
            {
                AdmiredPercentage = total > 0 ? (int)Math.Round((double)admired / total * 100) : 0,
                DesiredPercentage = total > 0 ? (int)Math.Round((double)desired / total * 100) : 0
            };
        }

        if (question.Type == "single-choice" || question.Type == "multiple-choice")
        {
            foreach (var option in question.Options)
            {
                var count = question.Responses.Count(r => r.OptionId == option.Id);
                dto.Options.Add(new OptionResultDto
                {
                    Id = option.ExternalId,
                    Label = option.Value,
                    Count = count
                });
            }
        }

        return dto;
    }

}
