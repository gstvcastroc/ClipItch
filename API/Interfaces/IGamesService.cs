using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
  public interface IGamesService
  {
    Task<List<Game>> GetGamesFromTwitchAsync();
    Task AddGamesToDatabaseAsync(List<Game> gameList);
    Task<string> GetGamesFromDatabaseAsync();
  }
}