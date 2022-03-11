using System.Threading.Tasks;
using API.Models;
using Refit;

namespace API.Interfaces
{
    public interface IClipInterface
    {
        [Get("/helix/clips?game_id={id}")]
        [Headers("Authorization: Bearer")]
        Task<Clips> GetClips(int id, [Header("Client-Id")] string client_id);
    }
}