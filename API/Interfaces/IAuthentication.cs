using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces
{
  public interface IAuthentication
  {
    string ClientId { get; }
    Task<Token> GetToken();
  }
}