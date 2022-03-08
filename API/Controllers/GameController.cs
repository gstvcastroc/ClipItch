using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Configuration;
using API.Interface;
using API.ViewModels;
using API.ViewModels.Games;
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

        public GameController(IGameInterface gameInterface)
        {
            _gameInterface = gameInterface;
            _conexao = new Connection();
        }

        [HttpGet("topGames")]
        public async Task<IActionResult> GetTopGames()
        {
            try
            {
                TokenViewModel tokenViewModel = await _conexao.GetToken();

                var callback = RestService.For<IGameInterface>("https://api.twitch.tv/", new RefitSettings()
                {
                    AuthorizationHeaderValueGetter = () => Task.FromResult(tokenViewModel.AccessToken)
                });

                var result = callback.GetTopGames(_conexao.ClientId).Result;

                List<GameViewModel> gamesList = result.Data;

                return Ok(gamesList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}