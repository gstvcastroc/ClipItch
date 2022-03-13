using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
  public interface IClipsService
  {
    Task<List<Clip>> GetClipsFromTwitchAsync();
    Task AddClipsToDatabaseAsync(List<Clip> clipsList);
    Task<string> GetClipsFromDatabaseAsync(int? quantity);
    Task<string> GetClipsFromDatabaseByGameIdAsync(string gameId, int? quantity);
  }
}