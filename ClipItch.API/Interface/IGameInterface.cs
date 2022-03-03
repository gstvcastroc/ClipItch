using System.Threading.Tasks;
using ClipItch.API.ViewModels.Games;
using Refit;

namespace ClipItch.API.Interface
{
    public interface IGameInterface
    {
        [Get("/helix/games/top")]
        [Headers("Authorization: Bearer")]
        Task<GamesRootViewModel> GetTopGames([Header("Client-Id")] string client_id);         
    }
}