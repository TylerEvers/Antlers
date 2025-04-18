using Newtonsoft.Json;

namespace Antlers.DraftKings.Models
{
    public class Event
    {
        [JsonProperty("id")]
        public required string Id { get; set; }
        [JsonProperty("seoIdentifier")]
        public string? SEOIdentifier { get; set; }
        [JsonProperty("sportId")]
        public string? SportId { get; set; }
        [JsonProperty("leagueId")]
        public string? LeagueId { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("startEventDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("participants")]
        public Participant[]? Participants { get; set; }
        [JsonProperty("eventParticipantType")]
        public string? ParticipantType { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("eventScorecard")]
        public EventScorecard? EventScorecard { get; set; }
        [JsonProperty("metadata")]
        public EventMetadata? Metadata { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
        [JsonProperty("subscriptionKey")]
        public string? SubscriptionKey { get; set; }
    }

    public class EventScorecard
    {
        [JsonProperty("scorecardComponentId")]
        public int ScorecardComponentId { get; set; }
    }

    public class EventMetadata
    {
        [JsonProperty("settlementDate")]
        public DateTime? SettlementDate { get; set; }
    }
}
