namespace Antlers.Sleeper.Models
{
    public class Player
    {
        public string? player_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public int number { get; set; }
        public string? position { get; set; }
        public string[]? fantasy_positions { get; set; }
        public string? status { get; set; }
        public int age { get; set; }
        public string? height { get; set; }
        public string? weight { get; set; }
        public string? birth_country { get; set; }
        public string? sport { get; set; }
        public string? team { get; set; }
        public string? college { get; set; }
        public int depth_chart_position { get; set; }
        public int depth_chart_order { get; set; }
        public int years_exp { get; set; }
        public DateTime? injury_start_date { get; set; }
        public bool? practice_participation { get; set; }
        public bool? injury_status { get; set; }
        public int search_rank { get; set; }
        public string? search_full_name { get; set; }
        public string? search_first_name { get; set; }
        public string? search_last_name { get; set; }
        public string? hashtag { get; set; }
        public int? fantasy_data_id { get; set; }
        public string? stats_id { get; set; }
        public string? sportradar_id { get; set; }
        public string? espn_id { get; set; }
        public int rotowire_id { get; set; }
        public int rotoworld_id { get; set; }
        public int? yahoo_id { get; set; }
    }

    public class Response
    {
        public Dictionary<int, Player> Players { get; set; } = new Dictionary<int, Player>();
    }
}
