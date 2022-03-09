using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings
{
  public class ThumbnailMap : IEntityTypeConfiguration<Thumbnail>
  {
    public void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
      // Tabela
      builder.ToTable("Thumbnail");

      // Chave primária
      builder.HasKey(x => x.Id);

      // Adição de ID no momento de inserção no banco de dados.
      builder.Property(x => x.Id)
        .ValueGeneratedOnAdd();

      // Propriedades
      builder.Property(x => x.Tiny)
        .HasColumnName("Tiny")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.Small)
        .HasColumnName("Small")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.Medium)
        .HasColumnName("Medium")
        .HasColumnType("TEXT")
        .HasMaxLength(120);
    }
  }
}