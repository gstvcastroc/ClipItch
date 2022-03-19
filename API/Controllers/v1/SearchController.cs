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
  public class SearchController : Controller
  {
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
      _searchService = searchService;
    }

    /// <summary>
    /// Busca a lista de clips com os termos inseridos pelo usuário.
    /// A lista é ordenada de acordo com o número de visualizações.
    /// </summary>
    /// <param name="input">Termos de busca</param>
    /// <returns>JSON com todos os clips relativos à busca realizada.</returns>
    /// /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST api/v1/clips/valorant
    ///     
    /// </remarks>
    /// <response code="200">JSON retornado com sucesso.</response>
    /// <response code="400">Erro no cliente.</response>
    /// <response code="404">Lista de clips vazia.</response>
    [HttpPost("clips/{input}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchClips([FromRoute] string input)
    {
      try
      {
        var clipsList = await _searchService.SearchClipsAsync(input);

        return Ok(clipsList);
      }

      catch (Exception)
      {
        return BadRequest("Nenhum clip encontrado.");
      }
    }
  }
}