using API.Configuration;
using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
  public class GamesService : IGamesService
  {
    private readonly IAuthentication _authentication;
    private readonly DataContext _context;

    public GamesService(DataContext context, IAuthentication authentication)
    {
      _context = context;
      _authentication = authentication;
    }

    public async Task<List<Game>> GetTopGamesAsync()
    {
      var token = await _authentication.GetToken();

      var callback = RestService.For<IGamesInterface>("https://api.twitch.tv/", new RefitSettings()
      {
        AuthorizationHeaderValueGetter = () => Task.FromResult(token.AccessToken)
      });

      var result = await callback.GetTopGames(_authentication.ClientId);

      var gamesList = result.Data;

      return gamesList;
    }

    public async Task AddGamesToDatabase(List<Game> gameList)
    {
      foreach (var entry in gameList)
      {
        var game = await _context.Games
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Id == entry.Id);

        if (game is not null) return;

        _context.Games.Add(entry);
        await _context.SaveChangesAsync();
      }
    }
  }
}
