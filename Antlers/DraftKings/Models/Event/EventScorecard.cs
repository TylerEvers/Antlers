using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class EventScorecard
    {
        [JsonProperty("scorecardComponentId")]
        public int ScorecardComponentId { get; set; }
    }
}
