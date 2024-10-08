using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class BlockElementService : IBlockElementService
{
    private readonly DeveloperSurveyContext _context;
    
    public BlockElementService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    public async Task<List<BlockElementDto>> CreateBlockElement(List<NewBlockElementDto> newBlockElementDtos)
    {
        var blockIds = newBlockElementDtos.Select(dto => dto.BlockId).Distinct().ToList();
        
        var blocks = await _context.SurveyBlocks
            .Where(b => blockIds.Contains(b.Id))
            .ToListAsync();
        
        if(blocks.Count != blockIds.Count)
        {
            var missingBlockIds = blockIds.Except(blocks.Select(b => b.Id)).ToList();
            throw new NotFoundException($"Blocks not found: {string.Join(", ", missingBlockIds)}");
        }
        
        //Create BlockElements
        var blockElements = newBlockElementDtos.Select(dto => new BlockElement
        {
            Id = Guid.NewGuid(),
            SurveyElementGuid = dto.BlockId,
            Type = dto.Type,
            QuestionId = dto.QuestionId,
            CreatedAt = DateTimeOffset.Now
        }).ToList();
        
        await _context.BlockElements.AddRangeAsync(blockElements);
        await _context.SaveChangesAsync();
        
        var dtos = blockElements.Select(BlockElementDto.CreateFromEntity).ToList();
        
        return dtos;
        
    }

    public async Task<BlockElementDto> GetBlockElementById(Guid blockElementId)
    {
        var blockElement = await _context.BlockElements.FirstOrDefaultAsync(be => be.Id == blockElementId);
        
        if (blockElement == null) throw new NotFoundException("Block element not found");
        
        var dto = BlockElementDto.CreateFromEntity(blockElement);
        
        return dto;
    }

    public async Task<IEnumerable<BlockElementDto>> GetBlockElementsByBlockId(Guid blockId)
    {
        var block = await _context.SurveyBlocks.FirstOrDefaultAsync(b => b.Id == blockId);
        
        if (block == null) throw new NotFoundException("Block not found");
        
        var blockElements = await _context.BlockElements.Where(be => be.SurveyElementGuid == blockId).ToListAsync();
        
        var blockElementList = blockElements.Select(BlockElementDto.CreateFromEntity).ToList();
        
        return blockElementList;
    }

    public async Task<BlockElementDto> UpdateBlockElement(Guid blockElementId, NewBlockElementDto updateBlockElementDto)
    {
        var blockElement = await _context.BlockElements.FirstOrDefaultAsync(be => be.Id == blockElementId);
        
        if (blockElement == null) throw new NotFoundException("Block element not found");
        
        blockElement.Type = updateBlockElementDto.Type;
        blockElement.QuestionId = updateBlockElementDto.QuestionId;
        blockElement.UpdatedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
        
        var dto = BlockElementDto.CreateFromEntity(blockElement);
        
        return dto;
    }

    public async Task DeleteBlockElement(Guid blockId)
    {
        var blockElement = await _context.BlockElements.FirstOrDefaultAsync(be => be.Id == blockId);
        
        if (blockElement == null) throw new NotFoundException("Block element not found");
        
        blockElement.DeletedAt = DateTimeOffset.Now;
        
        await _context.SaveChangesAsync();
    }
}