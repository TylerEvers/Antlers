using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class PlayerTests
    {
        private const int _validPlayerId = 4881;
        private const int _invalidPlayerId = 0;

        [Fact]
        public async Task GetPlayer_WithValidPlayerId_ReturnsPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var result = await apiClient.GetPlayer("nfl", _validPlayerId);
            var player = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(player);
            Assert.Equal(_validPlayerId.ToString(), player.PlayerId);
            Assert.False(string.IsNullOrEmpty(rawJson));
        }

        [Fact]
        public async Task GetPlayer_WithInvalidPlayerId_ReturnsNullPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var result = await apiClient.GetPlayer("nfl", _invalidPlayerId);
            var player = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.Null(player.PlayerId);
            Assert.True(string.IsNullOrEmpty(rawJson));
        }
    }
}
