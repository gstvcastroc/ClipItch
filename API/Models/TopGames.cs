using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Models
{
  public class TopGames
  {
    [JsonProperty("data")]
    public List<Game> Data { get; set; }

    [JsonProperty("pagination")]
    public Pagination Pagination { get; set; }
  }
}