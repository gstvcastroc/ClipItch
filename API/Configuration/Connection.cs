using API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Configuration
{
  public class Connection
  {
    // Propriedade que representa o client ID da API do Twitch.
    public string ClientId { get; } = "kgl6s2v5suh14svu58vdbdzpazxc0j";

    // Constantes que representam o client secret e grant type da API do Twitch. 
    const string CLIENT_SECRET = "1gh4hvydxoow3byxqavpf8iepgsuui";
    const string GRANT_TYPE = "client_credentials";

    public async Task<Token> GetToken()
    {
      var client = new HttpClient();

      var values = new Dictionary<string, string>
      {
        { "client_id", ClientId },
        { "client_secret", CLIENT_SECRET },
        { "grant_type", GRANT_TYPE }
      };

      var content = new FormUrlEncodedContent(values);

      var response = await client.PostAsync("https://id.twitch.tv/oauth2/token", content);

      var responseString = await response.Content.ReadAsStringAsync();

      var result = JsonConvert.DeserializeObject<Token>(responseString);

      return result;
    }
  }
}