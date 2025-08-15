using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Technical_Test_COTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController(IClienteService _clienteService): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _clienteService.GetAll();
            return Ok(response);
        }
    }
}
