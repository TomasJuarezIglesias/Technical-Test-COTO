using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ClienteConfig : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> b)
    {
        b.ToTable("Clientes");
        b.HasKey(x => x.Id);

        b.Property(x => x.Nombre)
            .HasMaxLength(200)
            .IsRequired();

        b.Property(x => x.Email)
            .HasMaxLength(256)
            .IsRequired();

        b.HasIndex(x => x.Email).IsUnique();
    }
}
