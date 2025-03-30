using Antlers.UnitTests.Sleeper.Mocks;
using Newtonsoft.Json;

namespace Antlers.UnitTests.Sleeper
{
    public class RosterTests
    {
        [Fact]
        public void Roster_Serialization_ReturnsCorrectJson()
        {
            // Arrange
            var validRoster = new RosterFaker().GenerateValidRoster();
            
            // Act
            var json = JsonConvert.SerializeObject(validRoster);
            
            // Assert
            string expectedJson = $"{{\"roster_id\":{validRoster.RosterId}," +
                                  $"\"players\":[{string.Join(",", validRoster.Players)}]," +
                                  $"\"starters\":[{string.Join(",", validRoster.Starters)}]," +
                                  $"\"reserve\":[{string.Join(",", validRoster.Reserve)}]," +
                                  $"\"taxi\":[{string.Join(",", validRoster.Taxi)}]," +
                                  $"\"settings\":{{" +
                                  $"\"wins\":{validRoster.Settings.Wins}," +
                                  $"\"waiver_position\":{validRoster.Settings.WaiverPosition}," +
                                  $"\"waiver_budget_used\":{validRoster.Settings.WaiverBudgetUsed}," +
                                  $"\"total_moves\":{validRoster.Settings.TotalMoves}," +
                                  $"\"ties\":{validRoster.Settings.Ties}," +
                                  $"\"losses\":{validRoster.Settings.Losses}," +
                                  $"\"fpts\":{validRoster.Settings.FantasyPoints}," +
                                  $"\"fpts_decimal\":{validRoster.Settings.FantasyPointsDecimal}," +
                                  $"\"fpts_against\":{validRoster.Settings.FantasyPointsAgainst}," +
                                  $"\"fpts_against_decimal\":{validRoster.Settings.FantasyPointsAgainstDecimal}}}," +
                                  $"\"owner_id\":\"{validRoster.OwnerId}\"," +
                                  $"\"co_owners\":{(validRoster.CoOwners == null ? "" : $"[{string.Join(",", validRoster.CoOwners.Select(co => $"\"{co}\""))}]")}," +
                                  $"\"league_id\":\"{validRoster.LeagueId}\"}}";

            Assert.Equal(expectedJson, json);
        }
    }
}
