using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
  public interface IClipsService
  {
    Task<List<Clip>> GetClipsFromTwitchAsync();
    Task AddClipsToDatabaseAsync(List<Clip> clipsList);
    Task<string> GetClipsAsync(int? quantity);
    Task<string> GetClipsByGameIdAsync(string gameId, int? quantity);
    Task<string> GetDailyClipsAsync(int? quantity);
    Task<string> GetWeeklyClipsAsync(int? quantity);
  }
}