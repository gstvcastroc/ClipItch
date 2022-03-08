using System;

namespace API.Models
{
    public class Clip
    {
        public int Id { get; set; }
        public string EmbedUrl { get; set; }
        public string EmbedHtml { get; set; }
        public string Game { get; set; }
        public string Language { get; set; }
        public int Views { get; set; }
        public float Duration { get; set; }
        public DateTime CreatedAt { get; set; }
        public Broadcaster Broadcaster { get; set; }
        public Curator Curator { get; set; }
        public Vod Vod { get; set; }
        public Thumbnails Thumbnails { get; set; }
    }
}