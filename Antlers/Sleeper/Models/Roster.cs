namespace Antlers.Sleeper.Models
{
    public class Roster
    {
        public int roster_id { get; set; }
        public int[]? players { get; set; }
        public int[]? starters { get; set; }
        public int[]? reserve { get; set; }
        public Settings settings { get; set; } = new Settings();
        public string? owner_id { get; set; }
        public string? league_id { get; set; }

        public class Settings
        {
            public int wins { get; set; }
            public int waiver_position { get; set; }
            public int waiver_budget_used { get; set; }
            public int total_moves { get; set; }
            public int ties { get; set; }
            public int losses { get; set; }
            public int fpts { get; set; }
            public int fpts_decimal { get; set; }
            public int fpts_against { get; set; }
            public int fpts_against_decimal { get; set; }
        }
    }
}
