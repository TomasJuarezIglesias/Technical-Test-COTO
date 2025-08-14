using Application.Services;
using Domain.Entities;
using Domain.IRepository;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Technical_Test_COTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController(ReservaService reservaService) : ControllerBase
    {

        [HttpGet("{fecha:datetime}")]
        public async Task<IActionResult> GetByDate([FromRoute] DateTime fecha)
        {
            var response = await reservaService.GetByDate(fecha);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var response = new ApiResponse<object>(success: true, data: null, message: "Base de datos inicializada correctamente");
            return Ok(response);
        }

    }
}
