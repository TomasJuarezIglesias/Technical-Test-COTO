using System.Linq.Expressions;
using Application.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.IRepository;
using Moq;

namespace Application.Tests
{
    public class ReservaServiceTests
    {
        private static ReservaService BuildService(
            out Mock<IRepository<Reserva>> reservaRepo,
            out Mock<IRepository<Salon>> salonRepo,
            out Mock<IRepository<Cliente>> clienteRepo,
            out Mock<IMapper> mapper,
            IEnumerable<Reserva>? seedReservas = null)
        {
            reservaRepo = new Mock<IRepository<Reserva>>();
            salonRepo = new Mock<IRepository<Salon>>();
            clienteRepo = new Mock<IRepository<Cliente>>();
            mapper = new Mock<IMapper>();

            salonRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Salon, bool>>>()))
                     .ReturnsAsync(true);

            clienteRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Cliente, bool>>>()))
                       .ReturnsAsync(true);

            var data = seedReservas?.ToList() ?? new List<Reserva>();

            reservaRepo.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Reserva, bool>>>()))
                       .ReturnsAsync((Expression<Func<Reserva, bool>> pred) =>
                           data.Where(pred.Compile()).ToList());

            reservaRepo.Setup(r => r.AddAsync(It.IsAny<Reserva>()))
                       .ReturnsAsync((Reserva e) =>
                       {
                           if (e.Id == 0) e.Id = 123;
                           data.Add(e);
                           return e;
                       });

            mapper.Setup(m => m.Map<Reserva>(It.IsAny<ReservaCreateDto>()))
                  .Returns((ReservaCreateDto s) => new Reserva
                  {
                      Fecha = s.Fecha.Date,
                      HoraInicio = s.HoraInicio,
                      HoraFin = s.HoraFin,
                      SalonId = s.SalonId,
                      ClienteId = s.ClienteId
                  });

            mapper.Setup(m => m.Map<ReservaDto>(It.IsAny<Reserva>()))
                  .Returns((Reserva r) => new ReservaDto
                  {
                      Id = r.Id,
                      Fecha = r.Fecha,
                      HoraInicio = r.HoraInicio,
                      HoraFin = r.HoraFin,
                      Salon = null,
                      Cliente = null
                  });

            mapper.Setup(m => m.Map<IEnumerable<ReservaDto>>(It.IsAny<IEnumerable<Reserva>>()))
                  .Returns((IEnumerable<Reserva> list) => list.Select(r => new ReservaDto
                  {
                      Id = r.Id,
                      Fecha = r.Fecha,
                      HoraInicio = r.HoraInicio,
                      HoraFin = r.HoraFin
                  }).ToList());

            return new ReservaService(reservaRepo.Object, salonRepo.Object, clienteRepo.Object, mapper.Object);
        }

        private static ReservaCreateDto NewDto(DateTime fecha, string rango, int salonId = 1, int clienteId = 1)
        {
            var parts = rango.Split('-');
            var hi = TimeSpan.Parse(parts[0]);
            var hf = TimeSpan.Parse(parts[1]);

            return new ReservaCreateDto
            {
                Fecha = fecha,
                HoraInicio = hi,
                HoraFin = hf,
                SalonId = salonId,
                ClienteId = clienteId
            };
        }

        private static Reserva NewReserva(DateTime fecha, string rango, int salonId = 1, int clienteId = 1, int id = 0)
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


        [Fact]
        public async Task GetByDate_Deberia_Retornar_Solo_La_Fecha_Solicitada()
        {
            var fecha = new DateTime(2025, 8, 14, 15, 0, 0);
            var seed = new[]
            {
                NewReserva(fecha, "10:00-11:00", salonId: 1),
                NewReserva(fecha.AddDays(1),"12:00-13:00", salonId: 1)
            };

            var service = BuildService(out var reservaRepo, out _, out _, out _, seed);

            var res = await service.GetByDate(fecha);

            Assert.True(res.Success);
            Assert.Single(res.Data);
            Assert.Equal(fecha.Date, res.Data.First().Fecha.Date);

            reservaRepo.Verify(r => r.FindAsync(It.IsAny<Expression<Func<Reserva, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task Create_Deberia_Fallar_Si_Salon_No_Existe()
        {
            var service = BuildService(out _, out var salonRepo, out _, out _);
            salonRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Salon, bool>>>())).ReturnsAsync(false);

            var dto = NewDto(DateTime.Today, "10:00-11:00");

            await Assert.ThrowsAsync<BusinessException>(() => service.Create(dto));
        }

        [Fact]
        public async Task Create_Deberia_Fallar_Si_Cliente_No_Existe()
        {
            var service = BuildService(out _, out _, out var clienteRepo, out _);
            clienteRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Cliente, bool>>>())).ReturnsAsync(false);

            var dto = NewDto(DateTime.Today, "10:00-11:00");

            await Assert.ThrowsAsync<BusinessException>(() => service.Create(dto));
        }

        [Fact]
        public async Task Create_Deberia_Fallar_Si_HoraFin_No_Mayor_A_HoraInicio()
        {
            var service = BuildService(out _, out _, out _, out _);

            var dto = NewDto(DateTime.Today, "10:00-10:00");

            var ex = await Assert.ThrowsAsync<BusinessException>(() => service.Create(dto));
            Assert.Contains("Hora fin debe ser mayor", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Create_Deberia_Fallar_Si_Hay_Solapamiento()
        {
            var fecha = new DateTime(2025, 8, 14);
            var seed = new[] { NewReserva(fecha, "10:30-11:30", salonId: 1) };

            var service = BuildService(out _, out _, out _, out _, seed);
            var dto = NewDto(fecha, "10:00-10:15", salonId: 1);

            var ex = await Assert.ThrowsAsync<BusinessException>(() => service.Create(dto));
            Assert.Contains("Conflicto de horario", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Create_Deberia_Permitir_Gap_Exacto_De_30_Minutos()
        {
            var fecha = new DateTime(2025, 8, 14);
            var seed = new[] { NewReserva(fecha, "10:30-11:30", salonId: 1) };

            var service = BuildService(out var reservaRepo, out _, out _, out _, seed);
            var dto = NewDto(fecha, "09:00-10:00", salonId: 1);

            var res = await service.Create(dto);

            Assert.True(res.Success);
            Assert.NotNull(res.Data);
            Assert.Equal(123, res.Data.Id);

            reservaRepo.Verify(r => r.AddAsync(It.IsAny<Reserva>()), Times.Once);
        }
    }
}