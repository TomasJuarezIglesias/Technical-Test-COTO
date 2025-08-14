using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Technical_Test_COTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController(IReservaService _reservaService) : ControllerBase
    {

        [HttpGet("{fecha:datetime}")]
        public async Task<IActionResult> GetByDate([FromRoute] DateTime fecha)
        {
            var response = await _reservaService.GetByDate(fecha);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservaCreateDto reserva)
        {
            var response = await _reservaService.Create(reserva);
            return Ok(response);
        }

    }
}
