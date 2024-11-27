// using Bouvet.Developer.Survey.Domain.Exceptions;
// using Bouvet.Developer.Survey.Infrastructure.Data;
// using Bouvet.Developer.Survey.Service.Interfaces.Survey;
// using Bouvet.Developer.Survey.Service.Survey;
// using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
// using Bouvet.Developer.Survey.Tests.Builders.Entities;
// using Microsoft.EntityFrameworkCore;
// using Xunit;
//
// namespace Bouvet.Developer.Survey.Tests.Services.Survey;
//
// public class OptionServiceTests
// {
//     private readonly IOptionService _optionService;
//     private readonly IBlockService _blockService;
//     private readonly SurveyBuilder _surveyBuilder = new();
//     
//     private readonly DeveloperSurveyContext _context;
//     private const string OptionValue = "Test survey";
//     private const string BlockQuestion = "Test question";
//     private const string BlockText = "Text";
//
//
//     public OptionServiceTests()
//     {
//         // Setting up an in-memory database for the context
//         var options = new DbContextOptionsBuilder<DeveloperSurveyContext>()
//             .UseInMemoryDatabase(databaseName: "TestDatabase")
//             .Options;
//
//         _context = new DeveloperSurveyContext(options);
//
//         // Injecting the in-memory context into the service
//         _optionService = new OptionService(_context);
//         _blockService = new BlockService(_context);
//         
//     }
//     
//     private async Task<BlockDto> CreateTestBlockAsync(Guid surveyId)
//     {
//         var newBlockDto = new NewBlockDto
//         {
//             Question = BlockQuestion,
//             Type = BlockText,
//             SurveyId = surveyId
//         };
//
//         return await _blockService.CreateBlockAsync(newBlockDto);
//     }
//
//     private async Task<OptionDto> CreateTestOptionAsync(Guid blockId)
//     {
//         var newOptionDto = new NewOptionDto
//         {
//             Value = OptionValue,
//             BlockId = blockId
//         };
//
//         return await _optionService.CreateOptionAsync(newOptionDto);
//     }
//     
//     [Fact]
//     public async Task Should_Create_Option()
//     {
//         var survey = _surveyBuilder.Build();
//         var block = await CreateTestBlockAsync(survey.Id);
//
//         // Act
//         var option = await CreateTestOptionAsync(block.Id);
//
//         // Assert
//         Assert.NotNull(option);
//         Assert.Equal(OptionValue, option.Value);
//         Assert.Equal(block.Id, option.BlockId);
//     }
//
//     [Fact]
//     public async Task Should_Get_Options_To_Block()
//     {
//         var survey = _surveyBuilder.Build();
//         var block = await CreateTestBlockAsync(survey.Id);
//         await CreateTestOptionAsync(block.Id);
//
//         // Act
//         var optionsToBlock = await _optionService.GetOptionsToBlockAsync(block.Id);
//
//         // Assert
//         Assert.NotNull(optionsToBlock);
//         Assert.Single(optionsToBlock.Options!);
//     }
//     
//     [Fact]
//     public async Task Should_Get_Option_By_Id()
//     {
//         var survey = _surveyBuilder.Build();
//         
//         var block = await CreateTestBlockAsync(survey.Id);
//         var newOption = await CreateTestOptionAsync(block.Id);
//         
//         // Act
//         var option = await _optionService.GetOptionAsync(newOption.Id);
//         
//         // Assert
//         Assert.NotNull(option);
//         Assert.Equal(newOption.Value, option.Value);
//         Assert.Equal(newOption.BlockId, option.BlockId);
//     }
//     
//     [Fact]
//     public async Task Should_Update_Option()
//     {
//         var survey = _surveyBuilder.Build();
//         
//         var block = await CreateTestBlockAsync(survey.Id);
//         var newOption = await CreateTestOptionAsync(block.Id);
//         
//         var updatedOptionDto = new NewOptionDto
//         {
//             Value = "Updated option",
//             BlockId = block.Id
//         };
//         
//         // Act
//         var updatedOption = await _optionService.UpdateOptionAsync(newOption.Id, updatedOptionDto);
//         
//         // Assert
//         Assert.NotNull(updatedOption);
//         Assert.Equal(updatedOptionDto.Value, updatedOption.Value);
//     }
//     
//     [Fact]
//     public async Task Should_Delete_Option()
//     {
//         var survey = _surveyBuilder.Build();
//         
//         var block = await CreateTestBlockAsync(survey.Id);
//         var newOption = await CreateTestOptionAsync(block.Id);
//         
//         // Act
//         await _optionService.DeleteOptionAsync(newOption.Id);
//         
//         // Assert
//         var option = await Assert.ThrowsAsync<NotFoundException>(() => _optionService.GetOptionAsync(newOption.Id));
//         Assert.Equal("Option not found", option.Message);
//     }
//     
//     [Fact]
//     public async Task Should_Trigger_NotFoundException_When_Updating_NonExisting_Option()
//     {
//         // Arrange
//         var optionId = Guid.NewGuid();
//         var updateOption = new NewOptionDto
//         {
//             Value = "Updated option",
//             BlockId = Guid.NewGuid()
//         };
//         
//         // Act
//         var option = await Assert.ThrowsAsync<NotFoundException>(() => _optionService.UpdateOptionAsync(optionId, updateOption));
//         
//         // Assert
//         Assert.Equal("Option not found", option.Message);
//     }
// }