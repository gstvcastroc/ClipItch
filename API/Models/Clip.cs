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
    public int BroadcasterId { get; set; }
    public int CuratorId { get; set; }
    public int VodId { get; set; }
    public int ThumbnailId { get; set; }

    // Propriedades de navegação
    public Broadcaster Broadcaster { get; set; }
    public Curator Curator { get; set; }
    public Vod Vod { get; set; }
    public Thumbnail Thumbnail { get; set; }
  }
}