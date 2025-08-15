using Application.Services;
using AutoMapper;
using Domain.IRepository;
using Moq;
using Domain.Entities;

namespace Application.Tests.SalonTests
{
    public static class SalonServiceBuilder
    {
        public static SalonService Build(
            out Mock<IRepository<Salon>> salonRepo,
            out Mock<IMapper> mapper,
            IEnumerable<Salon>? seed = null)
        {
            salonRepo = new Mock<IRepository<Salon>>();
            mapper = new Mock<IMapper>();

            var data = seed?.ToList() ?? new();

            salonRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(data);

            mapper.Setup(m => m.Map<IEnumerable<Dtos.SalonDto>>(It.IsAny<IEnumerable<Salon>>()))
                .Returns((IEnumerable<Salon> s) => s.Select(x => new Dtos.SalonDto
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Ubicacion = x.Ubicacion,
                    Capacidad = x.Capacidad
                }));

            return new SalonService(salonRepo.Object, mapper.Object);
        }
    }
}
