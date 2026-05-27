using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class Projection
    {
        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("date")]
        public string? Date { get; set; }

        [JsonProperty("stats")]
        public Dictionary<string, decimal>? Stats { get; set; }

        [JsonProperty("category")]
        public string? Category { get; set; }

        [JsonProperty("last_modified")]
        public long LastModified { get; set; }

        [JsonProperty("week")]
        public int? Week { get; set; }

        [JsonProperty("sport")]
        public string? Sport { get; set; }

        [JsonProperty("season_type")]
        public string? SeasonType { get; set; }

        [JsonProperty("season")]
        public string? Season { get; set; }

        [JsonProperty("player")]
        public Player? Player { get; set; }

        [JsonProperty("team")]
        public string? Team { get; set; }

        [JsonProperty("player_id")]
        public string? PlayerId { get; set; }

        [JsonProperty("game_id")]
        public string? GameId { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("week_shard")]
        public string? WeekShard { get; set; }

        [JsonProperty("company")]
        public string? Company { get; set; }

        [JsonProperty("opponent")]
        public string? Opponent { get; set; }
    }
}
