using API.Data.Mappings;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Data
{
  public class DataContext : DbContext
  {
    public DbSet<Clip> Clips { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Broadcaster> Broadcasters { get; set; }
    public DbSet<Curator> Curators { get; set; }
    public DbSet<Thumbnail> Thumbnails { get; set; }
    public DbSet<Vod> Vods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=Clipitch.db")
      
      .LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ClipMap());
      modelBuilder.ApplyConfiguration(new GameMap());
      modelBuilder.ApplyConfiguration(new BroadcasterMap());
      modelBuilder.ApplyConfiguration(new CuratorMap());
      modelBuilder.ApplyConfiguration(new ThumbnailMap());
      modelBuilder.ApplyConfiguration(new VodMap());
    }
  }
}
