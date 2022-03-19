using API.Interfaces;
using API.Workers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.v1
{
  [ApiController]
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  //[Produces("application/json")] Ao habilitar essa anotação, o Swagger UI otimiza o JSON e remove a formatação.
  public class UpdateController : Controller
  {
    private readonly IGamesService _gamesService;
    private readonly IClipsService _clipsService;

    public UpdateController(IGamesService gamesService, IClipsService clipsService)
    {
      _gamesService = gamesService;
      _clipsService = clipsService;
    }

    /// <summary>
    /// Dispara os métodos para buscar jogos e clips na API do Twitch, além de salvar os resultados no banco de dados.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     GET api/v1/update
    ///     
    /// </remarks>
    /// <response code="200">Operações realizadas com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update()
    {
      await new GameWorker(_gamesService).Invoke();

      await new ClipsWorker(_clipsService).Invoke();

      return Ok();
    }
  }
}