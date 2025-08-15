using Application.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.IRepository;
using Moq;
using System.Linq.Expressions;

namespace Application.Tests.ReservaTests
{
    public static class ReservaServiceBuilder
    {
        public static ReservaService Build(
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

            var data = seedReservas?.ToList() ?? new List<Reserva>();

            salonRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Salon, bool>>>()))
                     .ReturnsAsync(true);

            clienteRepo.Setup(r => r.ExistsAsync(It.IsAny<Expression<Func<Cliente, bool>>>()))
                       .ReturnsAsync(true);

            reservaRepo.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Reserva, bool>>>(), It.IsAny<Expression<Func<Reserva, object>>[]>()))
                .ReturnsAsync((Expression<Func<Reserva, bool>> pred,
                               Expression<Func<Reserva, object>>[] _) => data.Where(pred.Compile()).ToList());

            reservaRepo.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Reserva, bool>>>()))
                .ReturnsAsync((Expression<Func<Reserva, bool>> pred) => data.Where(pred.Compile()).ToList());


            reservaRepo.Setup(r => r.AddAsync(It.IsAny<Reserva>()))
                .ReturnsAsync((Reserva e) =>
                {
                    if (e.Id == 0) e.Id = 123;
                    data.Add(e);
                    return e;
                });

            mapper.Setup(m => m.Map<Reserva>(It.IsAny<ReservaCreateDto>()))
                  .Returns((ReservaCreateDto dto) => new Reserva
                  {
                      Fecha = dto.Fecha.Date,
                      HoraInicio = dto.HoraInicio,
                      HoraFin = dto.HoraFin,
                      SalonId = dto.SalonId,
                      ClienteId = dto.ClienteId
                  });

            mapper.Setup(m => m.Map<ReservaDto>(It.IsAny<Reserva>()))
                  .Returns((Reserva r) => new ReservaDto
                  {
                      Id = r.Id,
                      Fecha = r.Fecha,
                      HoraInicio = r.HoraInicio,
                      HoraFin = r.HoraFin
                  });

            mapper.Setup(m => m.Map<IEnumerable<ReservaDto>>(It.IsAny<IEnumerable<Reserva>>()))
                  .Returns((IEnumerable<Reserva> list) =>
                      list.Select(r => new ReservaDto
                      {
                          Id = r.Id,
                          Fecha = r.Fecha,
                          HoraInicio = r.HoraInicio,
                          HoraFin = r.HoraFin
                      }).ToList());

            return new ReservaService(reservaRepo.Object, salonRepo.Object, clienteRepo.Object, mapper.Object);
        }
    }
}
