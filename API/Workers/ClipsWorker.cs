using API.Interfaces;
using Coravel.Invocable;
using System.Threading.Tasks;

namespace API.Workers
{
  public class ClipsWorker : IInvocable
  {

    private readonly IClipsService _clipsService;

    public ClipsWorker(IClipsService clipsService)
    {
      _clipsService = clipsService;
    }

    public async Task Invoke()
    {
      var clipsList = await _clipsService.GetClipsFromTwitchAsync();

      await _clipsService.AddClipsToDatabaseAsync(clipsList);
    }
  }
}
