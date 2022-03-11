using System.Threading.Tasks;
using API.Models;
using Refit;

namespace API.Interfaces
{
    public interface IGamesInterface
    {
        [Get("/helix/games/top")]
        [Headers("Authorization: Bearer")]
        Task<TopGames> GetTopGames([Header("Client-Id")] string client_id);         
    }
}