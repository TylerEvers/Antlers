using Antlers.Sleeper;
using Antlers.Sleeper.Models;

namespace Antlers.IntegrationTests.Sleeper
{
    public class StatsTests
    {
        private const int _validPlayerId = 4881;
        private const int _invalidPlayerId = 0;

        [Fact]
        public async Task GetPlayerStats_WithValidPlayerId_ReturnsPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var result = await apiClient.GetPlayerStats("nfl", _validPlayerId, 2024, "regular");
            var stats = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(stats);
            Assert.Equal(_validPlayerId.ToString(), stats.PlayerId);
            Assert.False(string.IsNullOrEmpty(rawJson));
        }

        [Fact]
        public async Task GetPlayer_WithInvalidPlayerId_ReturnsNullPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var result = await apiClient.GetPlayerStats("nfl", _invalidPlayerId, 2024, "regular");
            var stats = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(stats);
            Assert.Null(stats.PlayerId);
            Assert.True(string.IsNullOrEmpty(rawJson));
        }
    }
}
