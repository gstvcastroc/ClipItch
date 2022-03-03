using System.Threading.Tasks;
using Refit;

namespace ClipItch.API.Interface
{
    public interface IClipeInterface
    {
        [Get("/helix/clips?game_id={id}")]
        [Headers("Authorization: Bearer")]
        Task<dynamic> GetClipes(int id, [Header("Client-Id")] string client_id);
    }
}