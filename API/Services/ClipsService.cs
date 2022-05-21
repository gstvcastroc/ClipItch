using API.Data;
using API.Entities;
using API.Interfaces;
using API.Interfaces.API;
using API.Utils;
using Microsoft.EntityFrameworkCore;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Services
{
  public class ClipsService : IClipsService
  {
    private readonly IAuthenticationContract _authentication;
    private readonly IGamesService _gamesService;
    private readonly IUsersService _usersService;
    private readonly DataContext _context;

    public ClipsService(
      IAuthenticationContract authentication,
      IGamesService gamesService,
      IUsersService usersService,
      DataContext context)
    {
      _authentication = authentication;
      _gamesService = gamesService;
      _usersService = usersService;
      _context = context;
    }

    // Método para buscar clips da API do Twitch.
    public async Task<List<Clip>> GetClipsFromTwitchAsync()
    {
      var token = await _authentication.GetToken();

      var gamesListJson = await _gamesService.GetGamesAsync();

      var gamesList = JsonSerializer.Deserialize<List<Game>>(gamesListJson);

      var clipsList = new List<Clip>();

      foreach (var game in gamesList)
      {
        int gameId = int.Parse(game.Id);

        var callback = RestService.For<IClipsContract>("https://api.twitch.tv/", new RefitSettings()
        {
          AuthorizationHeaderValueGetter = () => Task.FromResult(token.AccessToken)
        });

        var result = await callback.GetClips(gameId, _authentication.ClientId);

        clipsList.AddRange(result.Data);

      }

      return clipsList;
    }

    // Método para adicionar a lista de clips no banco de dados.
    public async Task AddClipsToDatabaseAsync(List<Clip> clipsList)
    {
      var finalParams = new List<string>();

      var queryParams = new List<string>();

      var broadcasters = new List<User>();

      foreach (var clip in clipsList) finalParams.Add(clip.BroadcasterId);

      while (finalParams.Count() > 100)
      {
        queryParams = finalParams.Take(100).ToList();
        finalParams.RemoveRange(0, 100);

        broadcasters.AddRange(await _usersService.GetBroadcasters(queryParams));
      }

      broadcasters.AddRange(await _usersService.GetBroadcasters(finalParams));

      foreach (var entry in clipsList)
      {
        var clip = await _context.Clips
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Id == entry.Id);

        if (clip is not null) continue;

        // Adiciona o nome do jogo e o URL da imagem do streamer ao clip.
        entry.GameName = await _gamesService.GetGameNameAsync(entry.GameId);
        entry.BroadcasterProfileImageUrl = GetBroadcasterProfileImageUrl(broadcasters, entry.BroadcasterId);

        _context.Clips.Add(entry);
        await _context.SaveChangesAsync();
      }
    }

    private string GetBroadcasterProfileImageUrl(List<User> broadcasters, string broadcasterId)
    {
      var broadcaster = broadcasters.FirstOrDefault(broadcaster => broadcaster.Id == broadcasterId);

      if (broadcaster is null) return null;

      return broadcaster.ProfileImageUrl;
    }

    // Método para buscar a lista de clips no banco de dados e enviá-la em formato JSON. Lista ordenada apenas de acordo com o número de visualizações, independente das datas dos clips. Recebe a quantidade de clips a ser retornada como parâmetro. Caso esse parâmetro seja nulo, retorna todos os clips.
    public async Task<string> GetClipsAsync(int? quantity = null)
    {
      var clipsList = new List<Clip>();

      if (quantity is null)
      {
        clipsList = await _context.Clips
          .AsNoTracking()
          .OrderByDescending(x => x.ViewCount)
          .ToListAsync();
      }

      else
      {
        clipsList = await _context.Clips
          .AsNoTracking()
          .OrderByDescending(x => x.ViewCount)
          .Take(quantity.Value)
          .ToListAsync();
      }

      var json = Serialization.GetJson(clipsList);

      return json;
    }

    // Método para buscar a lista de clips de um determinada jogo no banco de dados e enviá-la em formato JSON. Lista ordenada apenas de acordo com o número de visualizações, independente das datas dos clips. Recebe o ID do jogo e a quantidade de clips a ser retornada como parâmetro. Caso esse parâmetro seja nulo, retorna todos os clips do jogo.
    public async Task<string> GetClipsByGameIdAsync(string gameId, int? quantity = null)
    {
      var clipsList = new List<Clip>();

      if (quantity is null)
      {
        clipsList = await _context.Clips
          .AsNoTracking()
          .Where(x => x.GameId == gameId)
          .OrderByDescending(x => x.ViewCount)
          .ToListAsync();
      }

      else
      {
        clipsList = await _context.Clips
          .AsNoTracking()
          .Where(x => x.GameId == gameId)
          .OrderByDescending(x => x.ViewCount)
          .Take(quantity.Value)
          .ToListAsync();
      }

      var json = Serialization.GetJson(clipsList);

      return json;
    }

    // Método para buscar a lista de clips mais visualizados do dia. A lista é ordenada de acordo com o número de visualizações dos clips. Recebe a quantidade de clips a ser buscada como parâmetro. Caso esse parâmetro seja nulo, retorna todos os clips do dia.
    public async Task<string> GetDailyClipsAsync(int? quantity = null)
    {
      var clipsList = new List<Clip>();

      if (quantity is null)
      {
        clipsList = await _context.Clips
          .AsNoTracking()
          .Where(x => x.CreatedAt.Date == DateTime.Today.AddDays(-3))
          .OrderByDescending(x => x.ViewCount)
          .ToListAsync();
      }

      else
      {
        clipsList = await _context.Clips
          .AsNoTracking()
          .Where(x => x.CreatedAt.Date == DateTime.Today.AddDays(-3))
          .OrderByDescending(x => x.ViewCount)
          .Take(quantity.Value)
          .ToListAsync();
      }

      var json = Serialization.GetJson(clipsList);

      return json;
    }

    // Método para buscar a lista de clips mais visualizados da semana. A lista é ordenada de acordo com o número de visualizações dos clips. Recebe a quantidade de clips a ser buscada como parâmetro. Caso esse parâmetro seja nulo, retorna todos os clips do dia.
    public async Task<string> GetWeeklyClipsAsync(int? quantity = null)
    {
      var clipsList = new List<Clip>();

      if (quantity is null)
      {
        clipsList = await _context.Clips
          .AsNoTracking()
          .Where(x => x.CreatedAt.Date >= DateTime.Today.AddDays(-7))
          .OrderByDescending(x => x.ViewCount)
          .ToListAsync();
      }

      else
      {
        clipsList = await _context.Clips
          .AsNoTracking()
          .Where(x => x.CreatedAt.Date >= DateTime.Today.AddDays(-7))
          .OrderByDescending(x => x.ViewCount)
          .Take(quantity.Value)
          .ToListAsync();
      }

      var json = Serialization.GetJson(clipsList);

      return json;
    }

    public async Task<string> GetClipByIdAsync(string clipId)
    {
      var clip = await _context.Clips.AsNoTracking().FirstOrDefaultAsync(x => x.Id == clipId);

      var json = Serialization.GetJson(clip);

      return json;
    }
  }
}
