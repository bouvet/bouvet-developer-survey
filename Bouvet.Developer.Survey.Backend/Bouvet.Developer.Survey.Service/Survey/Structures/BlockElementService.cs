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
    public async Task<BlockElementDto> CreateBlockElement(NewBlockElementDto newBlockElementDto)
    {
        var block = await _context.SurveyBlocks.FirstOrDefaultAsync(b => b.Id == newBlockElementDto.BlockId);
        
        if (block == null) throw new NotFoundException("Block not found");
        
        var blockElement = new BlockElement
        {
            Id = Guid.NewGuid(),
            SurveyElementGuid = newBlockElementDto.BlockId,
            Type = newBlockElementDto.Type,
            QuestionId = newBlockElementDto.QuestionId,
            CreatedAt = DateTimeOffset.Now
        };
        
        await _context.BlockElements.AddAsync(blockElement);
        await _context.SaveChangesAsync();
        
        var dto = BlockElementDto.CreateFromEntity(blockElement);
        
        return dto;
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