namespace Antlers.Sleeper.Models
{
    public class League
    {
        public int totalRosters { get; set; }
        public string? status { get; set; }
        public string? sport { get; set; }
        //public Settings settings { get; set; } //TODO: map object from response, not in API doc
        public string? seasonType { get; set; }
        public string? season { get; set; }
        //public ScoringSettings scoring_settings { get; set; } //TODO: map object from response, not in API doc
        //public RosterPositions roster_positions { get; set; } //TODO: map object from response, not in API doc
        public string? previous_league_id { get; set; }
        public string? name { get; set; }
        public string? league_id { get; set; }
        public string? draft_id { get; set; }
        public string? avatar { get; set; }
    }
}
