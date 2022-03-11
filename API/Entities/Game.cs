using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace API.Models
{
  public class Game
  {
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("box_art_url")]
    public string BoxArtUrl { get; set; }
  }
}