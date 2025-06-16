using Kolokwium2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>().HasData(new List<Character>()
        {
            new Character() {CharacterId = 1, FirstName = "name1", LastName = "lastname1", CurrentWeight = 100, MaxWeight = 200},
            new Character() {CharacterId = 2, FirstName = "name2", LastName = "lastname2", CurrentWeight = 200, MaxWeight = 400},
            new Character() {CharacterId = 3, FirstName = "name3", LastName = "lastname3", CurrentWeight = 300, MaxWeight = 600},
        });
        
        modelBuilder.Entity<Title>().HasData(new List<Title>()
        {
            new Title() { TitleId = 1, Name = "title1"},
            new Title() { TitleId = 2, Name = "title2"},
            new Title() { TitleId = 3, Name = "title3"},
        });
        
        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new Item() {ItemId = 1, Name = "item1", Weight = 10},
            new Item() {ItemId = 2, Name = "item2", Weight = 20},
            new Item() {ItemId = 3, Name = "item3", Weight = 30},
        });
        
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>()
        {
            new Backpack() { CharacterId = 1, ItemId = 1, Amount = 1},
            new Backpack() { CharacterId = 2, ItemId = 2, Amount = 2},
            new Backpack() { CharacterId = 3, ItemId = 3, Amount = 3},
        });
        
        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>()
        {
            new CharacterTitle() {CharacterId = 1, TitleId = 1},
            new CharacterTitle() {CharacterId = 3, TitleId = 3},
            new CharacterTitle() {CharacterId = 3, TitleId = 2},
            new CharacterTitle() {CharacterId = 2, TitleId = 1},
        });
    }
}