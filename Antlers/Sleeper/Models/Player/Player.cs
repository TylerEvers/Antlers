using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class Player
    {
        [JsonProperty("player_id")]
        public string? PlayerId { get; set; }
        [JsonProperty("first_name")]
        public string? FirstName { get; set; }
        [JsonProperty("last_name")]
        public string? LastName { get; set; }
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("position")]
        public string? Position { get; set; }
        [JsonProperty("fantasy_positions")]
        public string[]? FantasyPositions { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("height")]
        public string? Height { get; set; }
        [JsonProperty("weight")]
        public string? Weight { get; set; }
        [JsonProperty("birth_date")]
        public string? BirthDate { get; set; }
        [JsonProperty("sport")]
        public string? Sport { get; set; }
        [JsonProperty("team")]
        public string? Team { get; set; }
        [JsonProperty("team_abbr")]
        public string? TeamAbbreviation { get; set; }
        [JsonProperty("college")]
        public string? College { get; set; }
        [JsonProperty("high_school")]
        public string? HighSchool { get; set; }
        [JsonProperty("depth_chart_position")]
        public string? DepthChartPosition { get; set; }
        [JsonProperty("depth_chart_order")]
        public int DepthChartOrder { get; set; }
        [JsonProperty("years_exp")]
        public int YearsExperience { get; set; }
        [JsonProperty("practice_participation")]
        public bool? PracticeParticipation { get; set; }
        [JsonProperty("injury_start_date")]
        public string? InjuryStartDate { get; set; }
        [JsonProperty("injury_status")]
        public bool? InjuryStatus { get; set; }
        [JsonProperty("injury_body_part")]
        public string? InjuryBodyPart { get; set; }
        [JsonProperty("injury_notes")]
        public string? InjuryNotes { get; set; }
        [JsonProperty("search_rank")]
        public int SearchRank { get; set; }
        [JsonProperty("search_full_name")]
        public string? SearchFullName { get; set; }
        [JsonProperty("search_first_name")]
        public string? SearchFirstName { get; set; }
        [JsonProperty("search_last_name")]
        public string? SearchLastName { get; set; }
        [JsonProperty("hashtag")]
        public string? Hashtag { get; set; }
        [JsonProperty("fantasy_data_id")]
        public int? FantasyDataId { get; set; }
        [JsonProperty("stats_id")]
        public int? StatsId { get; set; }
        [JsonProperty("sportradar_id")]
        public string? SportRadarId { get; set; }
        [JsonProperty("espn_id")]
        public int? EspnId { get; set; }
        [JsonProperty("rotowire_id")]
        public int RotowireId { get; set; }
        [JsonProperty("rotoworld_id")]
        public int RotoworldId { get; set; }
        [JsonProperty("yahoo_id")]
        public int? YahooId { get; set; }
        [JsonProperty("oddsjam_id")]
        public string? OddsJamId { get; set; }
        [JsonProperty("pandascore_id")]
        public string? PandaScoreId { get; set; }
        [JsonProperty("gsis_id")]
        public string? GsisId { get; set; }
        [JsonProperty("metadata")]
        public PlayerMetadata? Metadata { get; set; }
    }
}
