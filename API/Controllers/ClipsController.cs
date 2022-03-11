using API.Configuration;
using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ClipsController : Controller
  {
    private readonly IClipInterface _clipInterface;
    private readonly IGameInterface _gameInterface;
    private readonly Authentication _conexao;
    private readonly DataContext _context;

    public ClipsController(DataContext context, IClipInterface clipeInterface, IGameInterface gameInterface)
    {
      _clipInterface = clipeInterface;
      _gameInterface = gameInterface;
      _conexao = new Authentication();
      _context = context;
    }

    [HttpGet("obterTodos")]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        Token token = await _conexao.GetToken();

        var callbackGames = RestService.For<IGameInterface>("https://api.twitch.tv/", new RefitSettings()
        {
          AuthorizationHeaderValueGetter = () => Task.FromResult(token.AccessToken)
        });

        var gamesList = await callbackGames.GetTopGames(_conexao.ClientId);

        var listaRetorno = new List<Clip>();

        foreach (var game in gamesList.Data)
        {
          int gameId = int.Parse(game.Id);

          var callback = RestService.For<IClipInterface>("https://api.twitch.tv/", new RefitSettings()
          {
            AuthorizationHeaderValueGetter = () => Task.FromResult(token.AccessToken)
          });

          var result = await callback.GetClips(gameId, _conexao.ClientId);

          listaRetorno.AddRange(result.Data);

          //listaRetorno.AsEnumerable().Union(result.Data.AsEnumerable()).ToList();
        }

        //_context.AddRange(listaRetorno);
        //_context.SaveChanges();

        return Ok(listaRetorno);
      }

      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}