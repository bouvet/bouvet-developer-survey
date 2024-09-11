using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Bouvet.Developer.Survey.Tests.Builders.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bouvet.Developer.Survey.Tests.Services.Survey;

public class OptionServiceTests
{
    private readonly IOptionService _optionService;
    private readonly SurveyBuilder _surveyBuilder = new();
    private readonly BlockBuilder _blockBuilder = new();
    private readonly DeveloperSurveyContext _context;
    private const string OptionValue = "Test survey";


    public OptionServiceTests()
    {
        // Setting up an in-memory database for the context
        var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new DeveloperSurveyContext(options);

        // Injecting the in-memory context into the service
        _optionService = new OptionService(_context);
    }
    
    [Fact]
    public async Task Should_Create_Option()
    {
        var block = _blockBuilder.Build();
        _surveyBuilder.Build();
        
        // Arrange
        var newOptionDto = new NewOptionDto
        {
            Value = OptionValue,
            BlockId = block.Id
        };
        
        // Act
        var option = await _optionService.CreateOptionAsync(newOptionDto);
        
        // Assert
        Assert.NotNull(option);
        Assert.Equal(newOptionDto.Value, option.Value);
        Assert.Equal(newOptionDto.BlockId, option.BlockId);
    }
    
    [Fact]
    public async Task Should_Get_Options_To_Block()
    {
        _surveyBuilder.Build();
        var block = _blockBuilder.Build();
        
        // Arrange
        var newOptionDto = new NewOptionDto
        {
            Value = OptionValue,
            BlockId = block.Id
        };

        await _optionService.CreateOptionAsync(newOptionDto);
        
        // Act
        var optionsToBlock = await _optionService.GetOptionsToBlockAsync(block.Id);
        
        // Assert
        Assert.NotNull(optionsToBlock);
        Assert.Equal(1, optionsToBlock.Options.Count);
    }
    
    [Fact]
    public async Task Should_Get_Option_By_Id()
    {
        var block = _blockBuilder.Build();

        // Arrange
        var newOptionDto = new NewOptionDto
        {
            Value = OptionValue,
            BlockId = block.Id
        };

        var newOption = await _optionService.CreateOptionAsync(newOptionDto);
        
        // Act
        var option = await _optionService.GetOptionAsync(newOption.Id);
        
        // Assert
        Assert.NotNull(option);
        Assert.Equal(newOptionDto.Value, option.Value);
        Assert.Equal(newOptionDto.BlockId, option.BlockId);
    }
    
    [Fact]
    public async Task Should_Update_Option()
    {
        var block = _blockBuilder.Build();

        // Arrange
        var newOptionDto = new NewOptionDto
        {
            Value = OptionValue,
            BlockId = block.Id
        };

        var newOption = await _optionService.CreateOptionAsync(newOptionDto);
        
        var updatedOptionDto = new NewOptionDto
        {
            Value = "Updated option",
            BlockId = block.Id
        };
        
        // Act
        var updatedOption = await _optionService.UpdateOptionAsync(newOption.Id, updatedOptionDto);
        
        // Assert
        Assert.NotNull(updatedOption);
        Assert.Equal(updatedOptionDto.Value, updatedOption.Value);
    }
    
    [Fact]
    public async Task Should_Delete_Option()
    {
        var block = _blockBuilder.Build();

        // Arrange
        var newOptionDto = new NewOptionDto
        {
            Value = OptionValue,
            BlockId = block.Id
        };

        var newOption = await _optionService.CreateOptionAsync(newOptionDto);
        
        // Act
        await _optionService.DeleteOptionAsync(newOption.Id);
        
        // Assert
        var option = await Assert.ThrowsAsync<NotFoundException>(() => _optionService.GetOptionAsync(newOption.Id));
        Assert.Equal("Option not found", option.Message);
    }
    
    [Fact]
    public async Task Should_Trigger_NotFoundException_When_Updating_NonExisting_Option()
    {
        // Arrange
        var optionId = Guid.NewGuid();
        var updateOption = new NewOptionDto
        {
            Value = "Updated option",
            BlockId = Guid.NewGuid()
        };
        
        // Act
        var option = await Assert.ThrowsAsync<NotFoundException>(() => _optionService.UpdateOptionAsync(optionId, updateOption));
        
        // Assert
        Assert.Equal("Option not found", option.Message);
    }
}