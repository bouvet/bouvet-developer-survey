using Bouvet.Developer.Survey.Domain.Entities.Bouvet;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Response; // Corrected path
using Bouvet.Developer.Survey.Service.Interfaces.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // For logging
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bouvet.Developer.Survey.Service
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly BouvetSurveyContext _context;
        private readonly IBouvetUserService _userService;
        private readonly ILogger<SurveyResponseService> _logger; // Added for logging

        public SurveyResponseService(BouvetSurveyContext context, IBouvetUserService userService, ILogger<SurveyResponseService> logger) // Added logger
        {
            _context = context;
            _userService = userService;
            _logger = logger; // Added logger
        }

        public async Task SubmitResponseAsync(BouvetSurveyResponseDto dto)
        {
            if (dto == null)
            {
                _logger.LogError("SubmitResponseAsync called with null DTO.");
                throw new ArgumentNullException(nameof(dto));
            }
            if (string.IsNullOrWhiteSpace(dto.SurveyExternalId))
            {
                _logger.LogError("SubmitResponseAsync: SurveyExternalId is required. RespondentId: {RespondentId}", dto.RespondentId);
                throw new ArgumentException("SurveyExternalId is required.", nameof(dto.SurveyExternalId));
            }
            if (string.IsNullOrWhiteSpace(dto.RespondentId))
            {
                _logger.LogError("SubmitResponseAsync: RespondentId is required. SurveyExternalId: {SurveyExternalId}", dto.SurveyExternalId);
                throw new ArgumentException("RespondentId is required.", nameof(dto.RespondentId));
            }
            if (dto.Answers == null || !dto.Answers.Any())
            {
                _logger.LogInformation("SubmitResponseAsync: No answers provided for RespondentId: {RespondentId}, SurveyExternalId: {SurveyExternalId}. Nothing to process.", dto.RespondentId, dto.SurveyExternalId);
                return;
            }

            var survey = await _context.Surveys
                                     .AsNoTracking()
                                     .FirstOrDefaultAsync(s => s.ExternalId == dto.SurveyExternalId);
            if (survey == null)
            {
                _logger.LogError("SubmitResponseAsync: Survey with ExternalId '{SurveyExternalId}' not found. RespondentId: {RespondentId}", dto.SurveyExternalId, dto.RespondentId);
                throw new InvalidOperationException($"Survey with ExternalId '{dto.SurveyExternalId}' not found.");
            }

            var user = await _userService.GetOrCreateUserAsync(dto.RespondentId, survey.Id);
            if (user == null) // Should not happen if GetOrCreateUserAsync is implemented correctly
            {
                _logger.LogCritical("SubmitResponseAsync: Failed to get or create user. RespondentId: {RespondentId}, SurveyId: {SurveyId}", dto.RespondentId, survey.Id);
                throw new InvalidOperationException("Failed to retrieve or create user.");
            }


            var questionsWithOptionsForSurvey = await _context.Questions
                                            .Where(q => q.SurveyId == survey.Id)
                                            .Include(q => q.Options)
                                            .ToDictionaryAsync(q => q.ExternalId);

            var newResponses = new List<BouvetResponse>();
            var utcNow = DateTimeOffset.UtcNow;

            foreach (var answerDto in dto.Answers)
            {
                if (!questionsWithOptionsForSurvey.TryGetValue(answerDto.QuestionExternalId, out var questionEntity))
                {
                    _logger.LogWarning("SubmitResponseAsync: Question with ExternalId '{QuestionExternalId}' not found for survey '{SurveyExternalId}'. RespondentId: {RespondentId}. Skipping answer.",
                        answerDto.QuestionExternalId, survey.ExternalId, dto.RespondentId);
                    continue;
                }

                bool hasProcessedAnswerForQuestion = false;

                // Handle free-text answer if provided
                if (!string.IsNullOrWhiteSpace(answerDto.FreeTextAnswer))
                {
                    newResponses.Add(new BouvetResponse
                    {
                        UserId = user.Id,
                        QuestionId = questionEntity.Id,
                        OptionId = null,
                        HasWorkedWith = null,
                        WantsToWorkWith = null,
                        FreeTextAnswer = answerDto.FreeTextAnswer, // Save free-text
                        CreatedAt = utcNow
                    });
                    hasProcessedAnswerForQuestion = true;
                }

                // Handle option-based answers (including Likert)
                if (answerDto.OptionExternalIds != null && answerDto.OptionExternalIds.Any())
                {
                    foreach (var optionInputString in answerDto.OptionExternalIds)
                    {
                        string optionExternalIdToLookup = optionInputString;
                        bool? hasWorkedWithValue = null;
                        bool? wantsToWorkWithValue = null;

                        if (questionEntity.Type != null && questionEntity.Type.Equals("likert", StringComparison.OrdinalIgnoreCase))
                        {
                            var parts = optionInputString.Split('-');
                            if (parts.Length == 2)
                            {
                                optionExternalIdToLookup = parts[0];
                                string likertAspect = parts[1];
                                if (likertAspect.Equals("Admired", StringComparison.OrdinalIgnoreCase)) // Consider making these constants
                                    hasWorkedWithValue = true;
                                else if (likertAspect.Equals("Desired", StringComparison.OrdinalIgnoreCase)) // Consider making these constants
                                    wantsToWorkWithValue = true;
                                else
                                {
                                    _logger.LogWarning("SubmitResponseAsync: Invalid Likert aspect '{LikertAspect}' for option '{OptionExternalIdToLookup}' on question '{QuestionExternalId}'. RespondentId: {RespondentId}. Skipping.",
                                        likertAspect, optionExternalIdToLookup, questionEntity.ExternalId, dto.RespondentId);
                                    continue;
                                }
                            }
                            else
                            {
                                _logger.LogWarning("SubmitResponseAsync: Invalid Likert format '{OptionInputString}' for question '{QuestionExternalId}'. RespondentId: {RespondentId}. Skipping.",
                                    optionInputString, questionEntity.ExternalId, dto.RespondentId);
                                continue;
                            }
                        }

                        var optionEntity = questionEntity.Options.FirstOrDefault(o => o.ExternalId == optionExternalIdToLookup);
                        if (optionEntity == null)
                        {
                            _logger.LogWarning("SubmitResponseAsync: Option with ExternalId '{OptionExternalIdToLookup}' not found for question '{QuestionExternalId}'. RespondentId: {RespondentId}. Skipping option.",
                                optionExternalIdToLookup, questionEntity.ExternalId, dto.RespondentId);
                            continue;
                        }

                        newResponses.Add(new BouvetResponse
                        {
                            UserId = user.Id,
                            QuestionId = questionEntity.Id,
                            OptionId = optionEntity.Id,
                            HasWorkedWith = hasWorkedWithValue,
                            WantsToWorkWith = wantsToWorkWithValue,
                            FreeTextAnswer = null, // Explicitly null for option-based answers
                            CreatedAt = utcNow
                        });
                        hasProcessedAnswerForQuestion = true;
                    }
                }
                
                if (!hasProcessedAnswerForQuestion)
                {
                     _logger.LogWarning("SubmitResponseAsync: Answer for question '{QuestionExternalId}' had no options and no free-text. RespondentId: {RespondentId}. Skipping.", 
                        answerDto.QuestionExternalId, dto.RespondentId);
                }
            }

            if (newResponses.Any())
            {
                _context.Responses.AddRange(newResponses);
                await _context.SaveChangesAsync();
                _logger.LogInformation("SubmitResponseAsync: Successfully saved {Count} responses for RespondentId: {RespondentId}, SurveyExternalId: {SurveyExternalId}.",
                    newResponses.Count, dto.RespondentId, dto.SurveyExternalId);
            }
            else
            {
                _logger.LogInformation("SubmitResponseAsync: No valid responses to save for RespondentId: {RespondentId}, SurveyExternalId: {SurveyExternalId}.",
                    dto.RespondentId, dto.SurveyExternalId);
            }
        }

        public async Task<SurveyExportDto?> GetSurveyStructureRelationalAsync(int year)
        {
            var survey = await _context.Surveys
                .AsNoTracking()
                .Include(s => s.Sections)
                .Include(s => s.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(s => s.Year == year); // Corrected to filter by survey.Year

            if (survey == null)
            {
                _logger.LogWarning("GetSurveyStructureRelationalAsync: Survey for year {Year} not found.", year);
                return null;
            }

            return new SurveyExportDto
            {
                Id = survey.ExternalId,
                Title = survey.Title,
                StartDate = survey.StartDate.ToString("yyyy-MM-dd"),
                EndDate = survey.EndDate.ToString("yyyy-MM-dd"),
                Year = survey.Year,
                Sections = survey.Sections?.Select(sec => new SectionExportDto
                {
                    Id = sec.ExternalId,
                    Title = sec.Title,
                    Description = sec.Description,
                }).ToList() ?? new List<SectionExportDto>(),
                Questions = survey.Questions?.Select(q => new QuestionExportDto
                {
                    Id = q.ExternalId,
                    Title = q.Title,
                    Description = q.Description,
                    Type = q.Type,
                    SectionId = q.Section?.ExternalId, 
                    Options = q.Options?.Select(opt => new OptionExportDto
                    {
                        Id = opt.ExternalId,
                        Value = opt.Value,
                    }).ToList() ?? new List<OptionExportDto>(),
                    Columns = q.Type != null && q.Type.Equals("likert", StringComparison.OrdinalIgnoreCase) ? new List<string> { "Admired", "Desired" } : null
                }).ToList() ?? new List<QuestionExportDto>()
            };
        }
    }
}
