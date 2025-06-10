using Newtonsoft.Json;

namespace Antlers.Caesars.Models
{
    public class Event
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
        [JsonProperty("sportId")]
        public string? SportId { get; set; }
        [JsonProperty("competitionName")]
        public string? CompetitionName { get; set; }
        [JsonProperty("competitionId")]
        public string? CompetitionId { get; set; }
        [JsonProperty("display")]
        public bool? Display { get; set; }
        [JsonProperty("tradedInPlay")]
        public bool? TradingInPlay { get; set; }
        [JsonProperty("active")]
        public bool? Active { get; set; }
        [JsonProperty("started")]
        public bool? Started { get; set; }
        [JsonProperty("byoEligible")]
        public bool? ByoEligible { get; set; }
        //[JsonProperty("keyMarketGroups")]
        //public KeyMarketGroup[]? KeyMarketGroups { get; set; }
        [JsonProperty("marketCountActivePreMatch")]
        public int? MarketCountActivePreMatch { get; set; }
        [JsonProperty("marketCountActiveInPlay")]
        public int? MarketCountActiveInPlay { get; set; }
        [JsonProperty("markets")]
        public Market[]? Markets { get; set; }
    }
}
