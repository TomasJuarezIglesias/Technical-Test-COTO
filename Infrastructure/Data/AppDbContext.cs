using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    #region Entities
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Salon> Salones => Set<Salon>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}