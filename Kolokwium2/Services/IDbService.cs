using Kolokwium2.DTOs;

namespace Kolokwium2.Services;

public interface IDbService
{
    Task<CharacterDto?> GetCharacterById(int id);
    
    Task AddItem(int characterId, AddItemDto item);
}