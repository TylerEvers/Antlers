using Newtonsoft.Json;

namespace Antlers.Caesars.Models
{
    public class Tab
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
        [JsonProperty("dataPath")]
        public string? DataPath { get; set; }
    }
}
