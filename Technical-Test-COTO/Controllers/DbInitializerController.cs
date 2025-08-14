using Domain.Entities;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Technical_Test_COTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DbInitializerController(IAppDbInitializer _appDbInitializer) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> InitializeDatabase()
        {
            await _appDbInitializer.MigrateAndSeedAsync();

            var response = ApiResponse<object>.SuccessResponse(null, "Base de datos inicializada correctamente");
            return Ok(response);
        }
    }
}
