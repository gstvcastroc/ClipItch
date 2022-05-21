using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
  public interface IUsersService
  {
    Task<List<User>> GetBroadcasters(List<string> idList);
  }
}