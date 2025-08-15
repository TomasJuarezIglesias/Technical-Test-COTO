using Domain.Entities;

namespace Application.Tests.ClienteTests
{
    public static class ClienteServiceTestData
    {
        public static Cliente NewCliente(int id, string nombre, string email) => 
            new()
            {
                Id = id,
                Nombre = nombre,
                Email = email
            };
    }
}
