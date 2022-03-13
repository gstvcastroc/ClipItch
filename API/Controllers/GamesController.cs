using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
  [Route("api/")]
  [ApiController]
  public class GamesController : ControllerBase
  {
    private readonly IGamesService _gamesService;

    public GamesController(IGamesService gamesService)
    {
      _gamesService = gamesService;
    }

    [HttpGet("topgames")]
    public async Task<IActionResult> GetTopGamesRequest()
    {
      try
      {
        var gamesList = await _gamesService.GetGamesFromDatabaseAsync();

        if (gamesList is null) return NotFound("Lista de jogos vazia.");

        return Ok(gamesList);
      }

      catch (Exception)
      {
        return BadRequest("Erro na requisição.");
      }
    }
  }
}