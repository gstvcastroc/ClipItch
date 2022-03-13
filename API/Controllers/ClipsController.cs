using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
    public async Task<IActionResult> GetAllClipsRequest()
    {
      try
      {
        var clipsList = await _clipsService.GetClipsFromDatabaseAsync(null);

        if (clipsList is null) return NotFound("Lista de clips vazia.");

        return Ok(clipsList);
      }

      catch (Exception)
      {
        return BadRequest("Erro na requisição.");
      }
    }

    [HttpGet("clips/{quantity:int}")]
    public async Task<IActionResult> GetClipsRequest([FromRoute] int quantity)
    {
      try
      {
        var clipsList = await _clipsService.GetClipsFromDatabaseAsync(quantity);

        if (clipsList is null) return NotFound("Lista de clips vazia.");

        return Ok(clipsList);
      }

      catch (Exception)
      {
        return BadRequest("Erro na requisição.");
      }
    }

    [HttpGet("clips/game/{id}")]
    public async Task<IActionResult> GetAllClipsByGameIdRequest ([FromRoute] string id)
    {
      try
      {
        var clipsList = await _clipsService.GetClipsFromDatabaseByGameIdAsync(id, null);

        if (clipsList is null) return NotFound("Lista de clips vazia.");

        return Ok(clipsList);
      }

      catch (Exception)
      {
        return BadRequest("Erro na requisição.");
      }
    }

    [HttpGet("clips/game/{id}/{quantity:int}")]
    public async Task<IActionResult> GetClipsByGameIdRequest([FromRoute] string id, int quantity)
    {
      try
      {
        var clipsList = await _clipsService.GetClipsFromDatabaseByGameIdAsync(id, quantity);

        if (clipsList is null) return NotFound("Lista de clips vazia.");

        return Ok(clipsList);
      }

      catch (Exception)
      {
        return BadRequest("Erro na requisição.");
      }
    }

  }
}