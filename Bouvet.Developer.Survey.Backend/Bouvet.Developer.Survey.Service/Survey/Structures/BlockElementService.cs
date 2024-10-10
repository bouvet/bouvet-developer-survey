using Bouvet.Developer.Survey.Domain.Entities.Survey;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey.Structures;
using Bouvet.Developer.Survey.Service.TransferObjects.Import.SurveyStructure;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures;
using Microsoft.EntityFrameworkCore;
using BlockElementDto = Bouvet.Developer.Survey.Service.TransferObjects.Survey.Structures.BlockElementDto;

namespace Bouvet.Developer.Survey.Service.Survey.Structures;

public class BlockElementService : IBlockElementService
{
    private readonly DeveloperSurveyContext _context;
    
    public BlockElementService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    public async Task<List<BlockElementDto>> CreateBlockElements(List<NewBlockElementDto> newBlockElementDtos)
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

    private async Task CreateBlockElement(NewBlockElementDto newBlockElementDto)
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
        
        if(blockElement.Type == updateBlockElementDto.Type) 
            return BlockElementDto.CreateFromEntity(blockElement);
        
        blockElement.Type = updateBlockElementDto.Type;
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

    public async Task CheckBlockElements(Guid surveyGuid, SurveyElementBlockDto block)
    {
        var surveyBlocks = await _context.SurveyBlocks.Where(s => s.SurveyGuid == surveyGuid)
            .Include(surveyBlock => surveyBlock.BlockElements)
            .ToListAsync();
        
        var blockElementsList = surveyBlocks.SelectMany(s => s.BlockElements).ToList();
        
        //Look up dictionaries
        var surveyBlockDict = surveyBlocks.ToDictionary(s => s.SurveyBlockId, s => s);
        var blockPayloadDict = block.Payload.Values.ToDictionary(b => b.Id, b => b);
        
        
        var tasks = new List<Task>();
       
        // Check for new block elements to add
        foreach (var blockElement in block.Payload.Values)
        {
            if (!surveyBlockDict.TryGetValue(blockElement.Id, out var findBlock))
                continue;

            foreach (var element in blockElement.BlockElements)
            {
                if (!findBlock.BlockElements.Any(be => be.QuestionId == element.QuestionId))
                {
                    tasks.Add(CreateBlockElement(new NewBlockElementDto
                    {
                        BlockId = findBlock.Id,
                        Type = element.Type,
                        QuestionId = element.QuestionId
                    }));
                }
            }
        }
        
        // Check for block elements to delete
        foreach (var blockElement in blockElementsList)
        {
            if (!blockPayloadDict.TryGetValue(blockElement.QuestionId, out var payloadBlock))
                continue;

            if (payloadBlock.BlockElements.All(be => be.QuestionId != blockElement.QuestionId))
            {
                tasks.Add(DeleteBlockElement(blockElement.Id));
            }
        }

        // Check for block elements to update
        foreach (var blockElement in blockElementsList)
        {
            if (!blockPayloadDict.TryGetValue(blockElement.QuestionId, out var payloadBlock))
                continue;

            var findElement = payloadBlock.BlockElements
                .FirstOrDefault(be => be.QuestionId == blockElement.QuestionId);

            if (findElement != null && findElement.Type != blockElement.Type)
            {
                tasks.Add(UpdateBlockElement(blockElement.Id, new NewBlockElementDto
                {
                    Type = findElement.Type
                }));
            }
        }
        
        await Task.WhenAll(tasks);
    }
}