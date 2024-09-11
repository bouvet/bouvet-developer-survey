using Bouvet.Developer.Survey.Domain.Entities;
using Bouvet.Developer.Survey.Domain.Exceptions;
using Bouvet.Developer.Survey.Infrastructure.Data;
using Bouvet.Developer.Survey.Service.Interfaces.Survey;
using Bouvet.Developer.Survey.Service.TransferObjects.Survey;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.Developer.Survey.Service.Survey;

public class OptionService : IOptionService
{
    private readonly DeveloperSurveyContext _context;
    
    public OptionService(DeveloperSurveyContext context)
    {
        _context = context;
    }
    
    public async Task<OptionDto> CreateOptionAsync(NewOptionDto newOptionDto)
    {
       var option = new Option
       {
           Value = newOptionDto.Value,
           BlockId = newOptionDto.BlockId
       };
       
        await _context.Options.AddAsync(option);
        await _context.SaveChangesAsync();
            
        var dto = OptionDto.CreateFromEntity(option);
        return dto;
    }

    public async Task<BlockDto> GetOptionsToBlockAsync(Guid blockId)
    {
        var block = await _context.Blocks
            .FirstOrDefaultAsync(b => b.Id == blockId);
        
        if(block == null) throw new NotFoundException("Options to block not found");
        
        var blockDto = BlockDto.CreateFromEntity(block);
        
        return blockDto;
    }

    public async Task<OptionDto> GetOptionAsync(Guid optionId)
    {
        var option = await _context.Options.FirstOrDefaultAsync(o => o.Id == optionId);

        if(option == null) throw new NotFoundException("Option not found");

        var optionDto = OptionDto.CreateFromEntity(option);

        return optionDto;
    }

    public async Task<OptionDto> UpdateOptionAsync(Guid optionId, NewOptionDto newOptionDto)
    {
        var option = await _context.Options.FirstOrDefaultAsync(o => o.Id == optionId);

        if(option == null) throw new NotFoundException("Option not found");

        if(newOptionDto.Value == null) throw new ArgumentNullException(nameof(newOptionDto.Value));
        
        option.Value = newOptionDto.Value;

        await _context.SaveChangesAsync();

        var optionDto = OptionDto.CreateFromEntity(option);

        return optionDto;
    }

    public async Task DeleteOptionAsync(Guid optionId)
    {
        var option = await _context.Options.FirstOrDefaultAsync(o => o.Id == optionId);
        
        if(option == null) throw new NotFoundException("Option not found");

        option.DeletedAt = DateTimeOffset.Now;
        
        _context.Options.Update(option);
        await _context.SaveChangesAsync();
    }
}