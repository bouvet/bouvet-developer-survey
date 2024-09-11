using Bouvet.Developer.Survey.Domain.Entities;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey;

public class BlockService : IBlockService
{
    private readonly DeveloperSurveyContext _context;
    
    public BlockService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    
    public async Task<BlockDto> CreateBlockAsync(NewBlockDto newBlockDto)
    {
        var block = new Block
        {
            Id = Guid.NewGuid(),
            Question = newBlockDto.Question ?? throw new ArgumentNullException(nameof(newBlockDto.Question)),
            Type = newBlockDto.Type ?? throw new ArgumentNullException(nameof(newBlockDto.Type)),
            SurveyId = newBlockDto.SurveyId,
            CreatedAt = DateTimeOffset.Now
        };
        
        await _context.Blocks.AddAsync(block);
        await _context.SaveChangesAsync();
        
        var blockDto = BlockDto.CreateFromEntity(block);
        return blockDto;
    }

    public async Task<SurveyDto> GetBlocksToSurveyAsync(Guid surveyId)
    {
        var survey = await _context.Surveys
            .FirstOrDefaultAsync(s => s.Id == surveyId);
        
        if(survey == null) throw new NotFoundException("Blocks to survey not found");
        
        var surveyDto = SurveyDto.CreateFromEntity(survey);
        
        return surveyDto;
    }

    public async Task<BlockDto> GetBlockAsync(Guid blockId)
    {
        var block = await _context.Blocks.FirstOrDefaultAsync(b => b.Id == blockId);

        if(block == null) throw new NotFoundException("Block not found");

        var blockDto = BlockDto.CreateFromEntity(block);

        return blockDto;
    }

    public async Task<BlockDto> UpdateBlockAsync(Guid blockId, NewBlockDto newBlockDto)
    {
        var updateBlock = await _context.Blocks.FirstOrDefaultAsync(b => b.Id == blockId);
        
        if(updateBlock == null) throw new NotFoundException("Block not found");
        
        
        updateBlock.Question = newBlockDto.Question ?? updateBlock.Question;
        updateBlock.Type = newBlockDto.Type ?? updateBlock.Type;
        
        updateBlock.UpdatedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
        
        var blockDto = BlockDto.CreateFromEntity(updateBlock);
        
        return blockDto;
    }

    public async Task DeleteBlockAsync(Guid blockId)
    {
        var blockToDelete = await _context.Blocks.FirstOrDefaultAsync(b => b.Id == blockId);
        
        if(blockToDelete == null) throw new NotFoundException("Block not found");
        
        blockToDelete.DeletedAt = DateTimeOffset.Now;
        
        _context.Blocks.Update(blockToDelete);
        await _context.SaveChangesAsync();
    }
}