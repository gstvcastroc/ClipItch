using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Configuration;
using API.Data;
using API.Interfaces;
using API.Models;
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
    private readonly Authentication _conexao;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public GameController(IGameInterface gameInterface, DataContext context, IMapper mapper)
    {
      _gameInterface = gameInterface;
      _conexao = new Authentication();
      _context = context;
      _mapper = mapper;
    }

    [HttpGet("topGames")]
    public async Task<IActionResult> GetTopGames()
    {
      try
      {
        Token token = await _conexao.GetToken();

        var callback = RestService.For<IGameInterface>("https://api.twitch.tv/", new RefitSettings()
        {
          AuthorizationHeaderValueGetter = () => Task.FromResult(token.AccessToken)
        });

        var result = callback.GetTopGames(_conexao.ClientId).Result;

        List<Game> gamesList = result.Data;

        //_context.AddRange(gamesList);
        //_context.SaveChanges();

        return Ok(gamesList);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}