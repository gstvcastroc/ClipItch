using API.Data;
using API.Entities;
using API.Interfaces;
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
        private readonly IAuthentication _authentication;
        private readonly IGamesService _gamesService;
        private readonly DataContext _context;

        public ClipsService(
          IAuthentication authentication,
          IGamesService gamesService,
          DataContext context)
        {
            _authentication = authentication;
            _gamesService = gamesService;
            _context = context;
        }

        // Método para buscar clips da API do Twitch.
        public async Task<List<Clip>> GetClipsFromTwitchAsync()
        {
            var token = await _authentication.GetToken();

            var gamesList = await _gamesService.GetGamesFromTwitchAsync();

            var clipsList = new List<Clip>();

            foreach (var game in gamesList)
            {
                int gameId = int.Parse(game.Id);

                var callback = RestService.For<IClipsInterface>("https://api.twitch.tv/", new RefitSettings()
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

            foreach (var entry in clipsList)
            {
                var clip = await _context.Clips
                  .AsNoTracking()
                  .FirstOrDefaultAsync(x => x.Id == entry.Id);

                if (clip is not null) continue;

                // Adiciona o nome do jogo ao clip.
                entry.GameName = await GetGameName(entry.GameId);

                _context.Clips.Add(entry);
                await _context.SaveChangesAsync();
            }
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

            var json = GetJson(clipsList);

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

            var json = GetJson(clipsList);

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
                  .Where(x => x.CreatedAt.Date == DateTime.Today)
                  .OrderByDescending(x => x.ViewCount)
                  .ToListAsync();
            }

            else
            {
                clipsList = await _context.Clips
                  .AsNoTracking()
                  .Where(x => x.CreatedAt.Date == DateTime.Today)
                  .OrderByDescending(x => x.ViewCount)
                  .Take(quantity.Value)
                  .ToListAsync();
            }

            var json = GetJson(clipsList);

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

            var json = GetJson(clipsList);

            return json;
        }

        private async Task<string> GetGameName(string gameId)
        {
            var game = await _context.Games
              .AsNoTracking()
              .FirstOrDefaultAsync(game => game.Id == gameId);

            return game.Name;
        }

        public static string GetJson(List<Clip> clipsList)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            var json = JsonSerializer.Serialize(clipsList, options);

            return json;
        }

        public async Task<Clip> GetClipById(string idClip)
        {
            var clipsList = await _context.Clips.AsNoTracking().FirstOrDefaultAsync(x => x.Id == idClip);

            return clipsList;
        }
    }
}
