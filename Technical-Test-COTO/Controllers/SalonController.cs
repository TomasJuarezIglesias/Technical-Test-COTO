using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Technical_Test_COTO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalonController(ISalonService _salonService): ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _salonService.GetAll();
            return Ok(response);
        }
    }
}
