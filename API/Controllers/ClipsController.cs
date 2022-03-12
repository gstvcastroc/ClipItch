using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  [Route("api/")]
  [ApiController]
  public class ClipsController : Controller
  {
    private readonly IClipsService _clipsService;

    public ClipsController(IClipsService clipsService)
    {
      _clipsService = clipsService;
    }

    [HttpGet("clips")]
    public async Task<IActionResult> GetClipsRequest()
    {
      return Ok();
    }
  }
}