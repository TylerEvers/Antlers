using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class Market
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("eventId")]
        public string? EventId { get; set; }
        [JsonProperty("sportId")]
        public string? SportId { get; set; }
        [JsonProperty("leagueId")]
        public string? LeagueId { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("subcategoryId")]
        public int SubCategoryId { get; set; }
        [JsonProperty("marketType")]
        public MarketType? MarketType { get; set; }
        [JsonProperty("subscriptionKey")]
        public string? SubscriptionKey { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
        [JsonProperty("tags")]
        public string[]? Tags { get; set; }

        //Currently no response examples utilizing this property 
        //[JsonProperty("dynamicMetadata")]
        //public DynamicMetadata? DynamicMetadata { get; set; }
    }
}
