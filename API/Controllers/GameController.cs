using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClipItch.API.Configuration;
using ClipItch.API.Interface;
using ClipItch.API.ViewModels;
using ClipItch.API.ViewModels.Games;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace ClipItch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameInterface _gameInterface;        
        private readonly Conexao _conexao;

        public GameController(IGameInterface gameInterface)
        {
            _gameInterface = gameInterface;
            _conexao = new Conexao();
        }

        [HttpGet("topGames")]
        public async Task<IActionResult> GetTopGames()
        {
            try
            {
                TokenViewModel tokenViewModel = await _conexao.ObterToken();

                var callback = RestService.For<IGameInterface>("https://api.twitch.tv/", new RefitSettings()
                {
                    AuthorizationHeaderValueGetter = () => Task.FromResult(tokenViewModel.access_token)
                });

                var result = callback.GetTopGames(_conexao.ClientId).Result;

                List<GameViewModel> gamesList = result.data;

                return Ok(gamesList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}