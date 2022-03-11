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
       .HasColumnName("id")
       .HasColumnType("TEXT")
       .HasMaxLength(120);

      builder.Property(x => x.Url)
      .HasColumnName("url")
      .HasColumnType("TEXT")
      .HasMaxLength(120);

      builder.Property(x => x.EmbedUrl)
        .HasColumnName("embed_url")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.BroadcasterId)
        .HasColumnName("broadcaster_id")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.BroadcasterName)
        .HasColumnName("broadcaster_name")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.CreatorId)
        .HasColumnName("creator_id")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.CreatorName)
        .HasColumnName("creator_name")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.VideoId)
        .HasColumnName("video_id")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.GameId)
        .HasColumnName("game_id")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.Language)
        .HasColumnName("language")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.Title)
        .HasColumnName("title")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.ViewCount)
        .HasColumnName("view_count")
        .HasColumnType("INTEGER")
        .HasMaxLength(120);

      builder.Property(x => x.CreatedAt)
        .HasColumnName("created_at")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.ThumbnailUrl)
        .HasColumnName("thumbnail_url")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.Duration)
        .HasColumnName("duration")
        .HasColumnType("REAL")
        .HasMaxLength(120);
    }
  }
}