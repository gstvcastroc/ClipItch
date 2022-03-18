using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
  public class SearchService : ISearchService
  {
    private readonly DataContext _context;

    public SearchService(DataContext context)
    {
      _context = context;
    }

    public async Task<string> SearchClipsAsync(string input)
    {
      var queries = input
        .Trim()
        .Split(' ')
        .ToList();

      var filter = new[] { "of", "from", "the" };

      var filteredQueries = queries.Except(filter);

      var clipsList = new List<Clip>();

      foreach (var query in filteredQueries)
      {
        clipsList.AddRange(await _context.Clips
          .AsNoTracking()
          .Where
          (clip =>
           clip.Title.Contains(query) ||
           clip.GameName.Contains(query) ||
           clip.CreatorName.Contains(query) ||
           clip.BroadcasterName.Contains(query))
          .ToListAsync());
      }

      var json = ClipsService.GetJson(clipsList);

      return json;
    }
  }
}
