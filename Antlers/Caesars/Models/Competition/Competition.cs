using Newtonsoft.Json;

namespace Antlers.Caesars.Models
{
    public class Competition
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("collectionName")]
        public string? collectionName { get; set; }
        [JsonProperty("events")]
        public Event[]? Events { get; set; }
        [JsonProperty("displayAsEvent")]
        public bool? DisplayAsEvent { get; set; }
        [JsonProperty("collectionDisplayOrder")]
        public int? CollectionDisplayOrder { get; set; }
        [JsonProperty("displayOrder")]
        public int? DisplayOrder { get; set; }
    }
}
