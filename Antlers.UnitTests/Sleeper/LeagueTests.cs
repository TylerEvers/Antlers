using Antlers.UnitTests.Sleeper.Mocks;
using Newtonsoft.Json;

namespace Antlers.UnitTests.Sleeper
{
    public class LeagueTests
    {
        [Fact]
        public void League_Serialization_ReturnsCorrectJson()
        {
            // Arrange
            var validLeague = new LeagueFaker().GenerateValidLeague();
            
            // Act
            var json = JsonConvert.SerializeObject(validLeague);


            // Assert
            string expectedJson = $"{{\"name\":\"{validLeague.Name}\"," +
                                    $"\"status\":\"{validLeague.Status}\"," +
                                    $"\"settings\":{{" +
                                        $"\"best_ball\":{validLeague.Settings?.BestBall}," +
                                        $"\"last_report\":{validLeague.Settings?.LastReport}," +
                                        $"\"waiver_budget\":{validLeague.Settings?.WaiverBudget}," +
                                        $"\"disable_adds\":{validLeague.Settings?.DisableAdds}," +
                                        $"\"capacity_override\":{validLeague.Settings?.CapacityOverride}," +
                                        $"\"waiver_bid_min\":{validLeague.Settings?.WaiverBidMin}," +
                                        $"\"taxi_deadline\":{validLeague.Settings?.TaxiDeadline}," +
                                        $"\"draft_rounds\":{validLeague.Settings?.DraftRounds}," +
                                        $"\"position_limit_lb\":{validLeague.Settings?.PositionLimitLb}," +
                                        $"\"reserve_allow_na\":{validLeague.Settings?.ReserveAllowNa}," +
                                        $"\"start_week\":{validLeague.Settings?.StartWeek}," +
                                        $"\"playoff_seed_type\":{validLeague.Settings?.PlayoffSeedType}," +
                                        $"\"playoff_teams\":{validLeague.Settings?.PlayoffTeams}," +
                                        $"\"veto_votes_needed\":{validLeague.Settings?.VetoVotesNeeded}," +
                                        $"\"squads\":{validLeague.Settings?.Squads}," +
                                        $"\"num_teams\":{validLeague.Settings?.NumTeams}," +
                                        $"\"daily_waivers_hour\":{validLeague.Settings?.DailyWaiversHour}," +
                                        $"\"playoff_type\":{validLeague.Settings?.PlayoffType}," +
                                        $"\"taxi_slots\":{validLeague.Settings?.TaxiSlots}," +
                                        $"\"sub_start_time_eligibility\":{validLeague.Settings?.SubStartTimeEligibility}," +
                                        $"\"last_scored_leg\":{validLeague.Settings?.LastScoredLeg}," +
                                        $"\"daily_waivers_days\":{validLeague.Settings?.DailyWaiversDays}," +
                                        $"\"sub_lock_if_starter_active\":{validLeague.Settings?.SubLockIfStarterActive}," +
                                        $"\"playoff_week_start\":{validLeague.Settings?.PlayoffWeekStart}," +
                                        $"\"position_limit_db\":{validLeague.Settings?.PositionLimitDb}," +
                                        $"\"waiver_clear_days\":{validLeague.Settings?.WaiverClearDays}," +
                                        $"\"reserve_allow_doubtful\":{validLeague.Settings?.ReserveAllowDoubtful}," +
                                        $"\"commissioner_direct_invite\":{validLeague.Settings?.CommissionerDirectInvite}," +
                                        $"\"veto_auto_poll\":{validLeague.Settings?.VetoAutoPoll}," +
                                        $"\"reserve_allow_dnr\":{validLeague.Settings?.ReserveAllowDnr}," +
                                        $"\"taxi_allow_vets\":{validLeague.Settings?.TaxiAllowVets}," +
                                        $"\"waiver_day_of_week\":{validLeague.Settings?.WaiverDayOfWeek}," +
                                        $"\"playoff_round_type\":{validLeague.Settings?.PlayoffRoundType}," +
                                        $"\"reserve_allow_out\":{validLeague.Settings?.ReserveAllowOut}," +
                                        $"\"reserve_allow_sus\":{validLeague.Settings?.ReserveAllowSus}," +
                                        $"\"veto_show_votes\":{validLeague.Settings?.VetoShowVotes}," +
                                        $"\"trade_deadline\":{validLeague.Settings?.TradeDeadline}," +
                                        $"\"taxi_years\":{validLeague.Settings?.TaxiYears}," +
                                        $"\"daily_waivers\":{validLeague.Settings?.DailyWaivers}," +
                                        $"\"faab_suggestions\":{validLeague.Settings?.FaabSuggestions}," +
                                        $"\"disable_trades\":{validLeague.Settings?.DisableTrades}," +
                                        $"\"pick_trading\":{validLeague.Settings?.PickTrading}," +
                                        $"\"type\":{validLeague.Settings?.Type}," +
                                        $"\"max_keepers\":{validLeague.Settings?.MaxKeepers}," +
                                        $"\"waiver_type\":{validLeague.Settings?.WaiverType}," +
                                        $"\"max_subs\":{validLeague.Settings?.MaxSubs}," +
                                        $"\"league_average_match\":{validLeague.Settings?.LeagueAverageMatch}," +
                                        $"\"position_limit_dl\":{validLeague.Settings?.PositionLimitDl}," +
                                        $"\"trade_review_days\":{validLeague.Settings?.TradeReviewDays}," +
                                        $"\"bench_lock\":{validLeague.Settings?.BenchLock}," +
                                        $"\"offseason_adds\":{validLeague.Settings?.OffseasonAdds}," +
                                        $"\"leg\":{validLeague.Settings?.Leg}," +
                                        $"\"daily_waivers_base\":{validLeague.Settings?.DailyWaiversBase}," +
                                        $"\"reserve_slots\":{validLeague.Settings?.ReserveSlots}," +
                                        $"\"reserve_allow_cov\":{validLeague.Settings?.ReserveAllowCov}," +
                                        $"\"daily_waivers_last_ran\":{validLeague.Settings?.DailyWaiversLastRan}" +
                                    "}," +
                                    $"\"avatar\":\"{validLeague.Avatar}\"," +
                                    $"\"season\":\"{validLeague.Season}\"," +
                                    $"\"season_type\":\"{validLeague.SeasonType}\"," +
                                    $"\"sport\":\"{validLeague.Sport}\"," +
                                    $"\"scoring_settings\":{{" +
                                        $"\"pass_yd\":{validLeague.ScoringSettings?.PassYd}," +
                                        $"\"pass_td\":{validLeague.ScoringSettings?.PassTd}," +
                                        $"\"pass_2pt\":{validLeague.ScoringSettings?.Pass2pt}," +
                                        $"\"bonus_pass_yd_300\":{validLeague.ScoringSettings?.BonusPassYd300}," +
                                        $"\"bonus_pass_yd_400\":{validLeague.ScoringSettings?.BonusPassYd400}," +
                                        $"\"pass_int\":{validLeague.ScoringSettings?.PassInt}," +
                                        $"\"rush_yd\":{validLeague.ScoringSettings?.RushYd}," +
                                        $"\"rush_td\":{validLeague.ScoringSettings?.RushTd}," +
                                        $"\"rush_2pt\":{validLeague.ScoringSettings?.Rush2pt}," +
                                        $"\"bonus_rush_yd_100\":{validLeague.ScoringSettings?.BonusRushYd100}," +
                                        $"\"bonus_rush_yd_200\":{validLeague.ScoringSettings?.BonusRushYd200}," +
                                        $"\"rec\":{validLeague.ScoringSettings?.Rec}," +
                                        $"\"rec_yd\":{validLeague.ScoringSettings?.RecYd}," +
                                        $"\"rec_td\":{validLeague.ScoringSettings?.RecTd}," +
                                        $"\"rec_2pt\":{validLeague.ScoringSettings?.Rec2pt}," +
                                        $"\"bonus_rec_yd_100\":{validLeague.ScoringSettings?.BonusRecYd100}," +
                                        $"\"bonus_rec_yd_200\":{validLeague.ScoringSettings?.BonusRecYd200}," +
                                        $"\"idp_int\":{validLeague.ScoringSettings?.IdpInt}," +
                                        $"\"idp_safe\":{validLeague.ScoringSettings?.IdpSafe}," +
                                        $"\"idp_def_td\":{validLeague.ScoringSettings?.IdpDefTd}," +
                                        $"\"idp_ff\":{validLeague.ScoringSettings?.IdpFf}," +
                                        $"\"idp_fum_rec\":{validLeague.ScoringSettings?.IdpFumRec}," +
                                        $"\"idp_tkl\":{validLeague.ScoringSettings?.IdpTackle}," +
                                        $"\"idp_tkl_ast\":{validLeague.ScoringSettings?.IdpTklAst}," +
                                        $"\"idp_tkl_solo\":{validLeague.ScoringSettings?.IdpTklSolo}," +
                                        $"\"idp_tkl_loss\":{validLeague.ScoringSettings?.IdpTklLoss}," +
                                        $"\"idp_qb_hit\":{validLeague.ScoringSettings?.IdpQbHit}," +
                                        $"\"idp_sack\":{validLeague.ScoringSettings?.IdpSack}," +
                                        $"\"idp_pass_def\":{validLeague.ScoringSettings?.IdpPassDef}," +
                                        $"\"idp_pass_def_3p\":{validLeague.ScoringSettings?.IdpPassDef3p}," +
                                        $"\"idp_blk_kick\":{validLeague.ScoringSettings?.IdpBlkKick}," +
                                        $"\"fum\":{validLeague.ScoringSettings?.Fum}," +
                                        $"\"ff\":{validLeague.ScoringSettings?.Ff}," +
                                        $"\"st_ff\":{validLeague.ScoringSettings?.StFf}," +
                                        $"\"def_st_ff\":{validLeague.ScoringSettings?.DefStFf}," +
                                        $"\"fum_lost\":{validLeague.ScoringSettings?.FumLost}," +
                                        $"\"fum_rec\":{validLeague.ScoringSettings?.FumRec}," +
                                        $"\"st_fum_rec\":{validLeague.ScoringSettings?.StFumRec}," +
                                        $"\"def_st_fum_rec\":{validLeague.ScoringSettings?.DefStFumRec}," +
                                        $"\"fum_rec_td\":{validLeague.ScoringSettings?.FumRecTd}," +
                                        $"\"pts_allow_0\":{validLeague.ScoringSettings?.PtsAllow0}," +
                                        $"\"pts_allow_1_6\":{validLeague.ScoringSettings?.PtsAllow1To6}," +
                                        $"\"pts_allow_7_13\":{validLeague.ScoringSettings?.PtsAllow7To13}," +
                                        $"\"pts_allow_14_20\":{validLeague.ScoringSettings?.PtsAllow14To20}," +
                                        $"\"pts_allow_21_27\":{validLeague.ScoringSettings?.PtsAllow21To27}," +
                                        $"\"pts_allow_28_34\":{validLeague.ScoringSettings?.PtsAllow28To34}," +
                                        $"\"pts_allow_35p\":{validLeague.ScoringSettings?.PtsAllow35p}," +
                                        $"\"int\":{validLeague.ScoringSettings?.Int}," +
                                        $"\"safe\":{validLeague.ScoringSettings?.Safe}," +
                                        $"\"sack\":{validLeague.ScoringSettings?.Sack}," +
                                        $"\"def_td\":{validLeague.ScoringSettings?.DefTd}," +
                                        $"\"st_td\":{validLeague.ScoringSettings?.StTd}," +
                                        $"\"def_st_td\":{validLeague.ScoringSettings?.DefStTd}," +
                                        $"\"blk_kick\":{validLeague.ScoringSettings?.BlkKick}," +
                                        $"\"bonus_tkl_10p\":{validLeague.ScoringSettings?.BonusTkl10p}," +
                                        $"\"xpm\":{validLeague.ScoringSettings?.Xpm}," +
                                        $"\"xpmiss\":{validLeague.ScoringSettings?.Xpmiss}," +
                                        $"\"fgm_0_19\":{validLeague.ScoringSettings?.Fgm0To19}," +
                                        $"\"fgm_20_29\":{validLeague.ScoringSettings?.Fgm20To29}," +
                                        $"\"fgm_30_39\":{validLeague.ScoringSettings?.Fgm30To39}," +
                                        $"\"fgm_40_49\":{validLeague.ScoringSettings?.Fgm40To49}," +
                                        $"\"fgm_50p\":{validLeague.ScoringSettings?.Fgm50p}," +
                                        $"\"fgmiss\":{validLeague.ScoringSettings?.Fgmiss}" +
                                    "}," +
                                    $"\"draft_id\":\"{validLeague.DraftId}\"," +
                                    $"\"league_id\":\"{validLeague.LeagueId}\"," +
                                    $"\"previous_league_id\":\"{validLeague.PreviousLeagueId}\"," +
                                    $"\"bracket_id\":{validLeague.BracketId}," +
                                    $"\"losers_bracket_id\":{validLeague.LosersBracketId}," +
                                    $"\"roster_positions\":{(validLeague.RosterPositions == null ? "" : $"[{string.Join(",", validLeague.RosterPositions.Select(rp => $"\"{rp}\""))}]")}," +
                                    $"\"total_rosters\":{validLeague.TotalRosters}}}";

            Assert.Equal(expectedJson, json);
        }
    }
}
