using Microsoft.AspNetCore.Mvc;

namespace ClipItch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {        
        [HttpGet("inicio")]
        public IActionResult Index()
        {            
            return Ok("Teste");
        }
    }
}