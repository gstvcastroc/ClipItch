using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  [Route("api/")]
  [ApiController]
  public class SearchController : Controller
  {
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
      _searchService = searchService;
    }



  }
}