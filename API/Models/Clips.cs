using Newtonsoft.Json;
using System.Collections.Generic;

namespace API.Models
{
  public class Clips
  {
    [JsonProperty("data")]
    public List<Clip> Data { get; set; }

    [JsonProperty("pagination")]
    public Pagination Pagination { get; set; }
  }
}