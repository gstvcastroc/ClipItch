using API.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models.Response
{
  public class Users
  {
    [JsonPropertyName("data")]
    public List<User> Data { get; set; }
  }
}
