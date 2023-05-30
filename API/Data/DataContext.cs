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
            => options.UseNpgsql(@"server=db-clipitch.cuqu75ttemrd.us-east-1.rds.amazonaws.com;Port=5432;user id=main;password=admin123;database=postgres")
      
      .LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ClipMap());
      modelBuilder.ApplyConfiguration(new GameMap());
    }
  }
}
