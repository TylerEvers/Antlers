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
        [JsonProperty("blurbId")]
        public string? BlurbId { get; set; }
        [JsonProperty("dynamicMetadata")]
        public DynamicMetadata? DynamicMetadata { get; set; }
        [JsonProperty("clientMetadata")]
        public MarketClientMetadata? ClientMetadata { get; set; }
        [JsonProperty("selections")]
        [JsonConverter(typeof(EntityCollectionConverter<Selection>))]
        public Selection[]? Selections { get; set; }
    }

    public class MarketType
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("betOfferTypeId")]
        public int BetOfferTypeId { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public class DynamicMetadata
    {
    }

    public class MarketClientMetadata
    {
        [JsonProperty("subCategoryId")]
        public int? SubCategoryId { get; set; }
    }
}
