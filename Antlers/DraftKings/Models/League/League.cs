using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class League
    {
        [JsonProperty("id")]
        public required string Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("sportId")]
        public string? SportId { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
        [JsonProperty("tags")]
        public string[]? Tags { get; set; }
        [JsonProperty("isTeamSwap")]
        public bool TeamSwap { get; set; }
        [JsonProperty("featured")]
        public bool Featured { get; set; }
    }
}
