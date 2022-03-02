using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClipItch.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();                    
        
        [HttpGet("token")]
        public async Task<IActionResult> ObterToken()
        {
            var values = new Dictionary<string, string>
            {
                { "client_id", "" },
                { "client_secret", "" },
                { "grant_type", "client_credentials"}
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return Ok(responseString);
        }
    }
}