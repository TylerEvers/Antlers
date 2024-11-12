using System.Text.Json.Serialization;

namespace Antlers.Sleeper.Models
{
    public class Player
    {
        [JsonPropertyName("player_id")]
        public string? PlayerId { get; set; }
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }
        [JsonPropertyName("number")]
        public int Number { get; set; }
        [JsonPropertyName("position")]
        public string? Position { get; set; }
        [JsonPropertyName("fantasy_positions")]
        public string[]? FantasyPositions { get; set; }
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("height")]
        public string? Height { get; set; }
        [JsonPropertyName("weight")]
        public string? Weight { get; set; }
        [JsonPropertyName("birth_date")]
        public string? BirthDate { get; set; }
        [JsonPropertyName("sport")]
        public string? Sport { get; set; }
        [JsonPropertyName("team")]
        public string? Team { get; set; }
        [JsonPropertyName("college")]
        public string? College { get; set; }
        [JsonPropertyName("high_school")]
        public string? HighSchool { get; set; }
        [JsonPropertyName("depth_chart_position")]
        public string? DepthChartPosition { get; set; }
        [JsonPropertyName("depth_chart_order")]
        public int DepthChartOrder { get; set; }
        [JsonPropertyName("years_exp")]
        public int YearsExperience { get; set; }
        [JsonPropertyName("injury_start_date")]
        public DateTime? InjuryStartDate { get; set; }
        [JsonPropertyName("practice_participation")]
        public bool? PracticeParticipation { get; set; }
        [JsonPropertyName("injury_status")]
        public bool? InjuryStatus { get; set; }
        [JsonPropertyName("search_rank")]
        public int SearchRank { get; set; }
        [JsonPropertyName("search_full_name")]
        public string? SearchFullName { get; set; }
        [JsonPropertyName("search_first_name")]
        public string? SearchFirstName { get; set; }
        [JsonPropertyName("search_last_name")]
        public string? SearchLastName { get; set; }
        [JsonPropertyName("hashtag")]
        public string? Hashtag { get; set; }
        [JsonPropertyName("fantasy_data_id")]
        public int? FantasyDataId { get; set; }
        [JsonPropertyName("stats_id")]
        public string? StatsId { get; set; }
        [JsonPropertyName("sportradar_id")]
        public string? SportRadarId { get; set; }
        [JsonPropertyName("espn_id")]
        public string? EspnId { get; set; }
        [JsonPropertyName("rotowire_id")]
        public int RotowireId { get; set; }
        [JsonPropertyName("rotoworld_id")]
        public int RotoworldId { get; set; }
        [JsonPropertyName("yahoo_id")]
        public int? YahooId { get; set; }
    }

    public class Response
    {
        public Dictionary<int, Player> Players { get; set; } = new Dictionary<int, Player>();
    }
}
