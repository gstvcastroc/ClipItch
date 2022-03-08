using System.Threading.Tasks;
using ClipItch.API.ViewModels;
using Refit;

namespace ClipItch.API.Interface
{
    public interface IClipeInterface
    {
        [Get("/helix/clips?game_id={id}")]
        [Headers("Authorization: Bearer")]
        Task<ClipesRootViewModel> GetClipes(int id, [Header("Client-Id")] string client_id);
    }
}