using Antlers.UnitTests.Sleeper.Mocks;
using Newtonsoft.Json;

namespace Antlers.UnitTests.Sleeper
{
    public class PlayerTests
    {
        [Fact]
        public void Player_Serialization_ReturnsCorrectJson()
        {
            // Arrange
            var validPlayer = new PlayerFaker().GenerateValidPlayer();
            
            // Act
            var json = JsonConvert.SerializeObject(validPlayer);

            // Assert
            string expectedJson = $"{{\"player_id\":\"{validPlayer.PlayerId}\"," +
                                    $"\"first_name\":\"{validPlayer.FirstName}\"," +
                                    $"\"last_name\":\"{validPlayer.LastName}\"," +
                                    $"\"number\":{validPlayer.Number}," +
                                    $"\"position\":\"{validPlayer.Position}\"," +
                                    $"\"fantasy_positions\":[{string.Join(",", validPlayer.FantasyPositions.Select(fp => $"\"{fp}\""))}]," +
                                    $"\"status\":\"{validPlayer.Status}\"," +
                                    $"\"age\":{validPlayer.Age}," +
                                    $"\"height\":\"{validPlayer.Height}\"," +
                                    $"\"weight\":\"{validPlayer.Weight}\"," +
                                    $"\"birth_date\":\"{validPlayer.BirthDate}\"," +
                                    $"\"sport\":\"{validPlayer.Sport}\"," +
                                    $"\"team\":\"{validPlayer.Team}\"," +
                                    $"\"team_abbr\":\"{validPlayer.TeamAbbreviation}\"," +
                                    $"\"college\":\"{validPlayer.College}\"," +
                                    $"\"high_school\":\"{validPlayer.HighSchool}\"," +
                                    $"\"depth_chart_position\":\"{validPlayer.DepthChartPosition}\"," +
                                    $"\"depth_chart_order\":{validPlayer.DepthChartOrder}," +
                                    $"\"years_exp\":{validPlayer.YearsExperience}," +
                                    $"\"practice_participation\":{validPlayer.PracticeParticipation.ToString().ToLower()}," +
                                    $"\"injury_start_date\":\"{validPlayer.InjuryStartDate}\"," +
                                    $"\"injury_status\":{validPlayer.InjuryStatus.ToString().ToLower()}," +
                                    $"\"injury_body_part\":\"{validPlayer.InjuryBodyPart}\"," +
                                    $"\"injury_notes\":\"{validPlayer.InjuryNotes}\"," +
                                    $"\"search_rank\":{validPlayer.SearchRank}," +
                                    $"\"search_full_name\":\"{validPlayer.SearchFullName}\"," +
                                    $"\"search_first_name\":\"{validPlayer.SearchFirstName}\"," +
                                    $"\"search_last_name\":\"{validPlayer.SearchLastName}\"," +
                                    $"\"hashtag\":\"{validPlayer.Hashtag}\"," +
                                    $"\"fantasy_data_id\":{validPlayer.FantasyDataId}," +
                                    $"\"stats_id\":{validPlayer.StatsId}," +
                                    $"\"sportradar_id\":\"{validPlayer.SportRadarId}\"," +
                                    $"\"espn_id\":{validPlayer.EspnId}," +
                                    $"\"rotowire_id\":{validPlayer.RotowireId}," +
                                    $"\"rotoworld_id\":{validPlayer.RotoworldId}," +
                                    $"\"yahoo_id\":{validPlayer.YahooId}," +
                                    $"\"oddsjam_id\":\"{validPlayer.OddsJamId}\"," +
                                    $"\"pandascore_id\":\"{validPlayer.PandaScoreId}\"," +
                                    $"\"gsis_id\":\"{validPlayer.GsisId}\"," +
                                    "\"metadata\":{" +
                                        $"\"channel_id\":\"{validPlayer.Metadata?.ChannelId}\"," +
                                        $"\"rookie_year\":\"{validPlayer.Metadata?.RookieYear}\"}}" +
                                    "}";


            Assert.Equal(expectedJson, json);
        }
    }
}
