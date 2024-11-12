using System.Text.Json.Serialization;

namespace Antlers.Sleeper.Models
{
    public class League
    {
        [JsonPropertyName("league_id")]
        public string? LeagueId { get; set; }
        [JsonPropertyName("previous_league_id")]
        public string? PreviousLeagueId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("settings")]
        public LeagueSettings? Settings { get; set; }
        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
        [JsonPropertyName("season")]
        public string? Season { get; set; }
        [JsonPropertyName("season_type")]
        public string? SeasonType { get; set; }
        [JsonPropertyName("sport")]
        public string? Sport { get; set; }
        [JsonPropertyName("scoring_settings")]
        public ScoringSettings? ScoringSettings { get; set; }
        [JsonPropertyName("draft_id")]
        public string? DraftId { get; set; }
        [JsonPropertyName("roster_positions")]
        public string[]? RosterPositions { get; set; }
        [JsonPropertyName("total_rosters")]
        public int TotalRosters { get; set; }
    }
}