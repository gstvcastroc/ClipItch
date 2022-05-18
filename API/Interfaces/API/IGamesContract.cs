using API.Models.Response;
using Refit;
using System.Threading.Tasks;

namespace API.Interfaces.API
{
    public interface IGamesContract
    {
        [Get("/helix/games/top")]
        [Headers("Authorization: Bearer")]
        Task<TopGames> GetTopGames([Header("Client-Id")] string client_id);         
    }
}