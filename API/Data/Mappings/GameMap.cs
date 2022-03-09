using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings
{
  public class GameMap : IEntityTypeConfiguration<Game>
  {
    public void Configure(EntityTypeBuilder<Game> builder)
    {
      // Tabela
      builder.ToTable("Game");

      // Chave primária
      builder.HasKey(x => x.Id);

      // Propriedades
      builder.Property(x => x.Id)
       .HasColumnName("Id")
       .HasColumnType("INTEGER")
       .HasMaxLength(32);

      builder.Property(x => x.Name)
        .HasColumnName("Name")
        .HasColumnType("TEXT")
        .HasMaxLength(120);

      builder.Property(x => x.BoxArtUrl)
        .HasColumnName("BoxArtUrl")
        .HasColumnType("TEXT")
        .HasMaxLength(120);
    }
  }
}