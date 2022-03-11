using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models
{
  public class Clips
  {
    [JsonPropertyName("data")]
    public List<Clip> Data { get; set; }

    [JsonPropertyName("pagination")]
    public Pagination Pagination { get; set; }
  }
}