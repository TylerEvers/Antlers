using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class SleeperApiClientTests
    {
        private const int _validPlayerId = 4881; //Lamar Jackson
        private const int _invalidPlayerId = 0;

        [Fact]
        public void GetPlayer_WithValidPlayerId_ReturnsPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var player = apiClient.GetPlayer("nfl", _validPlayerId);

            // Assert
            Assert.NotNull(player);
            Assert.Equal(_validPlayerId.ToString(), player.PlayerId);
        }

        [Fact]
        public void GetPlayer_WithInvalidPlayerId_ReturnsPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var player = apiClient.GetPlayer("nfl", _invalidPlayerId);

            // Assert
            Assert.Null(player.PlayerId);
        }
    }
}
