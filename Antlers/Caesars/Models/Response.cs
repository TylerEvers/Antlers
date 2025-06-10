using Newtonsoft.Json;

namespace Antlers.Caesars.Models
{
    public class Response
    {
        [JsonProperty("sports")]
        public string? SportId { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("groupedCompetitionsView")]
        public bool? GroupedCompetitionsView { get; set; }
        [JsonProperty("selectedTabId")]
        public string? SelectedTabId { get; set; }
        [JsonProperty("tabs")]
        public Tab[]? Tabs { get; set; }
        [JsonProperty("competitions")]
        public Competition[]? Competitions { get; set; }
        [JsonProperty("displayOrder")]
        public int? DisplayOrder { get; set; }
        [JsonProperty("eventCount")]
        public int? EventCount { get; set; }
    }
}
