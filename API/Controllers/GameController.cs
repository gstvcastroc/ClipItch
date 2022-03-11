using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
  [Route("api/")]
  [ApiController]
  public class GameController : ControllerBase
  {
    private readonly IGamesService _gamesService;

    public GameController(IGamesService gamesService)
    {
      _gamesService = gamesService;
    }

    [HttpGet("topgames")]
    public async Task<IActionResult> GetTopGamesRequest()
    {
      var gamesList = await _gamesService.GetTopGamesAsync();

      return Ok(gamesList);
    }
  }
}