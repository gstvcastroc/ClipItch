using Newtonsoft.Json;

namespace API.Models
{
  public class Pagination
  {
    [JsonProperty("cursor")]
    public string Cursor { get; set; }
  }
}