using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Models;

[Table("Backpack")]
[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class Backpack
{
    [ForeignKey(nameof(Character))]
    public int CharacterId { get; set; }
    
    [ForeignKey(nameof(Item))]
    public int ItemId { get; set; }
    
    public int Amount { get; set; }
    
    public Character Character { get; set; }
    public Item Item { get; set; }
}