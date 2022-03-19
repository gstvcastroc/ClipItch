using API.Data.Mappings;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Clip> Clips { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=Clipitch.db")
      
      .LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.UseCollation("NOCASE");
      modelBuilder.ApplyConfiguration(new ClipMap());
      modelBuilder.ApplyConfiguration(new GameMap());
    }
  }
}
