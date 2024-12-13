using Newtonsoft.Json;

namespace Antlers.Sleeper.Models.League
{
    public class LeagueSettings
    {
        [JsonProperty("best_ball")]
        public int BestBall { get; set; }
        [JsonProperty("last_report")]
        public int LastReport { get; set; }
        [JsonProperty("waiver_budget")]
        public int WaiverBudget { get; set; }
        [JsonProperty("disable_adds")]
        public int DisableAdds { get; set; }
        [JsonProperty("capacity_override")]
        public int CapacityOverride { get; set; }
        [JsonProperty("waiver_bid_min")]
        public int WaiverBidMin { get; set; }
        [JsonProperty("taxi_deadline")]
        public int TaxiDeadline { get; set; }
        [JsonProperty("draft_rounds")]
        public int DraftRounds { get; set; }
        [JsonProperty("position_limit_lb")]
        public int PositionLimitLb { get; set; }
        [JsonProperty("reserve_allow_na")]
        public int ReserveAllowNa { get; set; }
        [JsonProperty("start_week")]
        public int StartWeek { get; set; }
        [JsonProperty("playoff_seed_type")]
        public int PlayoffSeedType { get; set; }
        [JsonProperty("playoff_teams")]
        public int PlayoffTeams { get; set; }
        [JsonProperty("veto_votes_needed")]
        public int VetoVotesNeeded { get; set; }
        [JsonProperty("squads")]
        public int Squads { get; set; }
        [JsonProperty("num_teams")]
        public int NumTeams { get; set; }
        [JsonProperty("daily_waivers_hour")]
        public int DailyWaiversHour { get; set; }
        [JsonProperty("playoff_type")]
        public int PlayoffType { get; set; }
        [JsonProperty("taxi_slots")]
        public int TaxiSlots { get; set; }
        [JsonProperty("sub_start_time_eligibility")]
        public int SubStartTimeEligibility { get; set; }
        [JsonProperty("last_scored_leg")]
        public int LastScoredLeg { get; set; }
        [JsonProperty("daily_waivers_days")]
        public int DailyWaiversDays { get; set; }
        [JsonProperty("sub_lock_if_starter_active")]
        public int SubLockIfStarterActive { get; set; }
        [JsonProperty("playoff_week_start")]
        public int PlayoffWeekStart { get; set; }
        [JsonProperty("position_limit_db")]
        public int PositionLimitDb { get; set; }
        [JsonProperty("waiver_clear_days")]
        public int WaiverClearDays { get; set; }
        [JsonProperty("reserve_allow_doubtful")]
        public int ReserveAllowDoubtful { get; set; }
        [JsonProperty("commissioner_direct_invite")]
        public int CommissionerDirectInvite { get; set; }
        [JsonProperty("veto_auto_poll")]
        public int VetoAutoPoll { get; set; }
        [JsonProperty("reserve_allow_dnr")]
        public int ReserveAllowDnr { get; set; }
        [JsonProperty("taxi_allow_vets")]
        public int TaxiAllowVets { get; set; }
        [JsonProperty("waiver_day_of_week")]
        public int WaiverDayOfWeek { get; set; }
        [JsonProperty("playoff_round_type")]
        public int PlayoffRoundType { get; set; }
        [JsonProperty("reserve_allow_out")]
        public int ReserveAllowOut { get; set; }
        [JsonProperty("reserve_allow_sus")]
        public int ReserveAllowSus { get; set; }
        [JsonProperty("veto_show_votes")]
        public int VetoShowVotes { get; set; }
        [JsonProperty("trade_deadline")]
        public int TradeDeadline { get; set; }
        [JsonProperty("taxi_years")]
        public int TaxiYears { get; set; }
        [JsonProperty("daily_waivers")]
        public int DailyWaivers { get; set; }
        [JsonProperty("faab_suggestions")]
        public int FaabSuggestions { get; set; }
        [JsonProperty("disable_trades")]
        public int DisableTrades { get; set; }
        [JsonProperty("pick_trading")]
        public int PickTrading { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("max_keepers")]
        public int MaxKeepers { get; set; }
        [JsonProperty("waiver_type")]
        public int WaiverType { get; set; }
        [JsonProperty("max_subs")]
        public int MaxSubs { get; set; }
        [JsonProperty("league_average_match")]
        public int LeagueAverageMatch { get; set; }
        [JsonProperty("position_limit_dl")]
        public int PositionLimitDl { get; set; }
        [JsonProperty("trade_review_days")]
        public int TradeReviewDays { get; set; }
        [JsonProperty("bench_lock")]
        public int BenchLock { get; set; }
        [JsonProperty("offseason_adds")]
        public int OffseasonAdds { get; set; }
        [JsonProperty("leg")]
        public int Leg { get; set; }
        [JsonProperty("daily_waivers_base")]
        public int DailyWaiversBase { get; set; }
        [JsonProperty("reserve_slots")]
        public int ReserveSlots { get; set; }
        [JsonProperty("reserve_allow_cov")]
        public int ReserveAllowCov { get; set; }
        [JsonProperty("daily_waivers_last_ran")]
        public int DailyWaiversLastRan { get; set; }
    }
}
