using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.ClienteTests
{
    public class ClienteServiceTests
    {
        [Fact]
        public async Task GetAll_Deberia_Devolver_Todos_Los_Clientes()
        {
            var seed = new[]
            {
                ClienteServiceTestData.NewCliente(1, "Cliente 1", "demo1@ejemplo.com"),
                ClienteServiceTestData.NewCliente(2, "Cliente 2", "demo2@ejemplo.com")
            };

            var service = ClienteServiceBuilder.Build(out var clienteRepo, out _, seed);

            var result = await service.GetAll();

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count());

            clienteRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
