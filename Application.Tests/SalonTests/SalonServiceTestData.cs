using Domain.Entities;

namespace Application.Tests.SalonTests
{
    public static class SalonServiceTestData
    {
        public static Salon NewSalon(int id, string nombre, string ubicacion, int capacidad) =>
            new()
            {
                Id = id,
                Nombre = nombre,
                Ubicacion = ubicacion,
                Capacidad = capacidad
            };
    }
}
