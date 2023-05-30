using System.Text.Json;

namespace API.Utils
{
  public class Serialization
  {
    public static string GetJson(object @string)
    {
      var options = new JsonSerializerOptions { WriteIndented = true };

      var json = JsonSerializer.Serialize(@string, options);

      return json;
    }
  }
}
