using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Technical_Test_COTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DbInitializerController(AppDbContext _dbContext, ILogger<DbInitializerController> _logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> InitializeDatabase()
        {
            await AppDbInitializer.MigrateAndSeedAsync(_dbContext, _logger);

            var response = new ApiResponse<object>( success: true, data: null, message: "Base de datos inicializada correctamente");
            return Ok(response);
        }
    }
}
