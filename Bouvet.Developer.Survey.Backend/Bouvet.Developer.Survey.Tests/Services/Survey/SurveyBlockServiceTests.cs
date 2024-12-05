using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class SurveyBlockServiceTests
{
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly ISurveyService _surveyService;
    private readonly DeveloperSurveyContext _context;
    private const string Type = "Test survey";
    private const string SurveyId = "gaf2345";
    private const string Description = "English";
    private const string SurveyName = "Test survey";
    private const string SurveyLanguage = "English";
    
    public SurveyBlockServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _surveyBlockService = new SurveyBlockService(_context);
        _surveyService = new SurveyService(_context);
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
    
    private async Task DeleteAllSurveyBlocks()
    {
        var surveyBlocks = await _context.SurveyBlocks.ToListAsync();
        _context.SurveyBlocks.RemoveRange(surveyBlocks);
        await _context.SaveChangesAsync();
    }
    
    private async Task<SurveyElementDto> CreateTestSurveyBlock()
    {
        var survey = await CreateTestSurvey();
        
        var newSurveyBlockDto = new NewSurveyBlockDto
        {
            Type = Type,
            SurveyId = survey.SurveyId,
            Description = Description,
            SurveyBlockId = "ga_567"
        };

        return await _surveyBlockService.CreateSurveyBlock(newSurveyBlockDto);
    }
    
    [Fact]
    public async Task Should_Create_Survey_Block()
    {
        await DeleteAllSurveyBlocks();
        
        // Arrange
        var surveyBlock = await CreateTestSurveyBlock();
        
        // Act
        var surveyBlocks = await _surveyBlockService.GetSurveyBlocks(surveyBlock.SurveyId);
        
        // Assert
        Assert.NotNull(surveyBlocks);
    }

    [Fact]
    public async Task CreateSurveyBlock_ThrowsNotFoundException_WhenSurveyDoesNotExist()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _surveyBlockService.CreateSurveyBlock(new NewSurveyBlockDto
        {
            SurveyId = "123456",
            Type = "Test",
            Description = "Test",
            SurveyBlockId = "ga_567"
        }));

        Assert.Equal("Survey not found", exception.Message);
    }

    [Fact]
    public async Task GetSurveyBlocks_ThrowsNotFoundException_WhenSurveyDoesNotExist()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _surveyBlockService.GetSurveyBlocks(Guid.NewGuid()));

        Assert.Equal("Survey not found", exception.Message);
    }

    [Fact]
    public async Task GetSurveyBlock_ThrowsNotFoundException_WhenSurveyBlockDoesNotExist()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _surveyBlockService.GetSurveyBlock(Guid.NewGuid()));

        Assert.Equal("Survey block not found", exception.Message);
    }

    [Fact]
    public async Task UpdateSurveyElement_ThrowsNotFoundException_WhenSurveyBlockDoesNotExist()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _surveyBlockService.UpdateSurveyElement(Guid.NewGuid(), new NewSurveyBlockDto
        {
            SurveyId = "123",
            Type = "Test",
            Description = "Test",
            SurveyBlockId = "ga_567"
        }));

        Assert.Equal("Survey block not found", exception.Message);
    }

    [Fact]
    public async Task DeleteSurveyBlock_ThrowsNotFoundException_WhenSurveyBlockDoesNotExist()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _surveyBlockService.DeleteSurveyBlock(Guid.NewGuid()));

        Assert.Equal("Survey block not found", exception.Message);
    }

    
    [Fact]
    public async Task Should_Get_Survey_Block_By_Id()
    {
        // Arrange
        var surveyBlock = await CreateTestSurveyBlock();
        
        // Act
        var surveyBlockFromService = await _surveyBlockService.GetSurveyBlock(surveyBlock.Id);
        
        // Assert
        Assert.NotNull(surveyBlockFromService);
        Assert.Equal(surveyBlock.Id, surveyBlockFromService.Id);
    }
    
    [Fact]
    public async Task Should_Update_Survey_Block()
    {
        // Arrange
        var surveyBlock = await CreateTestSurveyBlock();
        
        var newSurveyBlockDto = new NewSurveyBlockDto
        {
            Type = "Updated type",
            SurveyId = SurveyId,
            Description = "Updated description",
            SurveyBlockId = "ga_567"
        };
        
        // Act
        var updatedSurveyBlock = await _surveyBlockService.UpdateSurveyElement(surveyBlock.Id, newSurveyBlockDto);
        
        // Assert
        Assert.Equal(newSurveyBlockDto.Description, updatedSurveyBlock.Description);
    }
    
    [Fact]
    public async Task Should_Delete_Survey_Block()
    {
        // Arrange
        var surveyBlock = await CreateTestSurveyBlock();
        
        // Act
        await _surveyBlockService.DeleteSurveyBlock(surveyBlock.Id);
        
        // Assert
        var surveyBlocks = await Assert.ThrowsAsync<NotFoundException>(() => _surveyBlockService.GetSurveyBlock(surveyBlock.Id));
        
        Assert.Equal("Survey block not found", surveyBlocks.Message);
    }
    
}