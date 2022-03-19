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
        var search = await _context.Clips
          .AsNoTracking()
          .Where
          (clip =>
           EF.Functions.Like(clip.Title, $"%{query}%") ||
           EF.Functions.Like(clip.GameName, $"%{query}%") ||
           EF.Functions.Like(clip.CreatorName, $"%{query}%") ||
           EF.Functions.Like(clip.BroadcasterName, $"%{query}%"))
          .ToListAsync();

        clipsList.AddRange(search);
      }

      clipsList.OrderByDescending(clips => clips.ViewCount);

      var json = ClipsService.GetJson(clipsList);

      return json;
    }
  }
}
