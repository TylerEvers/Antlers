using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class MarketType
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("betOfferTypeId")]
        public int BetOfferTypeId { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}
