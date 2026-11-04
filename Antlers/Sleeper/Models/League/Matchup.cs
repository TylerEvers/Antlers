using Newtonsoft.Json;

namespace Antlers.Sleeper.Models;

public sealed class Matchup
{
    [JsonProperty("roster_id")]
    public int RosterId { get; set; }

    [JsonProperty("matchup_id")]
    public int? MatchupId { get; set; }

    [JsonProperty("points")]
    public decimal Points { get; set; }
}
