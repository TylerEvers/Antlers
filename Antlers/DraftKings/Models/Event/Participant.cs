using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class Participant
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
        [JsonProperty("metadata")]
        public ParticipantMetadata? Metadata { get; set; }
    }

    public class ParticipantMetadata
    {
        [JsonProperty("rosettaTeamId")]
        public string? RosettaTeamId { get; set; }
        [JsonProperty("shortName")]
        public string? ShortName { get; set; }
        [JsonProperty("teamColor")]
        public string? TeamColor { get; set; }
    }
}
