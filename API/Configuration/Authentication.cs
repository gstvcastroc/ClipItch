using API.Interfaces.API;
using API.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Configuration
{
  public class Authentication : IAuthenticationContract
  {
    // Propriedade que representa o client ID da API do Twitch.
    public string ClientId { get; } = "wlbp0x5pkyw3p2zr1ttqhmy1faur3d";

    // Constantes que representam o client secret e grant type da API do Twitch. 
    const string CLIENT_SECRET = "mxqi1gf8j031i1m384hh3lgvk6tw0n";
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

      var result = JsonSerializer.Deserialize<Token>(responseString);

      return result;
    }
  }
}