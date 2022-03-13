using System.Text.Json.Serialization;

namespace API.Models
{
  public class Pagination
  {
    [JsonPropertyName("cursor")]
    public string Cursor { get; set; }
  }
}