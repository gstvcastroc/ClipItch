using API.Models.Response;
using Refit;
using System.Threading.Tasks;

namespace API.Interfaces.API
{
    public interface IClipsContract
    {
        [Get("/helix/clips?game_id={id}")]
        [Headers("Authorization: Bearer")]
        Task<Clips> GetClips(int id, [Header("Client-Id")] string client_id);
    }
}