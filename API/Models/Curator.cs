using System.Collections.Generic;

namespace API.Models
{
    public class Curator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ChannelUrl { get; set; }
        public string Logo { get; set; }

        // Propriedade de navegação
        public IList<Clip> Clips { get; set; }
  }
}