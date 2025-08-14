using Domain.Entities;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbInitializer(AppDbContext _dbContext) : IAppDbInitializer
{
    public async Task MigrateAndSeedAsync()
    {
        await _dbContext.Database.MigrateAsync();

        if (!await _dbContext.Salones.AnyAsync())
        {
            _dbContext.Salones.AddRange(
                new Salon { Nombre = "Salón 1", Ubicacion = "Sede 1", Capacidad = 150 },
                new Salon { Nombre = "Salón 2", Ubicacion = "Sede 2", Capacidad = 80 },
                new Salon { Nombre = "Salón 3", Ubicacion = "Sede 3", Capacidad = 120 }
            );
        }

        if (!await _dbContext.Clientes.AnyAsync())
        {
            _dbContext.Clientes.AddRange(
                new Cliente { Nombre = "Cliente 1", Email = "demo1@ejemplo.com" },
                new Cliente { Nombre = "Cliente 2", Email = "demo2@ejemplo.com" },
                new Cliente { Nombre = "Cliente 3", Email = "demo3@ejemplo.com" }
            );
        }

        await _dbContext.SaveChangesAsync();
    }
}
