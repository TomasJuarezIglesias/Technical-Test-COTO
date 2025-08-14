using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ReservaConfig : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> b)
    {
        b.ToTable("Reservas");
        b.HasKey(x => x.Id);

        b.Property(x => x.Fecha).IsRequired();

        b.Property(x => x.HoraInicio).HasColumnType("time(0)").IsRequired();
        b.Property(x => x.HoraFin).HasColumnType("time(0)").IsRequired();

        // Relaciones
        b.HasOne(x => x.Cliente)
         .WithMany(c => c.Reservas)
         .HasForeignKey(x => x.ClienteId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.Salon)
         .WithMany(s => s.Reservas)
         .HasForeignKey(x => x.SalonId)
         .OnDelete(DeleteBehavior.Restrict);

        b.ToTable(t => t.HasCheckConstraint("CK_Reservas_RangoHora", "[HoraInicio] < [HoraFin]"));
    }
}
