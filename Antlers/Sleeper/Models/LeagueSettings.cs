using System.Text.Json.Serialization;

namespace Antlers.Sleeper.Models
{
    public class LeagueSettings
    {
        [JsonPropertyName("best_ball")]
        public int BestBall { get; set; }
        [JsonPropertyName("last_report")]
        public int LastReport { get; set; }
        [JsonPropertyName("waiver_budget")]
        public int WaiverBudget { get; set; }
        [JsonPropertyName("disable_adds")]
        public int DisableAdds { get; set; }
        [JsonPropertyName("capacity_override")]
        public int CapacityOverride { get; set; }
        [JsonPropertyName("waiver_bid_min")]
        public int WaiverBidMin { get; set; }
        [JsonPropertyName("taxi_deadline")]
        public int TaxiDeadline { get; set; }
        [JsonPropertyName("draft_rounds")]
        public int DraftRounds { get; set; }
        [JsonPropertyName("position_limit_lb")]
        public int PositionLimitLb { get; set; }
        [JsonPropertyName("reserve_allow_na")]
        public int ReserveAllowNa { get; set; }
        [JsonPropertyName("start_week")]
        public int StartWeek { get; set; }
        [JsonPropertyName("playoff_seed_type")]
        public int PlayoffSeedType { get; set; }
        [JsonPropertyName("playoff_teams")]
        public int PlayoffTeams { get; set; }
        [JsonPropertyName("veto_votes_needed")]
        public int VetoVotesNeeded { get; set; }
        [JsonPropertyName("squads")]
        public int Squads { get; set; }
        [JsonPropertyName("num_teams")]
        public int NumTeams { get; set; }
        [JsonPropertyName("daily_waivers_hour")]
        public int DailyWaiversHour { get; set; }
        [JsonPropertyName("playoff_type")]
        public int PlayoffType { get; set; }
        [JsonPropertyName("taxi_slots")]
        public int TaxiSlots { get; set; }
        [JsonPropertyName("sub_start_time_eligibility")]
        public int SubStartTimeEligibility { get; set; }
        [JsonPropertyName("last_scored_leg")]
        public int LastScoredLeg { get; set; }
        [JsonPropertyName("daily_waivers_days")]
        public int DailyWaiversDays { get; set; }
        [JsonPropertyName("sub_lock_if_starter_active")]
        public int SubLockIfStarterActive { get; set; }
        [JsonPropertyName("playoff_week_start")]
        public int PlayoffWeekStart { get; set; }
        [JsonPropertyName("position_limit_db")]
        public int PositionLimitDb { get; set; }
        [JsonPropertyName("waiver_clear_days")]
        public int WaiverClearDays { get; set; }
        [JsonPropertyName("reserve_allow_doubtful")]
        public int ReserveAllowDoubtful { get; set; }
        [JsonPropertyName("commissioner_direct_invite")]
        public int CommissionerDirectInvite { get; set; }
        [JsonPropertyName("veto_auto_poll")]
        public int VetoAutoPoll { get; set; }
        [JsonPropertyName("reserve_allow_dnr")]
        public int ReserveAllowDnr { get; set; }
        [JsonPropertyName("taxi_allow_vets")]
        public int TaxiAllowVets { get; set; }
        [JsonPropertyName("waiver_day_of_week")]
        public int WaiverDayOfWeek { get; set; }
        [JsonPropertyName("playoff_round_type")]
        public int PlayoffRoundType { get; set; }
        [JsonPropertyName("reserve_allow_out")]
        public int ReserveAllowOut { get; set; }
        [JsonPropertyName("reserve_allow_sus")]
        public int ReserveAllowSus { get; set; }
        [JsonPropertyName("veto_show_votes")]
        public int VetoShowVotes { get; set; }
        [JsonPropertyName("trade_deadline")]
        public int TradeDeadline { get; set; }
        [JsonPropertyName("taxi_years")]
        public int TaxiYears { get; set; }
        [JsonPropertyName("daily_waivers")]
        public int DailyWaivers { get; set; }
        [JsonPropertyName("faab_suggestions")]
        public int FaabSuggestions { get; set; }
        [JsonPropertyName("disable_trades")]
        public int DisableTrades { get; set; }
        [JsonPropertyName("pick_trading")]
        public int PickTrading { get; set; }
        [JsonPropertyName("type")]
        public int Type { get; set; }
        [JsonPropertyName("max_keepers")]
        public int MaxKeepers { get; set; }
        [JsonPropertyName("waiver_type")]
        public int WaiverType { get; set; }
        [JsonPropertyName("max_subs")]
        public int MaxSubs { get; set; }
        [JsonPropertyName("league_average_match")]
        public int LeagueAverageMatch { get; set; }
        [JsonPropertyName("position_limit_dl")]
        public int PositionLimitDl { get; set; }
        [JsonPropertyName("trade_review_days")]
        public int TradeReviewDays { get; set; }
        [JsonPropertyName("bench_lock")]
        public int BenchLock { get; set; }
        [JsonPropertyName("offseason_adds")]
        public int OffseasonAdds { get; set; }
        [JsonPropertyName("leg")]
        public int Leg { get; set; }
        [JsonPropertyName("daily_waivers_base")]
        public int DailyWaiversBase { get; set; }
        [JsonPropertyName("reserve_slots")]
        public int ReserveSlots { get; set; }
        [JsonPropertyName("reserve_allow_cov")]
        public int ReserveAllowCov { get; set; }
        [JsonPropertyName("daily_waivers_last_ran")]
        public int DailyWaiversLastRan { get; set; }
    }
}
