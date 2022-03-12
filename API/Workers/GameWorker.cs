using API.Data;
using API.Services;
using Coravel.Invocable;
using System.Threading.Tasks;

namespace API.Workers
{
  public class GameWorker : IInvocable
  {

    private readonly IGamesService _gamesService;

    public GameWorker(IGamesService gamesService)
    {
      _gamesService = gamesService;
    }

    public async Task Invoke()
    {
      var gamesList = await _gamesService.GetTopGamesAsync();

      await _gamesService.AddGamesToDatabase(gamesList);
    }
  }
}
