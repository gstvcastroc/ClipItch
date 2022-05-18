using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
  public interface IGamesService
  {
    Task<List<Game>> GetGamesFromTwitchAsync();
    Task AddGamesToDatabaseAsync(List<Game> gameList);
    Task<string> GetGamesAsync();
    Task<string> GetGameNameAsync(string gameId);
  }
}