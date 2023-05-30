using System.Threading.Tasks;

namespace API.Interfaces
{
  public interface ISearchService
  {
    Task<string> SearchClipsAsync(string input);
  }
}
