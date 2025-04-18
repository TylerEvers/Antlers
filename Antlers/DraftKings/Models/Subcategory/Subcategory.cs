using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class Subcategory
    {
        [JsonProperty("id")]
        public required int Id { get; set; }
        [JsonProperty("categoryId")]
        public required int CategoryId { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("componentId")]
        public int? ComponentId { get; set; }
        [JsonProperty("sortOrder")]
        public int? SortOrder { get; set; }
        [JsonProperty("tags")]
        public object[]? Tags { get; set; }
    }
}
