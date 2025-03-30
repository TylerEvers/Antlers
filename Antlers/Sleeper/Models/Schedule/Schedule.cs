using Newtonsoft.Json;

namespace Antlers.Sleeper.Models
{
    public class Schedule
    {
        [JsonProperty("game_id")]
        public string? GameId { get; set; }

        [JsonProperty("week")]
        public int Week { get; set; }

        [JsonProperty("date")]
        public string? Date { get; set; }

        [JsonProperty("home")]
        public string? Home { get; set; }

        [JsonProperty("away")]
        public string? Away { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }
}
