using Newtonsoft.Json;

namespace Antlers.Sleeper.Models.Roster
{
    public class RosterSettings
    {
        [JsonProperty("wins")]
        public int Wins { get; set; }
        [JsonProperty("waiver_position")]
        public int WaiverPosition { get; set; }
        [JsonProperty("waiver_budget_used")]
        public int WaiverBudgetUsed { get; set; }
        [JsonProperty("total_moves")]
        public int TotalMoves { get; set; }
        [JsonProperty("ties")]
        public int Ties { get; set; }
        [JsonProperty("losses")]
        public int Losses { get; set; }
        [JsonProperty("fpts")]
        public int FantasyPoints { get; set; }
        [JsonProperty("fpts_decimal")]
        public int FantasyPointsDecimal { get; set; }
        [JsonProperty("fpts_against")]
        public int FantasyPointsAgainst { get; set; }
        [JsonProperty("fpts_against_decimal")]
        public int FantasyPointsAgainstDecimal { get; set; }
    }
}
