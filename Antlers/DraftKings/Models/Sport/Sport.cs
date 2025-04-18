using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class Sport
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("seoIdentifier")]
        public string? SEOIdentifier { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
    }
}
