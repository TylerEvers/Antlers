using Newtonsoft.Json;

namespace Antlers.Caesars.Models
{
    public class Selection
    {
        [JsonProperty("id")]
        public required string Id { get; set; }
        [JsonProperty("marketId")]
        public required string MarketId { get; set; }
        [JsonProperty("marketId")]
        public required string MarketId { get; set; }
        [JsonProperty("label")]
        public string? Label { get; set; }
        [JsonProperty("displayOdds")]
        public DisplayOdd? DisplayOdds { get; set; }
        [JsonProperty("trueOdds")]
        public int? TrueOdds { get; set; }
        [JsonProperty("outcomeType")]
        public string? OutcomeType { get; set; }
        [JsonProperty("participants")]
        public Participant[]? Participants { get; set; }
        [JsonProperty("sortOrder")]
        public int? SortOrder { get; set; }
        [JsonProperty("metadata")]
        public SelectionMetadata? Metadata { get; set; }
    }
}
