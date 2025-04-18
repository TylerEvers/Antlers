using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public required string Id { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
    }
}
