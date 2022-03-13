using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  [Route("api/")]
  [ApiController]
  public class UpdateController : Controller
  {
    private readonly IGamesService _gamesService;
    private readonly IClipsService _clipsService;

    public UpdateController(IGamesService gamesService, IClipsService clipsService)
    {
      _gamesService = gamesService;
      _clipsService = clipsService;
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update()
    {
      var gamesList = await _gamesService.GetGamesFromTwitchAsync();
      await _gamesService.AddGamesToDatabaseAsync(gamesList);

      var clipsList = await _clipsService.GetClipsFromTwitchAsync();
      await _clipsService.AddClipsToDatabaseAsync(clipsList);

      return Ok();
    }
  }
}