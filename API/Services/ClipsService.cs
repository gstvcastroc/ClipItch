using API.Configuration;
using API.Data;
using API.Interfaces;
using API.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
  public class ClipsService : IClipsService
  {
    private readonly IAuthentication _authentication;
    private readonly IGamesService _gamesService;
    private readonly DataContext _context;

    public ClipsService(
      IAuthentication authentication,
      IGamesService gamesService,
      DataContext context)
    {
      _authentication = authentication;
      _gamesService = gamesService;
      _context = context;
    }

    public async Task<List<Clip>> GetClipsAsync()
    {
      var token = await _authentication.GetToken();

      var gamesList = await _gamesService.GetTopGamesAsync();

      var clipsList = new List<Clip>();

      foreach (var game in gamesList)
      {
        int gameId = int.Parse(game.Id);

        var callback = RestService.For<IClipsInterface>("https://api.twitch.tv/", new RefitSettings()
        {
          AuthorizationHeaderValueGetter = () => Task.FromResult(token.AccessToken)
        });

        var result = await callback.GetClips(gameId, _authentication.ClientId);

        clipsList.AddRange(result.Data);

      }

      return clipsList;
    }
  }
}
