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
            var validStats = new StatsResponseFaker().GenerateValidStatsResponse();
            
            // Act
            var json = JsonConvert.SerializeObject(validStats);

            // Assert
            string expectedJson = $"{{\"date\":\"{validStats.Date}\"," +
                $"\"stats\":{{" +
                    $"\"adp_2qb\":{validStats.Stats?.Adp2Qb}," +
                    $"\"adp_dynasty\":{validStats.Stats?.AdpDynasty}," +
                    $"\"adp_dynasty_2qb\":{validStats.Stats?.AdpDynasty2Qb}," +
                    $"\"adp_dynasty_half_ppr\":{validStats.Stats?.AdpDynastyHalfPpr}," +
                    $"\"adp_dynasty_ppr\":{validStats.Stats?.AdpDynastyPpr}," +
                    $"\"adp_dynasty_std\":{validStats.Stats?.AdpDynastyStd}," +
                    $"\"adp_half_ppr\":{validStats.Stats?.AdpHalfPpr}," +
                    $"\"adp_idp\":{validStats.Stats?.AdpIdp}," +
                    $"\"adp_ppr\":{validStats.Stats?.AdpPpr}," +
                    $"\"adp_rookie\":{validStats.Stats?.AdpRookie}," +
                    $"\"adp_std\":{validStats.Stats?.AdpStd}," +
                    $"\"rush_lng\":{validStats.Stats?.RushLng}," +
                    $"\"rush_td_lng\":{validStats.Stats?.RushTdLng}," +
                    $"\"pass_int\":{validStats.Stats?.PassInt}," +
                    $"\"pass_air_yd\":{validStats.Stats?.PassAirYd}," +
                    $"\"pos_rank_std\":{validStats.Stats?.PosRankStd}," +
                    $"\"gp\":{validStats.Stats?.GamesPlayed}," +
                    $"\"bonus_pass_yd_400\":{validStats.Stats?.BonusPassYd400}," +
                    $"\"rank_half_ppr\":{validStats.Stats?.RankHalfPpr}," +
                    $"\"tm_def_snp\":{validStats.Stats?.TeamDefSnp}," +
                    $"\"gms_active\":{validStats.Stats?.GamesActive}," +
                    $"\"penalty\":{validStats.Stats?.Penalty}," +
                    $"\"pass_sack_yds\":{validStats.Stats?.PassSackYds}," +
                    $"\"rank_std\":{validStats.Stats?.RankStd}," +
                    $"\"pos_rank_half_ppr\":{validStats.Stats?.PosRankHalfPpr}," +
                    $"\"rush_td\":{validStats.Stats?.RushTd}," +
                    $"\"pass_ypc\":{validStats.Stats?.PassYpc}," +
                    $"\"pass_lng\":{validStats.Stats?.PassLng}," +
                    $"\"pass_rush_yd\":{validStats.Stats?.PassRushYd}," +
                    $"\"pts_std\":{validStats.Stats?.PtsStd}," +
                    $"\"tm_st_snp\":{validStats.Stats?.TeamStSnp}," +
                    $"\"pass_rtg\":{validStats.Stats?.PassRtg}," +
                    $"\"bonus_pass_cmp_25\":{validStats.Stats?.BonusPassCmp25}," +
                    $"\"rush_fd\":{validStats.Stats?.RushFd}," +
                    $"\"pass_int_td\":{validStats.Stats?.PassIntTd}," +
                    $"\"pass_att\":{validStats.Stats?.PassAtt}," +
                    $"\"fum_lost\":{validStats.Stats?.FumLost}," +
                    $"\"rush_att\":{validStats.Stats?.RushAtt}," +
                    $"\"pass_rz_att\":{validStats.Stats?.PassRzAtt}," +
                    $"\"rush_ypa\":{validStats.Stats?.RushYpa}," +
                    $"\"gs\":{validStats.Stats?.Gs}," +
                    $"\"tm_off_snp\":{validStats.Stats?.TeamOffSnp}," +
                    $"\"pos_rank_ppr\":{validStats.Stats?.PosRankPpr}," +
                    $"\"rank_ppr\":{validStats.Stats?.RankPpr}," +
                    $"\"pass_sack\":{validStats.Stats?.PassSack}," +
                    $"\"penalty_yd\":{validStats.Stats?.PenaltyYd}," +
                    $"\"off_snp\":{validStats.Stats?.OffSnp}," +
                    $"\"pass_cmp\":{validStats.Stats?.PassCmp}," +
                    $"\"cmp_pct\":{validStats.Stats?.CmpPct}," +
                    $"\"rush_yac\":{validStats.Stats?.RushYac}," +
                    $"\"pts_half_ppr\":{validStats.Stats?.PtsHalfPpr}," +
                    $"\"pass_ypa\":{validStats.Stats?.PassYpa}," +
                    $"\"pass_cmp_40p\":{validStats.Stats?.PassCmp40p}," +
                    $"\"pass_fd\":{validStats.Stats?.PassFd}," +
                    $"\"pass_yd\":{validStats.Stats?.PassYd}," +
                    $"\"rush_rz_att\":{validStats.Stats?.RushRzAtt}," +
                    $"\"rush_rec_yd\":{validStats.Stats?.RushRecYd}," +
                    $"\"pass_td\":{validStats.Stats?.PassTd}," +
                    $"\"anytime_tds\":{validStats.Stats?.AnytimeTds}," +
                    $"\"bonus_fd_qb\":{validStats.Stats?.BonusFdQb}," +
                    $"\"rush_yd\":{validStats.Stats?.RushYd}," +
                    $"\"fum\":{validStats.Stats?.Fum}," +
                    $"\"pass_td_lng\":{validStats.Stats?.PassTdLng}," +
                    $"\"pts_ppr\":{validStats.Stats?.PtsPpr}," +
                    $"\"pass_inc\":{validStats.Stats?.PassInc}," +
                    $"\"rush_tkl_loss\":{validStats.Stats?.RushTklLoss}" +
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
                    $"\"injury_status\":{validStats.Player?.InjuryStatus?.ToString().ToLower()}," +
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
