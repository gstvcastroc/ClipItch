using API.Models.Response;
using Refit;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IGamesInterface
    {
        [Get("/helix/games/top")]
        [Headers("Authorization: Bearer")]
        Task<TopGames> GetTopGames([Header("Client-Id")] string client_id);         
    }
}