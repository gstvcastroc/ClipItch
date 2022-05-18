using API.Entities;
using API.Interfaces;
using API.Interfaces.API;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
  public class UsersService : IUsersService
  {
    private readonly IAuthenticationContract _authentication;

    public UsersService(IAuthenticationContract authentication) => _authentication = authentication;

    public async Task<List<User>> GetBroadcasters(List<string> idList)
    {
      var token = await _authentication.GetToken();

      var callback = RestService.For<IUsersContract>("https://api.twitch.tv/", new RefitSettings()
      {
        AuthorizationHeaderValueGetter = () => Task.FromResult(token.AccessToken)
      });

        var user = await callback.GetUsers(idList, _authentication.ClientId);

        return user.Data;
    }
  }
}
