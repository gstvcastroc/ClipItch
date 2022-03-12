using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
  public interface IGamesService
  {
    Task<List<Game>> GetTopGamesAsync();
    Task AddGamesToDatabase(List<Game> gameList);
  }
}