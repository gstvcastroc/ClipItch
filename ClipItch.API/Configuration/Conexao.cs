using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ClipItch.API.ViewModels;
using Newtonsoft.Json;

namespace ClipItch.API.Configuration
{
    public class Conexao
    {
        public string ClientId { get {return "Client_Secret";} }
        private string ClientSecret { get {return "Client_Id";} }
        private string GrantType { get {return "client_credentials";} }

        public async Task<TokenViewModel> ObterToken()
        {
            HttpClient client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "client_id", this.ClientId },
                { "client_secret", this.ClientSecret },
                { "grant_type", this.GrantType }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);

            var responseString = await response.Content.ReadAsStringAsync();

            TokenViewModel result = JsonConvert.DeserializeObject<TokenViewModel>(responseString);

            return result;
        }
    }
}