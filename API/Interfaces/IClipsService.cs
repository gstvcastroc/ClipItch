using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
  public interface IClipsService
  {
    Task<List<Clip>> GetClipsAsync();
    Task AddClipsToDatabase(List<Clip> clipsList);
  }
}