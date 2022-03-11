using System.Threading.Tasks;
using API.Models;
using Refit;

namespace API.Interfaces
{
    public interface IGameInterface
    {
        [Get("/helix/games/top")]
        [Headers("Authorization: Bearer")]
        Task<TopGames> GetTopGames([Header("Client-Id")] string client_id);         
    }
}