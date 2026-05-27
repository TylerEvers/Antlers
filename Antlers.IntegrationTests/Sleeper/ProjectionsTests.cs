using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class ProjectionsTests
    {
        [Fact]
        public async Task GetPlayerProjections_WithValidPositions_ReturnsProjections()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());
            var positions = new[] { "DB", "DL", "K", "LB", "QB", "RB", "TE", "WR" };

            // Act
            var result = await apiClient.GetPlayerProjections("nfl", 2026, "regular", positions, "adp_idp");
            var projections = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(projections);
            Assert.NotEmpty(projections);
            Assert.False(string.IsNullOrEmpty(rawJson));

            var firstProjection = projections.First();
            Assert.Equal("regular", firstProjection.SeasonType);
            Assert.Equal("2026", firstProjection.Season);
            Assert.NotNull(firstProjection.Stats);
            Assert.NotNull(firstProjection.PlayerId);
        }
    }
}
