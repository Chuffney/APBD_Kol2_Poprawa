using System.Data;
using Kolokwium2.Data;
using Kolokwium2.DTOs;
using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<CharacterDto?> GetCharacterById(int id)
    {
        var character = _context.Characters.Where(e => e.CharacterId == id)
            .Select(e => new CharacterDto()
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    CurrentWeight = e.CurrentWeight,
                    MaxWeight = e.MaxWeight,
                    BackpackItems = e.Backpacks.Select(ei => new ItemDto()
                    {
                        ItemName = ei.Item.Name,
                        ItemWeight = ei.Item.Weight,
                        Amount = ei.Amount
                    }).ToList(),
                    Titles = e.Titles.Select(ei => new TitleDto()
                    {
                        Title = ei.Title.Name,
                        AcquiredAt = ei.AcquiredAt,

                    }).ToList(),
                }
            );
        return await character.FirstOrDefaultAsync();
    }
    

    public async Task AddItem(int characterId, AddItemDto item)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            int invalidItems = item.Items.Count(e => !_context.Items.Select(ei => ei.ItemId).Contains(e));
            if (invalidItems > 0)
                throw new Exception("invalid item");

            int addedMass = item.Items.Select(e => _context.Items.Where(ei => ei.ItemId == e).Select(ei => ei.Weight).FirstOrDefault()).Sum();

            var character = await _context.Characters.FirstOrDefaultAsync(e => e.CharacterId == characterId);
            
            if (character.CurrentWeight + addedMass > character.MaxWeight)
                throw new Exception("items too heavy");
            
            character.CurrentWeight += addedMass;

            foreach (var itemId in item.Items)
            {
                if (!character.Backpacks.Select(e => e.ItemId).Contains(itemId))
                {
                    _context.Backpacks.Add(new Backpack() { CharacterId = characterId, ItemId = itemId , Amount = 1});
                }
                else
                {
                    character.Backpacks.Where(e => e.ItemId == itemId).FirstOrDefault().Amount++;
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}