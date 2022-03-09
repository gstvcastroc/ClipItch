namespace API.Models
{
    public class Vod
    {
        public int Id { get; set; }
        public string Url { get; set; }

        // Propriedade de navegação
        public Clip Clip { get; set; }
  }
}