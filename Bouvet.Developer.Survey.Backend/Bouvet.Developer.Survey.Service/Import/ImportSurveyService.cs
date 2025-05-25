using System.Text.Json;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Import;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Ai;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Results;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Results.User;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Import;

public class ImportSurveyService : IImportSurveyService
{
    private readonly ISurveyService _surveyService;
    private readonly IQuestionService _questionService;
    private readonly DeveloperSurveyContext _context;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly IBlockElementService _blockElementService;
    private readonly IResultService _resultService;
    private readonly ICsvToJsonService _csvToJsonService;
    private readonly IUserService _userService;
    private readonly IAiService _aiService;

    public ImportSurveyService(
        ISurveyService surveyService,
        DeveloperSurveyContext context,
        IQuestionService questionService,
        ISurveyBlockService surveyBlockService,
        IBlockElementService blockElementService,
        IResultService resultService,
        ICsvToJsonService csvToJsonService,
        IUserService userService,
        IAiService aiService
    )
    {
        _surveyService = surveyService;
        _context = context;
        _questionService = questionService;
        _surveyBlockService = surveyBlockService;
        _blockElementService = blockElementService;
        _resultService = resultService;
        _csvToJsonService = csvToJsonService;
        _userService = userService;
        _aiService = aiService;
    }

    public async Task<SurveyBlocksDto> UploadSurvey(Stream stream, int year)
    {
        using var reader = new StreamReader(stream);
        var jsonString = await reader.ReadToEndAsync();

        var surveyDto = JsonSerializer.Deserialize<SurveyBlocksDto>(jsonString);

        if (surveyDto == null)
            throw new BadRequestException("Invalid JSON");

        var questionsDto = JsonSerializer.Deserialize<SurveyQuestionsDto>(jsonString);

        var mapSurvey = await FindSurveyBlocks(surveyDto, year);

        if (questionsDto != null)
            await FindSurveyQuestions(questionsDto);

        return mapSurvey;
    }

    public async Task<SurveyBlocksDto> FindSurveyBlocks(SurveyBlocksDto surveyDto, int year)
    {
        // Filter out elements where Payload is null or empty
        surveyDto.SurveyElements = (
            surveyDto.SurveyElements ?? throw new BadRequestException("Invalid JSON")
        )
            .Where(element => element.Payload != null && element.Payload.Count > 0)
            .ToArray();

        await MapJsonSurveyBlocks(surveyDto, year);

        return surveyDto;
    }

    public async Task FindSurveyQuestions(SurveyQuestionsDto surveyQuestionsDto)
    {
        surveyQuestionsDto.SurveyElements = (
            surveyQuestionsDto.SurveyElements ?? throw new BadRequestException("Invalid JSON")
        )
            .Where(element => element.Payload != null && element.Payload.Choices.Count > 0)
            .ToArray();

        await MapJsonQuestions(surveyQuestionsDto);
    }

    public async Task GetQuestionsFromStream(Stream csvStream, Guid surveyId)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.Id == surveyId);

        if (survey == null)
            throw new NotFoundException("Survey not found");

        var questions = await _resultService.GetQuestions(survey.SurveyId);

        // Convert the CSV stream to JSON format
        var csvRecords = await _csvToJsonService.ConvertCsvToJson(csvStream);

        // Deserialize the CSV records to a list of dictionaries
        var records = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(csvRecords);

        // Create a HashSet of DataExportTags from questions for quick lookup
        var exportTagSet = new HashSet<string>(questions.Select(q => q.DataExportTag)!);

        // Filter the records to include only those fields present in the exportTagSet and contain numeric values
        if (records != null)
        {
            var filteredFields = records
                .SelectMany(record =>
                    exportTagSet
                        .Where(tag =>
                            record.ContainsKey(tag)
                            && IsNumeric(
                                record[tag].ToString() ?? throw new InvalidOperationException()
                            )
                        ) // Check if the value is a valid number
                        .Select(tag => new FieldDto // Create a new DTO with the field name and its value
                        {
                            ResponseId = record.ContainsKey("ResponseId")
                                ? record["ResponseId"].ToString()
                                : null,
                            FieldName = tag,
                            Value = record[tag].ToString(),
                        })
                )
                .ToList();

            await MapFieldsToResponse(filteredFields, survey.SurveyId);
        }
    }

    public async Task MapFieldsToResponse(List<FieldDto> fieldDto, string surveyId)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s => s.SurveyId == surveyId);

        if (survey == null)
            throw new NotFoundException("Survey not found");

        var questions = await _questionService.GetQuestionsBySurveyIdAsync(surveyId);

        if (questions == null)
            throw new NotFoundException("Questions not found");

        var respondents = fieldDto.Select(f => f.ResponseId).Distinct().ToList();

        //Check if the users already exist in the database
        var users = await _userService.GetUsersBySurveyId(survey.Id);
        await CheckForUsers(respondents, users, survey.Id);

        await _resultService.CheckForDifferences(fieldDto, questions, survey);

        await ConnectResponseToUsers(fieldDto);

        await _aiService.CheckForDifferenceAsync(survey.SurveyId);
    }

    private async Task ConnectResponseToUsers(List<FieldDto> fieldDto)
    {
        var responseUsers = new List<NewResponseUserDto>();

        var users = await _context.Users.ToListAsync();
        var allResponses = await _context.Responses.ToListAsync();
        var responseUsersList = await _context.ResponseUsers.ToListAsync();
        var allQuestions = await _context.Questions.ToListAsync();
        foreach (var field in fieldDto)
        {
            var response = allResponses.FirstOrDefault(r => r.FieldName == field.FieldName);
            if (response == null)
                continue;

            var user = users.FirstOrDefault(u => u.RespondId == field.ResponseId);

            if (user == null)
                throw new NotFoundException("User not found");

            //Check if the response is already connected to the user

            var responseUser = responseUsersList.FirstOrDefault(ru =>
                ru.ResponseId == response.Id && ru.UserId == user.Id
            );

            if (responseUser != null)
                continue;

            var choice = await _context.Choices.FirstOrDefaultAsync(c => c.Id == response.ChoiceId);

            if (choice == null)
                throw new NotFoundException("Choice not found");

            var question = allQuestions.FirstOrDefault(q => q.Id == choice.QuestionId);

            if (question == null)
                throw new NotFoundException("Question not found");

            responseUsers.Add(
                new NewResponseUserDto
                {
                    ResponseId = response.Id,
                    UserId = user.Id,
                    QuestionId = question.Id,
                }
            );
        }

        await _userService.ConnectResponseToUser(responseUsers);
    }

    private async Task CheckForUsers(
        List<string?> respondents,
        IEnumerable<UserDto> users,
        Guid surveyId
    )
    {
        // var users = await _userService.GetUsersBySurveyId(surveyId);

        var existingUsers = users.Select(u => u.RespondId).ToList();

        var newUsers = respondents.Except(existingUsers).ToList();

        if (newUsers.Count > 0)
        {
            var newUserDtos = newUsers
                .Where(u => u != null)
                .Select(u => new NewUserDto { SurveyId = surveyId, RespondId = u! })
                .ToList();

            await _userService.CreateUserBatch(newUserDtos, surveyId);
        }
    }

    private async Task MapJsonSurveyBlocks(SurveyBlocksDto surveyBlockDto, int year)
    {
        var checkSurveyExists = await _context.Surveys.FirstOrDefaultAsync(s =>
            s.SurveyId == surveyBlockDto.SurveyEntry.SurveyId
        );

        if (checkSurveyExists != null)
        {
            Console.WriteLine("Survey already exists, checking for differences");
            await CheckForDifferenceSurvey(surveyBlockDto, checkSurveyExists);
        }
        else
        {
            var survey = await _surveyService.CreateSurveyAsync(
                new NewSurveyDto
                {
                    Name = surveyBlockDto.SurveyEntry.SurveyName,
                    Year = year,
                    SurveyId = surveyBlockDto.SurveyEntry.SurveyId,
                    Language = surveyBlockDto.SurveyEntry.SurveyLanguage,
                }
            );

            if (surveyBlockDto.SurveyElements != null)
            {
                foreach (var surveyElement in surveyBlockDto.SurveyElements)
                {
                    await AddSurveyElements(surveyElement, survey.SurveyId);
                }
            }
        }
    }

    private async Task MapJsonQuestions(SurveyQuestionsDto questionsDto)
    {
        var survey = await _context.Surveys.FirstOrDefaultAsync(s =>
            questionsDto.SurveyElements != null
            && s.SurveyId == questionsDto.SurveyElements.First().SurveyId
        );

        if (survey == null)
            throw new NotFoundException("Survey not found");

        if (questionsDto.SurveyElements == null)
            throw new BadRequestException("Invalid JSON");

        await _questionService.CheckForDifference(questionsDto, survey);
    }

    private async Task AddSurveyElements(SurveyElementBlockDto surveyBlockDto, string surveyId)
    {
        var blockElements = new List<NewBlockElementDto>();

        foreach (var element in surveyBlockDto.Payload.Values)
        {
            var surveyBlock = await _surveyBlockService.CreateSurveyBlock(
                new NewSurveyBlockDto
                {
                    SurveyId = surveyId,
                    Type = element.Type,
                    Description = element.Description,
                    SurveyBlockId = element.Id,
                }
            );

            blockElements.AddRange(
                element.BlockElements.Select(blockElement => new NewBlockElementDto
                {
                    BlockId = surveyBlock.Id,
                    Type = blockElement.Type,
                    QuestionId = blockElement.QuestionId,
                })
            );
        }

        await _blockElementService.CreateBlockElements(blockElements);
    }

    private async Task CheckForDifferenceSurvey(
        SurveyBlocksDto surveyBlockDto,
        Domain.Entities.Survey.Survey survey
    )
    {
        //Find the survey, check if there are any differences in the name, Language
        var surveyEntry = surveyBlockDto.SurveyEntry;

        if (
            survey.Name != surveyEntry.SurveyName
            || survey.SurveyLanguage != surveyEntry.SurveyLanguage
        )
        {
            Console.WriteLine("Survey has been updated, updating survey");
            await _surveyService.UpdateSurveyAsync(
                survey.Id,
                new NewSurveyDto
                {
                    Name = surveyEntry.SurveyName,
                    Language = surveyEntry.SurveyLanguage,
                }
            );
        }

        if (surveyBlockDto.SurveyElements == null)
            return;

        foreach (var surveyElement in surveyBlockDto.SurveyElements)
        {
            var surveyElementsList = survey?.SurveyBlocks?.ToList();

            if (surveyElementsList == null)
                continue;

            //Check for differences in the surveyBlock
            if (survey != null)
            {
                await _surveyBlockService.CheckSurveyBlockElements(survey.Id, surveyElement);
                // await _blockElementService.CheckBlockElements(survey.Id, surveyElement);
            }
        }

        if (survey != null)
            survey.LastSyncedAt = DateTimeOffset.Now;
        await _context.SaveChangesAsync();
    }

    // Helper method to check if a string is numeric
    private bool IsNumeric(string value)
    {
        return !string.IsNullOrEmpty(value) && decimal.TryParse(value, out _); // Return true if the string can be parsed as a number
    }
}
