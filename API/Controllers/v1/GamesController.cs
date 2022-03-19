using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.v1
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  //[Produces("application/json")] Ao habilitar essa anotação, o Swagger UI otimiza o JSON e remove a formatação.
  public class GamesController : ControllerBase
  {
    private readonly IGamesService _gamesService;

    public GamesController(IGamesService gamesService)
    {
      _gamesService = gamesService;
    }

    /// <summary>
    /// Busca a lista completa de jogos.
    /// </summary>
    /// <returns>JSON com todos os jogos.</returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     GET api/v1/games
    ///     
    /// </remarks>
    /// <response code="200">JSON retornado com sucesso.</response>
    /// <response code="400">Erro no cliente.</response>
    /// <response code="404">Lista de jogos vazia.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTopGamesRequest()
    {
      try
      {
        var gamesList = await _gamesService.GetGamesAsync();

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