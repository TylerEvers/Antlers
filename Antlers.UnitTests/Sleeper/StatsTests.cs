using Antlers.UnitTests.Sleeper.Mocks;
using Newtonsoft.Json;

namespace Antlers.UnitTests.Sleeper
{
    public class StatsTests
    {
        [Fact]
        public void Stats_Serialization_ReturnsCorrectJson()
        {
            // Arrange
            var validStats = new StatsFaker().GenerateValidStats();
            
            // Act
            var json = JsonConvert.SerializeObject(validStats);

            // Assert
            string expectedJson = $"{{\"date\":\"{validStats.Date}\"," +
                $"\"stats\":{{" +
                    $"\"adp_2qb\":{validStats.StatsMetrics?.Adp2Qb}," +
                    $"\"adp_dynasty\":{validStats.StatsMetrics?.AdpDynasty}," +
                    $"\"adp_dynasty_2qb\":{validStats.StatsMetrics?.AdpDynasty2Qb}," +
                    $"\"adp_dynasty_half_ppr\":{validStats.StatsMetrics?.AdpDynastyHalfPpr}," +
                    $"\"adp_dynasty_ppr\":{validStats.StatsMetrics?.AdpDynastyPpr}," +
                    $"\"adp_dynasty_std\":{validStats.StatsMetrics?.AdpDynastyStd}," +
                    $"\"adp_half_ppr\":{validStats.StatsMetrics?.AdpHalfPpr}," +
                    $"\"adp_idp\":{validStats.StatsMetrics?.AdpIdp}," +
                    $"\"adp_ppr\":{validStats.StatsMetrics?.AdpPpr}," +
                    $"\"adp_rookie\":{validStats.StatsMetrics?.AdpRookie}," +
                    $"\"adp_std\":{validStats.StatsMetrics?.AdpStd}," +
                    $"\"rush_lng\":{validStats.StatsMetrics?.RushLng}," +
                    $"\"rush_td_lng\":{validStats.StatsMetrics?.RushTdLng}," +
                    $"\"pass_int\":{validStats.StatsMetrics?.PassInt}," +
                    $"\"pass_air_yd\":{validStats.StatsMetrics?.PassAirYd}," +
                    $"\"pos_rank_std\":{validStats.StatsMetrics?.PosRankStd}," +
                    $"\"gp\":{validStats.StatsMetrics?.GamesPlayed}," +
                    $"\"bonus_pass_yd_400\":{validStats.StatsMetrics?.BonusPassYd400}," +
                    $"\"rank_half_ppr\":{validStats.StatsMetrics?.RankHalfPpr}," +
                    $"\"tm_def_snp\":{validStats.StatsMetrics?.TeamDefSnp}," +
                    $"\"gms_active\":{validStats.StatsMetrics?.GamesActive}," +
                    $"\"penalty\":{validStats.StatsMetrics?.Penalty}," +
                    $"\"pass_sack_yds\":{validStats.StatsMetrics?.PassSackYds}," +
                    $"\"rank_std\":{validStats.StatsMetrics?.RankStd}," +
                    $"\"pos_rank_half_ppr\":{validStats.StatsMetrics?.PosRankHalfPpr}," +
                    $"\"rush_td\":{validStats.StatsMetrics?.RushTd}," +
                    $"\"pass_ypc\":{validStats.StatsMetrics?.PassYpc}," +
                    $"\"pass_lng\":{validStats.StatsMetrics?.PassLng}," +
                    $"\"pass_rush_yd\":{validStats.StatsMetrics?.PassRushYd}," +
                    $"\"pts_std\":{validStats.StatsMetrics?.PtsStd}," +
                    $"\"tm_st_snp\":{validStats.StatsMetrics?.TeamStSnp}," +
                    $"\"pass_rtg\":{validStats.StatsMetrics?.PassRtg}," +
                    $"\"bonus_pass_cmp_25\":{validStats.StatsMetrics?.BonusPassCmp25}," +
                    $"\"rush_fd\":{validStats.StatsMetrics?.RushFd}," +
                    $"\"pass_int_td\":{validStats.StatsMetrics?.PassIntTd}," +
                    $"\"pass_att\":{validStats.StatsMetrics?.PassAtt}," +
                    $"\"fum_lost\":{validStats.StatsMetrics?.FumLost}," +
                    $"\"rush_att\":{validStats.StatsMetrics?.RushAtt}," +
                    $"\"pass_rz_att\":{validStats.StatsMetrics?.PassRzAtt}," +
                    $"\"rush_ypa\":{validStats.StatsMetrics?.RushYpa}," +
                    $"\"gs\":{validStats.StatsMetrics?.Gs}," +
                    $"\"tm_off_snp\":{validStats.StatsMetrics?.TeamOffSnp}," +
                    $"\"pos_rank_ppr\":{validStats.StatsMetrics?.PosRankPpr}," +
                    $"\"rank_ppr\":{validStats.StatsMetrics?.RankPpr}," +
                    $"\"pass_sack\":{validStats.StatsMetrics?.PassSack}," +
                    $"\"penalty_yd\":{validStats.StatsMetrics?.PenaltyYd}," +
                    $"\"off_snp\":{validStats.StatsMetrics?.OffSnp}," +
                    $"\"pass_cmp\":{validStats.StatsMetrics?.PassCmp}," +
                    $"\"cmp_pct\":{validStats.StatsMetrics?.CmpPct}," +
                    $"\"rush_yac\":{validStats.StatsMetrics?.RushYac}," +
                    $"\"pts_half_ppr\":{validStats.StatsMetrics?.PtsHalfPpr}," +
                    $"\"pass_ypa\":{validStats.StatsMetrics?.PassYpa}," +
                    $"\"pass_cmp_40p\":{validStats.StatsMetrics?.PassCmp40p}," +
                    $"\"pass_fd\":{validStats.StatsMetrics?.PassFd}," +
                    $"\"pass_yd\":{validStats.StatsMetrics?.PassYd}," +
                    $"\"rush_rz_att\":{validStats.StatsMetrics?.RushRzAtt}," +
                    $"\"rush_rec_yd\":{validStats.StatsMetrics?.RushRecYd}," +
                    $"\"pass_td\":{validStats.StatsMetrics?.PassTd}," +
                    $"\"anytime_tds\":{validStats.StatsMetrics?.AnytimeTds}," +
                    $"\"bonus_fd_qb\":{validStats.StatsMetrics?.BonusFdQb}," +
                    $"\"rush_yd\":{validStats.StatsMetrics?.RushYd}," +
                    $"\"fum\":{validStats.StatsMetrics?.Fum}," +
                    $"\"pass_td_lng\":{validStats.StatsMetrics?.PassTdLng}," +
                    $"\"pts_ppr\":{validStats.StatsMetrics?.PtsPpr}," +
                    $"\"pass_inc\":{validStats.StatsMetrics?.PassInc}," +
                    $"\"rush_tkl_loss\":{validStats.StatsMetrics?.RushTklLoss}" +
                "}," +
                $"\"category\":\"{validStats.Category}\"," +
                $"\"last_modified\":{validStats.LastModified}," +
                $"\"week\":{validStats.Week}," +
                $"\"sport\":\"{validStats.Sport}\"," +
                $"\"season\":\"{validStats.Season}\"," +
                $"\"season_type\":\"{validStats.SeasonType}\"," +
                $"\"player_id\":\"{validStats.PlayerId}\"," +
                $"\"game_id\":\"{validStats.GameId}\"," +
                $"\"updated_at\":{validStats.UpdatedAt}," +
                $"\"team\":\"{validStats.Team}\"," +
                $"\"company\":\"{validStats.Company}\"," +
                $"\"opponent\":\"{validStats.Opponent}\"," +
                $"\"player\":{{" +
                    $"\"player_id\":\"{validStats.Player?.PlayerId}\"," +
                    $"\"first_name\":\"{validStats.Player?.FirstName}\"," +
                    $"\"last_name\":\"{validStats.Player?.LastName}\"," +
                    $"\"number\":{validStats.Player?.Number}," +
                    $"\"position\":\"{validStats.Player?.Position}\"," +
                    $"\"fantasy_positions\":[{string.Join(",", validStats.Player?.FantasyPositions.Select(fp => $"\"{fp}\""))}]," +
                    $"\"status\":\"{validStats.Player?.Status}\"," +
                    $"\"age\":{validStats.Player?.Age}," +
                    $"\"height\":\"{validStats.Player?.Height}\"," +
                    $"\"weight\":\"{validStats.Player?.Weight}\"," +
                    $"\"birth_date\":\"{validStats.Player?.BirthDate}\"," +
                    $"\"sport\":\"{validStats.Player?.Sport}\"," +
                    $"\"team\":\"{validStats.Player?.Team}\"," +
                    $"\"team_abbr\":\"{validStats.Player?.TeamAbbreviation}\"," +
                    $"\"college\":\"{validStats.Player?.College}\"," +
                    $"\"high_school\":\"{validStats.Player?.HighSchool}\"," +
                    $"\"depth_chart_position\":\"{validStats.Player?.DepthChartPosition}\"," +
                    $"\"depth_chart_order\":{validStats.Player?.DepthChartOrder}," +
                    $"\"years_exp\":{validStats.Player?.YearsExperience}," +
                    $"\"practice_participation\":{validStats.Player?.PracticeParticipation?.ToString().ToLower()}," +
                    $"\"injury_start_date\":\"{validStats.Player?.InjuryStartDate}\"," +
                    $"\"injury_status\":\"{validStats.Player?.InjuryStatus?.ToString()}\"," +
                    $"\"injury_body_part\":\"{validStats.Player?.InjuryBodyPart}\"," +
                    $"\"injury_notes\":\"{validStats.Player?.InjuryNotes}\"," +
                    $"\"search_rank\":{validStats.Player?.SearchRank}," +
                    $"\"search_full_name\":\"{validStats.Player?.SearchFullName}\"," +
                    $"\"search_first_name\":\"{validStats.Player?.SearchFirstName}\"," +
                    $"\"search_last_name\":\"{validStats.Player?.SearchLastName}\"," +
                    $"\"hashtag\":\"{validStats.Player?.Hashtag}\"," +
                    $"\"fantasy_data_id\":{validStats.Player?.FantasyDataId}," +
                    $"\"stats_id\":{validStats.Player?.StatsId}," +
                    $"\"sportradar_id\":\"{validStats.Player?.SportRadarId}\"," +
                    $"\"espn_id\":{validStats.Player?.EspnId}," +
                    $"\"rotowire_id\":{validStats.Player?.RotowireId}," +
                    $"\"rotoworld_id\":{validStats.Player?.RotoworldId}," +
                    $"\"yahoo_id\":{validStats.Player?.YahooId}," +
                    $"\"oddsjam_id\":\"{validStats.Player?.OddsJamId}\"," +
                    $"\"pandascore_id\":\"{validStats.Player?.PandaScoreId}\"," +
                    $"\"gsis_id\":\"{validStats.Player?.GsisId}\"," +
                    "\"metadata\":{" +
                        $"\"channel_id\":\"{validStats.Player?.Metadata?.ChannelId}\"," +
                        $"\"rookie_year\":\"{validStats.Player?.Metadata?.RookieYear}\"}}" +
                    "}}";

            Assert.Equal(expectedJson, json);
        }
    }
}
