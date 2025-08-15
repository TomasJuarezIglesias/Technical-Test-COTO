using Application.Services;
using AutoMapper;
using Domain.IRepository;
using Domain.Entities;
using Moq;

namespace Application.Tests.ClienteTests
{
    public static class ClienteServiceBuilder
    {
        public static ClienteService Build(
            out Mock<IRepository<Cliente>> clienteRepo,
            out Mock<IMapper> mapper,
            IEnumerable<Cliente>? seed = null)
        {
            clienteRepo = new Mock<IRepository<Cliente>>();
            mapper = new Mock<IMapper>();

            var data = seed?.ToList() ?? new();

            clienteRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(data);

            mapper.Setup(m => m.Map<IEnumerable<Dtos.ClienteDto>>(It.IsAny<IEnumerable<Cliente>>()))
                .Returns((IEnumerable<Cliente> c) => c.Select(x => new Dtos.ClienteDto
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Email = x.Email
                }));

            return new ClienteService(clienteRepo.Object, mapper.Object);
        }
    }
}
