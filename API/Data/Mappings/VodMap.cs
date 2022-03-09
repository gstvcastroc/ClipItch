using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings
{
  internal class VodMap : IEntityTypeConfiguration<Vod>
  {
    public void Configure(EntityTypeBuilder<Vod> builder)
    {
      // Tabela
      builder.ToTable("Vod");

      // Chave primária
      builder.HasKey(x => x.Id);

      // Propriedades
      builder.Property(x => x.Id)
       .HasColumnName("Id")
       .HasColumnType("INTEGER")
       .HasMaxLength(32);

      builder.Property(x => x.Url)
        .HasColumnName("Url")
        .HasColumnType("TEXT")
        .HasMaxLength(120);
    }
  }
}