using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class AnswerServiceTests
{
    private readonly ISurveyService _surveyService;
    private readonly IAnswerOptionService _answerOptionService;
    private readonly DeveloperSurveyContext _context;
    private const string SurveyId = "gaf2345";
    private const string SurveyName = "Test survey";
    private const string SurveyLanguage = "English";
    private const string AnswerText = "Test answer";
    private const string AnswerIndexId = "1";
    
    
    public AnswerServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "AnswerTestDb")
            .Options;

        _context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _surveyService = new SurveyService(_context);
        _answerOptionService = new AnswerOptionService(_context);
    }
    
    private async Task<SurveyDto> CreateTestSurvey()
    {
        var newSurveyDto = new NewSurveyDto
        {
            Name = SurveyName,
            SurveyId = SurveyId,
            Language = SurveyLanguage
        };

        var survey = await _surveyService.CreateSurveyAsync(newSurveyDto);
        
        return survey;
    }
    
    private async Task<AnswerOptionDto> CreateTestAnswerOption()
    {
        var survey = await CreateTestSurvey();
        
        var newAnswerOptionDto = new NewAnswerOptionDto
        {
            SurveyId = survey.Id,
            Text = AnswerText,
            IndexId = AnswerIndexId
        };

        var answerOption = await _answerOptionService.CreateAnswerOption(newAnswerOptionDto);
        
        return answerOption;
    }
    
    private async Task DeleteAllAnswerOptions()
    {
        var answerOptions = await _context.AnswerOptions.ToListAsync();
        _context.AnswerOptions.RemoveRange(answerOptions);
        await _context.SaveChangesAsync();
    }
    
    [Fact]
    public async Task Create_Answer_Option()
    {
        await DeleteAllAnswerOptions();
        
        var answerOption = await CreateTestAnswerOption();
        
        Assert.NotNull(answerOption);
        Assert.Equal(AnswerText, answerOption.Text);
        Assert.Equal(AnswerIndexId, answerOption.IndexId);
        
        var testError = await Assert.ThrowsAsync<NotFoundException>(() => _answerOptionService.CreateAnswerOption(new NewAnswerOptionDto
        {
            SurveyId = Guid.NewGuid(),
            Text = AnswerText,
            IndexId = AnswerIndexId
        }));
        
        Assert.Equal("Survey not found", testError.Message);
    }
    
    [Fact]
    public async Task Add_Answer_From_Dto()
    {
        await DeleteAllAnswerOptions();
        
        var survey = await CreateTestSurvey();
        
        var questionDto = new PayloadQuestionDto
        {
            Answers = new Dictionary<string, ChoicesDto>
            {
                {"1", new ChoicesDto
                {
                    Display = "Wish"
                }}
            }
        };
        
        await _answerOptionService.AddAnswerFromDto(survey.Id, questionDto);
        
        var answerOption = await _context.AnswerOptions.FirstOrDefaultAsync();
        
        Assert.NotNull(answerOption);
        Assert.Equal("Wish", answerOption.Text);
        Assert.Equal(AnswerIndexId, answerOption.IndexId);
        
        var testError = await Assert.ThrowsAsync<NotFoundException>(() => _answerOptionService.AddAnswerFromDto(new Guid(), questionDto));
        
        Assert.Equal("Survey not found", testError.Message);
    }
    
    [Fact]
    public async Task Update_Answer_Option()
    {
        await DeleteAllAnswerOptions();
        
        var answerOption = await CreateTestAnswerOption();
        
        var updatedAnswerOption = await _answerOptionService.UpdateAnswerOption(answerOption.Id, new NewAnswerOptionDto
        {
            Text = "Updated answer",
            IndexId = "2"
        });
        
        Assert.NotNull(updatedAnswerOption);
        Assert.Equal("Updated answer", updatedAnswerOption.Text);
        Assert.Equal("2", updatedAnswerOption.IndexId);
        
        var testError = await Assert.ThrowsAsync<NotFoundException>(() => _answerOptionService.UpdateAnswerOption(Guid.NewGuid(), new NewAnswerOptionDto
        {
            Text = "Updated answer",
            IndexId = "2"
        }));
        
        Assert.Equal("Answer option not found", testError.Message);
    }
    
    [Fact]
    public async Task Delete_Answer_Option()
    {
        await DeleteAllAnswerOptions();
        
        var answerOption = await CreateTestAnswerOption();
        
        await _answerOptionService.DeleteAnswerOption(answerOption.Id);
        
        var answerOptions = await _context.AnswerOptions.ToListAsync();
        
        Assert.Empty(answerOptions);
        
        var testError = await Assert.ThrowsAsync<NotFoundException>(() => _answerOptionService.DeleteAnswerOption(Guid.NewGuid()));
        
        Assert.Equal("Answer option not found", testError.Message);
    }
}