using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class AppDbInitializer
{
    public static async Task MigrateAndSeedAsync(AppDbContext db, ILogger logger)
    {
        await db.Database.MigrateAsync();

        if (!await db.Salones.AnyAsync())
        {
            db.Salones.AddRange(
                new Salon { Nombre = "Salón 1", Ubicacion = "Sede 1", Capacidad = 150 },
                new Salon { Nombre = "Salón 2", Ubicacion = "Sede 2", Capacidad = 80 },
                new Salon { Nombre = "Salón 3", Ubicacion = "Sede 3", Capacidad = 120 }
            );
        }

        if (!await db.Clientes.AnyAsync())
        {
            db.Clientes.AddRange(
                new Cliente { Nombre = "Cliente 1", Email = "demo1@ejemplo.com" },
                new Cliente { Nombre = "Cliente 2", Email = "demo2@ejemplo.com" },
                new Cliente { Nombre = "Cliente 3", Email = "demo3@ejemplo.com" }
            );
        }

        await db.SaveChangesAsync();
    }
}
