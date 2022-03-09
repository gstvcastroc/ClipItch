using API.Configuration;
using API.Interface;
using API.ViewModels;
using API.ViewModels.Games;
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
        private readonly Connection _conexao;

        public ClipsController(IClipInterface clipeInterface, IGameInterface gameInterface)
        {
            _clipInterface = clipeInterface;
            _gameInterface = gameInterface;
            _conexao = new Connection();
        }

        [HttpGet("obterTodos")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                TokenViewModel tokenViewModel = await _conexao.GetToken();

                var callbackGames = RestService.For<IGameInterface>("https://api.twitch.tv/", new RefitSettings()
                {
                    AuthorizationHeaderValueGetter = () => Task.FromResult(tokenViewModel.access_token)
                });

                var resultGames = callbackGames.GetTopGames(_conexao.ClientId).Result;

                List<ClipsViewModel> listaRetorno = new List<ClipsViewModel>();

                foreach (GameViewModel item in resultGames.data)
                {
                    int idGame = int.Parse(item.Id);

                    var callback = RestService.For<IClipInterface>("https://api.twitch.tv/", new RefitSettings()
                    {
                        AuthorizationHeaderValueGetter = () => Task.FromResult(tokenViewModel.access_token)
                    });

                    var result = callback.GetClips(idGame, _conexao.ClientId).Result;

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