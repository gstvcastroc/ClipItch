using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ClipesController : Controller
    {
        private readonly IClipeInterface _clipeInterface;
        private readonly IGameInterface _gameInterface;
        private readonly Conexao _conexao;

        public ClipesController(IClipeInterface clipeInterface, IGameInterface gameInterface)
        {
            _clipeInterface = clipeInterface;
            _gameInterface = gameInterface;
            _conexao = new Conexao();
        }

        [HttpGet("obterTodos")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                TokenViewModel tokenViewModel = await _conexao.ObterToken();

                var callbackGames = RestService.For<IGameInterface>("https://api.twitch.tv/", new RefitSettings()
                {
                    AuthorizationHeaderValueGetter = () => Task.FromResult(tokenViewModel.access_token)
                });

                var resultGames = callbackGames.GetTopGames(_conexao.ClientId).Result;

                List<ClipesViewModel> listaRetorno = new List<ClipesViewModel>();

                foreach (GameViewModel item in resultGames.data)
                {
                    int idGame = int.Parse(item.id);

                    var callback = RestService.For<IClipeInterface>("https://api.twitch.tv/", new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () => Task.FromResult(tokenViewModel.access_token)
                    });

                    var result = callback.GetClipes(idGame, _conexao.ClientId).Result;

                    listaRetorno = listaRetorno.AsEnumerable().Union(result.data.AsEnumerable()).ToList();
                }

                //ToDo: Melhorar performance colocando as requisções em um banco e buscando dele mesmo.
                return Ok(listaRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}