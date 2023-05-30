using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces.API
{
  public interface IAuthenticationContract
  {
    string ClientId { get; }
    Task<Token> GetToken();
  }
}