using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class BlockElementServiceTests
{
    private readonly IBlockElementService _blockElementService;
    private readonly ISurveyBlockService _surveyBlockService;
    private readonly ISurveyService _surveyService;
    private readonly DeveloperSurveyContext _context;
    private const string Type = "Test survey";
    private const string SurveyId = "gaf2345";
    private const string Description = "English";
    private const string SurveyName = "Test survey";
    private const string SurveyLanguage = "English";
    private const string ElementType = "TEXT";
    private const string ElementQuestionId = "QID2";
    public BlockElementServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _blockElementService = new BlockElementService(_context);
        _surveyBlockService = new SurveyBlockService(_context);
        _surveyService = new SurveyService(_context);
    }
    
    private async Task CreateTestSurvey()
    {
        var newSurveyDto = new NewSurveyDto
        {
            Name = SurveyName,
            SurveyId = SurveyId,
            Language = SurveyLanguage
        };

        await _surveyService.CreateSurveyAsync(newSurveyDto);
    }
    
    private async Task DeleteAllSurveyBlocks()
    {
        var surveyBlocks = await _context.SurveyBlocks.ToListAsync();
        _context.SurveyBlocks.RemoveRange(surveyBlocks);
        await _context.SaveChangesAsync();
    }
    
    private async Task<SurveyElementDto> CreateTestSurveyBlock()
    {
        await CreateTestSurvey();
        
        var newSurveyBlockDto = new NewSurveyBlockDto
        {
            Type = Type,
            SurveyId = SurveyId,
            Description = Description,
            SurveyBlockId = "ga_567"
        };

        return await _surveyBlockService.CreateSurveyBlock(newSurveyBlockDto);
    }
    
    private async Task<List<BlockElementDto>> CreateTestBlockElementAsync(Guid blockId)
    {
        var listElementDto = new List<NewBlockElementDto>();
        
        var newBlockDto = new NewBlockElementDto
        {
            QuestionId = ElementQuestionId,
            Type = ElementType,
            BlockId = blockId
        };
        
        listElementDto.Add(newBlockDto);

        return await _blockElementService.CreateBlockElements(listElementDto);
    }

    [Fact]
    public async Task Should_Create_Block()
    {
        var testBlock = await CreateTestSurveyBlock();
        
        // Act
        var block = await CreateTestBlockElementAsync(testBlock.Id);
        
        // Assert
        Assert.NotNull(block);
        Assert.Equal(ElementType, block.First().Type);
        
        var blockElementById = await _blockElementService.GetBlockElementById(block.First().Id);
        
        Assert.NotNull(blockElementById);
        Assert.Equal(ElementType, blockElementById.Type);
    }
    
    [Fact]
    public async Task Delete_BlockElement_With_Wrong_Id()
    {
        var blockElementDelete = await Assert.ThrowsAsync<NotFoundException>(() => _blockElementService.DeleteBlockElement(Guid.NewGuid()));
        
        Assert.Equal("Block element not found", blockElementDelete.Message);
    }
    
    [Fact]
    public async Task Create_Block_With_Wrong_BlockId()
    {
        // Arrange
        var listElementDto = new List<NewBlockElementDto>();
        var newBlockDto = new NewBlockElementDto
        {
            QuestionId = ElementQuestionId,
            Type = ElementType,
            BlockId = Guid.NewGuid()
        };
        
        listElementDto.Add(newBlockDto);
        
        // Act
        var createBlock = await Assert.ThrowsAsync<NotFoundException>(() => _blockElementService.CreateBlockElements(listElementDto));
        
        // Assert
        Assert.Equal("Blocks not found: " + newBlockDto.BlockId, createBlock.Message);
    }

    [Fact]
    public async Task Get_BlockElement_By_Wrong_Id()
    {
        var blockElementGet = await Assert.ThrowsAsync<NotFoundException>(() => _blockElementService.GetBlockElementById(Guid.NewGuid()));
        
        Assert.Equal("Block element not found", blockElementGet.Message);
    }

    [Fact]
    public async Task Update_Block_With_Wrong_Id()
    {
        var blockElementUpdate = await Assert.ThrowsAsync<NotFoundException>(() => _blockElementService.UpdateBlockElement(Guid.NewGuid(), new NewBlockElementDto()));
        
        Assert.Equal("Block element not found", blockElementUpdate.Message);
    }
    
    [Fact]
    public async Task Should_Get_BlockElements()
    {
        var testBlock = await CreateTestSurveyBlock();
        
        // Arrange
        await CreateTestBlockElementAsync(testBlock.Id);
        await CreateTestBlockElementAsync(testBlock.Id);
        
        // Act
        var blockElement = await _blockElementService.GetBlockElementsByBlockId(testBlock.Id);
        
        // Assert
        Assert.NotNull(blockElement);
        
        var elementDtos = blockElement.ToList();
        Assert.Equal(2, elementDtos.Count());
        
        var blockElementDtos = elementDtos.ToList();
        
        Assert.Equal(ElementType, blockElementDtos.First().Type);
        Assert.Equal(ElementType, blockElementDtos.Last().Type);
    }

    [Fact]
    public async Task Should_Update_BlockElement()
    {
        var testBlock = await CreateTestSurveyBlock();
        
        // Arrange
        var blockElement = await CreateTestBlockElementAsync(testBlock.Id);
        
        var updatedBlockElementDto = new NewBlockElementDto
        {
            Type = "TEXT",
            BlockId = testBlock.Id
        };
        
        // Act
        var updatedBlockElement = await _blockElementService.UpdateBlockElement(blockElement.First().Id, updatedBlockElementDto);
        
        // Assert
        Assert.NotNull(updatedBlockElement);
        Assert.Equal(updatedBlockElementDto.Type, updatedBlockElement.Type);
    }

    [Fact]
    public async Task Should_DeleteBlock_Element()
    {
        var testBlock = await CreateTestSurveyBlock();
        
        // Arrange
        var blockElement = await CreateTestBlockElementAsync(testBlock.Id);
        
        // Act
        await _blockElementService.DeleteBlockElement(blockElement.First().Id);
        
        // Assert
        var blockElementGet = await Assert.ThrowsAsync<NotFoundException>(() => _blockElementService.GetBlockElementById(blockElement.First().Id));
        
        Assert.Equal("Block element not found", blockElementGet.Message);
    }
    
}