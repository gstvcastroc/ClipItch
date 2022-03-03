using System;

namespace ClipItch.API.ViewModels
{
    public class ClipesViewModel
    {
        public int id { get; set; }
        public string url { get; set; }
        public string embed_url { get; set; }
        public int broadcaster_id { get; set; }
        public string broadcaster_name { get; set; }
        public int creator_id { get; set; }
        public string creator_name { get; set; }
        public int? video_id { get; set; }
        public int game_id { get; set; }
        public string language { get; set; }
        public string title { get; set; }
        public int view_count { get; set; }
        public DateTime created_at { get; set; }
        public string thumbnail_url { get; set; }
        public float duration { get; set; }
    }
}