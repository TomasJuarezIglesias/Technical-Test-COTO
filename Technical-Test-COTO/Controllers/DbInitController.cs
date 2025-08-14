using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Technical_Test_COTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DbInitController(AppDbContext _dbContext, ILogger<DbInitController> _logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> InitializeDatabase()
        {
            await AppDbInitializer.MigrateAndSeedAsync(_dbContext, _logger);
            return Ok(new { Message = "Base de datos inicializada" });
        }
    }
}
