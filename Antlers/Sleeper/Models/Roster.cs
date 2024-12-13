using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class Roster
    {
        [JsonProperty("roster_id")]
        public int RosterId { get; set; }
        [JsonProperty("players")]
        public int[]? Players { get; set; }
        [JsonProperty("starters")]
        public int[]? Starters { get; set; }
        [JsonProperty("reserve")]
        public int[]? Reserve { get; set; }
        [JsonProperty("taxi")]
        public int[]? Taxi { get; set; }
        [JsonProperty("settings")]
        public RosterSettings? Settings { get; set; }
        [JsonProperty("owner_id")]
        public string? OwnerId { get; set; }
        [JsonProperty("league_id")]
        public string? LeagueId { get; set; }
    }
}
