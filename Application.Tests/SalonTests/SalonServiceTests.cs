using Moq;

namespace Application.Tests.SalonTests
{
    public class SalonServiceTests
    {
        [Fact]
        public async Task GetAll_Deberia_Devolver_Todos_Los_Salones()
        {
            var seed = new[]
            {
                SalonServiceTestData.NewSalon(1, "Salón 1", "Sede 1", 120),
                SalonServiceTestData.NewSalon(2, "Salón 2", "Sede 2", 180)
            };

            var service = SalonServiceBuilder.Build(out var salonRepo, out _, seed);

            var result = await service.GetAll();

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count());

            salonRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
