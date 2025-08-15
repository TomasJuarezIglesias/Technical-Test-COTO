using Application.Dtos;
using Domain.Entities;

namespace Application.Tests.ReservaTests
{
    public static class ReservaServiceTestData
    {
        public static ReservaCreateDto NewDto(DateTime fecha, string rango, int salonId = 1, int clienteId = 1)
        {
            var parts = rango.Split('-');
            return new ReservaCreateDto
            {
                Fecha = fecha,
                HoraInicio = TimeSpan.Parse(parts[0]),
                HoraFin = TimeSpan.Parse(parts[1]),
                SalonId = salonId,
                ClienteId = clienteId
            };
        }

        public static Reserva NewReserva(DateTime fecha, string rango, int salonId = 1, int clienteId = 1, int id = 0)
        {
            var parts = rango.Split('-');
            return new Reserva
            {
                Id = id,
                Fecha = fecha.Date,
                HoraInicio = TimeSpan.Parse(parts[0]),
                HoraFin = TimeSpan.Parse(parts[1]),
                SalonId = salonId,
                ClienteId = clienteId
            };
        }
    }
}
