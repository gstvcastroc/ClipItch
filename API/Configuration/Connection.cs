using API.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Configuration
{
    public class Connection
    {
        public string ClientId { get {return "Client_Id";} }
        private string ClientSecret { get {return "Client_Secret";} }
        private string GrantType { get {return "client_credentials";} }

        public async Task<TokenViewModel> GetToken()
        {
            var client = new HttpClient();

            var values = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "grant_type", GrantType }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TokenViewModel>(responseString);

            return result;
        }
    }
}