using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings
{
  internal class ClipMap : IEntityTypeConfiguration<Clip>
  {
    public void Configure(EntityTypeBuilder<Clip> builder)
    {
      // Tabela
      builder.ToTable("Clip");

      // Chave primária
      builder.HasKey(x => x.Id);

      // Propriedades
      builder.Property(x => x.Id)
       .HasColumnName("Id")
       .HasColumnType("INTEGER")
       .HasMaxLength(32);

      builder.Property(x => x.EmbedUrl)
        .HasColumnName("EmbedUrl")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.EmbedHtml)
        .HasColumnName("EmbedHtml")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.Game)
        .HasColumnName("Game")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.Language)
        .HasColumnName("Language")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.Views)
        .HasColumnName("Views")
        .HasColumnType("INTEGER")
        .HasMaxLength(32);

      builder.Property(x => x.Duration)
        .HasColumnName("Duration")
        .HasColumnType("REAL")
        .HasMaxLength(32);

      builder.Property(x => x.CreatedAt)
        .HasColumnName("Created_at")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      // Relacionamentos
      builder
        .HasOne(x => x.Broadcaster)
        .WithMany(x => x.Clips)
        .HasForeignKey(x => x.BroadcasterId);

      builder
        .HasOne(x => x.Curator)
        .WithMany(x => x.Clips)
        .HasForeignKey(x => x.CuratorId);

      builder
        .HasOne(x => x.Vod)
        .WithOne(x => x.Clip)
        .HasForeignKey<Vod>(x => x.Id);

      builder
        .HasOne(x => x.Thumbnail)
        .WithOne(x => x.Clip)
        .HasForeignKey<Thumbnail>(x => x.Id);
    }
  }
}