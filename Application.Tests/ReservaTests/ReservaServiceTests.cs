using System.Linq.Expressions;
using Domain.Entities;
using Domain.Exceptions;
using Moq;

namespace Application.Tests.ReservaTests
{
    public class ReservaServiceTests
    {
        [Fact]
        public async Task GetByDate_Deberia_Retornar_Solo_La_Fecha_Solicitada()
        {
            var fecha = new DateTime(2025, 8, 14);
            var seed = new[]
            {
                ReservaServiceTestData.NewReserva(fecha, "10:00-11:00"),
                ReservaServiceTestData.NewReserva(fecha.AddDays(1), "12:00-13:00")
            };

            var service = ReservaServiceBuilder.Build(out var reservaRepo, out _, out _, out _, seed);

            var res = await service.GetByDate(fecha);

            Assert.True(res.Success);
            Assert.Single(res.Data);
            Assert.Equal(fecha.Date, res.Data.First().Fecha.Date);

            reservaRepo.Verify(r => r.FindAsync(It.IsAny<Expression<Func<Reserva, bool>>>(), It.IsAny<Expression<Func<Reserva, object>>[]>()), Times.Once);
        }

        [Fact]
        public async Task Create_Deberia_Fallar_Si_Salon_No_Existe()
        {
            var service = ReservaServiceBuilder.Build(out _, out var salonRepo, out _, out _);
            salonRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Salon, bool>>>())).ReturnsAsync(false);

            var dto = ReservaServiceTestData.NewDto(DateTime.Today, "10:00-11:00");

            await Assert.ThrowsAsync<BusinessException>(() => service.Create(dto));
        }

        [Fact]
        public async Task Create_Deberia_Fallar_Si_Cliente_No_Existe()
        {
            var service = ReservaServiceBuilder.Build(out _, out _, out var clienteRepo, out _);
            clienteRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Cliente, bool>>>())).ReturnsAsync(false);

            var dto = ReservaServiceTestData.NewDto(DateTime.Today, "10:00-11:00");

            await Assert.ThrowsAsync<BusinessException>(() => service.Create(dto));
        }

        [Fact]
        public async Task Create_Deberia_Fallar_Si_HoraFin_No_Mayor_A_HoraInicio()
        {
            var service = ReservaServiceBuilder.Build(out _, out _, out _, out _);

            var dto = ReservaServiceTestData.NewDto(DateTime.Today, "10:00-10:00");

            var ex = await Assert.ThrowsAsync<BusinessException>(() => service.Create(dto));
            Assert.Contains("Hora fin debe ser mayor", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Create_Deberia_Fallar_Si_Hay_Solapamiento()
        {
            var fecha = new DateTime(2025, 8, 14);
            var seed = new[] { ReservaServiceTestData.NewReserva(fecha, "10:30-11:30") };

            var service = ReservaServiceBuilder.Build(out _, out _, out _, out _, seed);
            var dto = ReservaServiceTestData.NewDto(fecha, "10:00-10:15");

            var ex = await Assert.ThrowsAsync<BusinessException>(() => service.Create(dto));
            Assert.Contains("Conflicto de horario", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task Create_Deberia_Permitir_Gap_Exacto_De_30_Minutos()
        {
            var fecha = new DateTime(2025, 8, 14);
            var seed = new[] { ReservaServiceTestData.NewReserva(fecha, "10:30-11:30") };

            var service = ReservaServiceBuilder.Build(out var reservaRepo, out _, out _, out _, seed);
            var dto = ReservaServiceTestData.NewDto(fecha, "09:00-10:00");

            var res = await service.Create(dto);

            Assert.True(res.Success);
            Assert.NotNull(res.Data);
            Assert.Equal(123, res.Data.Id);

            reservaRepo.Verify(r => r.AddAsync(It.IsAny<Reserva>()), Times.Once);
        }
    }
}