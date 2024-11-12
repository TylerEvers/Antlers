using System.Text.Json.Serialization;

namespace Antlers.Sleeper.Models
{
    public class Roster
    {
        [JsonPropertyName("roster_id")]
        public int RosterId { get; set; }
        [JsonPropertyName("players")]
        public int[]? Players { get; set; }
        [JsonPropertyName("starters")]
        public int[]? Starters { get; set; }
        [JsonPropertyName("reserve")]
        public int[]? Reserve { get; set; }
        [JsonPropertyName("taxi")]
        public int[]? Taxi { get; set; }
        [JsonPropertyName("settings")]
        public RosterSettings? Settings { get; set; }
        [JsonPropertyName("owner_id")]
        public string? OwnerId { get; set; }
        [JsonPropertyName("league_id")]
        public string? LeagueId { get; set; }
    }
}
