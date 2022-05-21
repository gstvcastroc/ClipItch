using API.Models.Response;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces.API
{
  public interface IUsersContract
  {
    [Get("/helix/users")]
    [Headers("Authorization: Bearer")]
    Task<Users> GetUsers([Query(CollectionFormat.Multi)] List<string> id, [Header("Client-Id")] string client_id);
  }
}