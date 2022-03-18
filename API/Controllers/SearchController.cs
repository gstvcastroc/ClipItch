using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SearchController : Controller
  {
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
      _searchService = searchService;
    }

    [HttpPost("clips/{input}")]
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