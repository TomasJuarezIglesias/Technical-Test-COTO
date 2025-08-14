using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class SalonConfig : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> b)
    {
        b.ToTable("Salones");
        b.HasKey(x => x.Id);

        b.Property(x => x.Nombre)
            .HasMaxLength(200)
            .IsRequired();

        b.Property(x => x.Ubicacion)
            .HasMaxLength(300)
            .IsRequired();

        b.Property(x => x.Capacidad)
            .IsRequired();
    }
}
