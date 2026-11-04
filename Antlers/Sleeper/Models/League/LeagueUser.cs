using Newtonsoft.Json;

namespace Antlers.Sleeper.Models;

public sealed class LeagueUser
{
    [JsonProperty("user_id")]
    public string? UserId { get; set; }

    [JsonProperty("display_name")]
    public string? DisplayName { get; set; }

    [JsonProperty("is_owner")]
    public bool IsOwner { get; set; }

    [JsonProperty("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }
}
