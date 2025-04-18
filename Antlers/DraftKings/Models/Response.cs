using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class Response
    {
        [JsonProperty("sports")]
        public Sport? Sports { get; set; }
        [JsonProperty("leagues")]
        public League? Leagues { get; set; }
        [JsonProperty("events")]
        public Event? Events { get; set; }
        [JsonProperty("markets")]
        public Market? Markets { get; set; }
        [JsonProperty("selections")]
        public Selection? Selections { get; set; }
        [JsonProperty("categories")]
        public Category? Categories { get; set; }
        [JsonProperty("subcategories")]
        public Subcategory? Subcategories { get; set; }
    }
}
