using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ClipItch.API.Interface;
using ClipItch.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;

namespace ClipItch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClipesController : Controller
    {
        private readonly IClipeInterface _clipeInterface;
        private readonly IMapper _mapper;

        public ClipesController(IClipeInterface clipeInterface, IMapper mapper)
        {
            _clipeInterface = clipeInterface;
            _mapper = mapper;
        }

        [HttpGet("obterTodos")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                TokenViewModel viewModel = await ObterToken();

                var callback = RestService.For<IClipeInterface>("https://api.twitch.tv/", new RefitSettings()
                {
                    AuthorizationHeaderValueGetter = () => Task.FromResult(viewModel.access_token)
                });

                var result = callback.GetClipes(21779, "Client_Id").Result;

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("token")]
        public async Task<IActionResult> Token()
        {
            try
            {
                TokenViewModel viewModel = await ObterToken();

                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<TokenViewModel> ObterToken()
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "client_id", "Client_Id" },
                { "client_secret", "Client_Secret" },
                { "grant_type", "client_credentials"}
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);

            var responseString = await response.Content.ReadAsStringAsync();

            TokenViewModel result = JsonConvert.DeserializeObject<TokenViewModel>(responseString);

            return result;
        }
    }
}