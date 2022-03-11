using API.Models;
using System.Threading.Tasks;

namespace API.Configuration
{
  public interface IAuthentication
  {
    string ClientId { get; }
    Task<Token> GetToken();
  }
}