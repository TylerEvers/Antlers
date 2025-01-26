using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class PlayerMetadata
    {
        [JsonProperty("channel_id")]
        public string? PlayerId { get; set; }
        [JsonProperty("rookie_year")]
        public string? RookieYear { get; set; }
    }
}
