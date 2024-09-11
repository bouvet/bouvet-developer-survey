using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Bouvet.Developer.Survey.Tests.Builders.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class BlockServiceTests
{
    private readonly IBlockService _blockService;
    private readonly DeveloperSurveyContext _context;
    private readonly SurveyBuilder _surveyBuilder = new();
    private const string BlockQuestion = "Test question";
    private const string BlockText = "Text";
    public BlockServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _blockService = new BlockService(_context);
    }

    [Fact]
    public async Task Should_Create_Block()
    {
        var survey = _surveyBuilder.Build();
        
        // Arrange
        var newBlockDto = new NewBlockDto
        {
            Question = BlockQuestion,
            Type = BlockText,
            SurveyId = survey.Id
        };
        
        // Act
        var block = await _blockService.CreateBlockAsync(newBlockDto);
        
        // Assert
        Assert.NotNull(block);
        Assert.Equal(newBlockDto.Question, block.Question);
        Assert.Equal(newBlockDto.Type, block.Type);
        Assert.Equal(newBlockDto.SurveyId, block.SurveyId);
    }

    [Fact]
    public async Task Should_Get_Blocks_To_Survey()
    {
        var survey = _surveyBuilder.Build();

        // Arrange
        var blocks = await _blockService.GetBlocksToSurveyAsync(survey.Id);
        
        // Assert
        Assert.NotNull(blocks);
        Assert.Equal(survey.Id, blocks.Id);
    }
    
    [Fact]
    public async Task Should_Trigger_NotFoundException_When_Getting_Blocks_To_NonExisting_Survey()
    {
        // Arrange
        var surveyId = Guid.NewGuid();
        
        // Act
        var blocks = await Assert.ThrowsAsync<NotFoundException>(() => _blockService.GetBlocksToSurveyAsync(surveyId));
        
        // Assert
        Assert.Equal("Blocks to survey not found", blocks.Message);
    }
    
    [Fact]
    public async Task Should_Get_Block_By_Id()
    {
        var survey = _surveyBuilder.Build();
        
        // Arrange
        var newBlockDto = new NewBlockDto
        {
            Question = BlockQuestion,
            Type = BlockText,
            SurveyId = survey.Id
        };
        
        // Act
        var newBlock = await _blockService.CreateBlockAsync(newBlockDto);
        var blockDto = await _blockService.GetBlockAsync(newBlock.Id);
        
        // Assert
        Assert.NotNull(blockDto);
        Assert.Equal(blockDto.Id, blockDto.Id);
    }
    
    [Fact]
    public async Task Should_Update_Block()
    {
        var survey = _surveyBuilder.Build();

        // Arrange
        var newBlockDto = new NewBlockDto
        {
            Question = BlockQuestion,
            Type = BlockText,
            SurveyId = survey.Id
        };

        var updateBlock = new NewBlockDto
        {
            Question = "Updated question"
        };
        
        // Act
        var newBlock = await _blockService.CreateBlockAsync(newBlockDto);
        var updatedBlockDto = await _blockService.UpdateBlockAsync(newBlock.Id, updateBlock);
        
        var getUpdatedBlock = await _blockService.GetBlockAsync(newBlock.Id);
        
        // Assert
        Assert.NotNull(updatedBlockDto);
        Assert.Equal(updateBlock.Question, updatedBlockDto.Question);
        Assert.Equal(newBlockDto.Type, getUpdatedBlock.Type);
        Assert.Equal(newBlockDto.SurveyId, getUpdatedBlock.SurveyId);
    }
    
    [Fact]
    public async Task Should_Trigger_NotFoundException_When_Updating_NonExisting_Block()
    {
        // Arrange
        var blockId = Guid.NewGuid();
        var updateBlock = new NewBlockDto
        {
            Question = "Updated question"
        };
        
        // Act
        var block = await Assert.ThrowsAsync<NotFoundException>(() => _blockService.UpdateBlockAsync(blockId, updateBlock));
        
        // Assert
        Assert.Equal("Block not found", block.Message);
    }
    
    [Fact]
    public async Task Should_Delete_Block()
    {
        var survey = _surveyBuilder.Build();

        // Arrange
        var newBlockDto = new NewBlockDto
        {
            Question = BlockQuestion,
            Type = BlockText,
            SurveyId = survey.Id
        };
        
        // Act
        var newBlock = await _blockService.CreateBlockAsync(newBlockDto);
        
        await _blockService.DeleteBlockAsync(newBlock.Id);
        
        var deletedBlock = await Assert.ThrowsAsync<NotFoundException>(() => _blockService.GetBlockAsync(newBlock.Id));
        
        // Assert
        Assert.Equal("Block not found", deletedBlock.Message);
    }
}