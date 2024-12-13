using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class League
    {
        [JsonProperty("league_id")]
        public string? LeagueId { get; set; }
        [JsonProperty("previous_league_id")]
        public string? PreviousLeagueId { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("settings")]
        public LeagueSettings? Settings { get; set; }
        [JsonProperty("avatar")]
        public string? Avatar { get; set; }
        [JsonProperty("season")]
        public string? Season { get; set; }
        [JsonProperty("season_type")]
        public string? SeasonType { get; set; }
        [JsonProperty("sport")]
        public string? Sport { get; set; }
        [JsonProperty("scoring_settings")]
        public ScoringSettings? ScoringSettings { get; set; }
        [JsonProperty("draft_id")]
        public string? DraftId { get; set; }
        [JsonProperty("roster_positions")]
        public string[]? RosterPositions { get; set; }
        [JsonProperty("total_rosters")]
        public int TotalRosters { get; set; }
    }
}