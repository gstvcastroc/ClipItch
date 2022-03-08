using System;

namespace ClipItch.API.Models
{
    public class Clipe
    {
        public int Id { get; set; }
        public string EmbedUrl { get; set; }
        public string EmbedHtml { get; set; }
        public string Game { get; set; }
        public string Language { get; set; }
        public int Views { get; set; }
        public float Duration { get; set; }
        public DateTime CreatedAte { get; set; }
        public BroadCaster BroadCaster { get; set; }
        public Curator Curator { get; set; }
        public Vod Vod { get; set; }
        public ThumbNails ThumbNails { get; set; }
    }
}