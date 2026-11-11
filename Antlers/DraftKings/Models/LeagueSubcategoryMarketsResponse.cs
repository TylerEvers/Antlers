using Newtonsoft.Json;

namespace Antlers.DraftKings.Models;

public class LeagueSubcategoryMarketsResponse
{
    [JsonProperty("markets")]
    [JsonConverter(typeof(EntityCollectionConverter<Market>))]
    public Market[] Markets { get; set; } = [];

    [JsonProperty("events")]
    [JsonConverter(typeof(EntityCollectionConverter<Event>))]
    public Event[] Events { get; set; } = [];

    [JsonProperty("selections")]
    [JsonConverter(typeof(EntityCollectionConverter<Selection>))]
    public Selection[] Selections { get; set; } = [];

    [JsonIgnore]
    public string RawPayloadJson { get; set; } = string.Empty;
}
