namespace Kolokwium2.DTOs;

public class CharacterDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public IList<ItemDto> BackpackItems { get; set; }
    public IList<TitleDto> Titles { get; set; }
}

public class ItemDto
{
    public string ItemName { get; set; }
    public int ItemWeight { get; set; }
    public int Amount { get; set; }
}

public class TitleDto
{
    public string Title { get; set; }
    public DateTime AcquiredAt { get; set; }
}