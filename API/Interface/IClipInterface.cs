using System.Threading.Tasks;
using API.ViewModels;
using Refit;

namespace API.Interface
{
    public interface IClipInterface
    {
        [Get("/helix/clips?game_id={id}")]
        [Headers("Authorization: Bearer")]
        Task<ClipsRootViewModel> GetClipes(int id, [Header("Client-Id")] string client_id);
    }
}