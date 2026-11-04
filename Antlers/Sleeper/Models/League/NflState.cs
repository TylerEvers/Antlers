using Newtonsoft.Json;

namespace Antlers.Sleeper.Models;

public sealed class NflState
{
    [JsonProperty("season")]
    public string? Season { get; set; }

    [JsonProperty("season_type")]
    public string? SeasonType { get; set; }

    [JsonProperty("week")]
    public int Week { get; set; }
}
