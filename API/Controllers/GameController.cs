using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Configuration;
using API.Data;
using API.Interface;
using API.Models;
using API.ViewModels;
using API.ViewModels.Games;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GameController : ControllerBase
  {
    private readonly IGameInterface _gameInterface;
    private readonly Connection _conexao;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GameController(IGameInterface gameInterface, DataContext context, IMapper mapper)
    {
      _gameInterface = gameInterface;
      _conexao = new Connection();
      _context = context;
      _mapper = mapper;
    }

    [HttpGet("topGames")]
    public async Task<IActionResult> GetTopGames()
    {
      try
      {
        TokenViewModel tokenViewModel = await _conexao.GetToken();

        var callback = RestService.For<IGameInterface>("https://api.twitch.tv/", new RefitSettings()
        {
          AuthorizationHeaderValueGetter = () => Task.FromResult(tokenViewModel.access_token)
        });

        var result = callback.GetTopGames(_conexao.ClientId).Result;

        List<GameViewModel> gamesListViewModel = result.data;

        var gamesList = new List<Game>();

        foreach (var gameViewModel in gamesListViewModel)
        {
          var game = _mapper.Map<Game>(gameViewModel);
          gamesList.Add(game);
        }
        _context.Games.AddRange(gamesList);
        _context.SaveChanges();

        return Ok();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}