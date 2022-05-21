using API.Data;
using API.Entities;
using API.Interfaces;
using API.Interfaces.API;
using API.Utils;
using Microsoft.EntityFrameworkCore;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
  public class GamesService : IGamesService
  {
    private readonly IAuthenticationContract _authentication;
    private readonly DataContext _context;

    public GamesService(DataContext context, IAuthenticationContract authentication)
    {
      _context = context;
      _authentication = authentication;
    }

    // Método para buscar jogos da API do Twitch.
    public async Task<List<Game>> GetGamesFromTwitchAsync()
    {
      var token = await _authentication.GetToken();

      var callback = RestService.For<IGamesContract>("https://api.twitch.tv/", new RefitSettings()
      {
        AuthorizationHeaderValueGetter = () => Task.FromResult(token.AccessToken)
      });

      var result = await callback.GetTopGames(_authentication.ClientId);

      var gamesList = result.Data;

      return gamesList;
    }

    // Método para adicionar a lista de jogos no banco de dados.
    public async Task AddGamesToDatabaseAsync(List<Game> gameList)
    {
      foreach (var entry in gameList)
      {
        var game = await _context.Games
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Id == entry.Id);

        if (game is not null) continue;

        _context.Games.Add(entry);
        await _context.SaveChangesAsync();
      }
    }

    // Método para buscar a lista de jogos no banco de dados e enviá-la em formato JSON.
    public async Task<string> GetGamesAsync()
    {
      var gamesList = await _context.Games.AsNoTracking().ToListAsync();

      var json = Serialization.GetJson(gamesList);

      return json;
    }

    public async Task<string> GetGameNameAsync(string gameId)
    {
      var game = await _context.Games
        .AsNoTracking()
        .FirstOrDefaultAsync(game => game.Id == gameId);

      return game.Name;
    }
  }
}
