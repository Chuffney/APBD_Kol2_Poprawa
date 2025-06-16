using Kolokwium2.DTOs;
using Microsoft.AspNetCore.Mvc;

using Kolokwium2.Services;

namespace Kolokwium2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCharacter(int id)
    {
        var character = await _dbService.GetCharacterById(id);
        
        if (character is null)
            return NotFound("Character not found");
        
        return Ok(character);
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddItemToBackpack(int characterId, AddItemDto dto)
    {
        try
        {
            await _dbService.AddItem(characterId, dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}