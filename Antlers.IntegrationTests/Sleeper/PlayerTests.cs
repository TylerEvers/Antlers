using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class PlayerTests
    {
        private const int _validPlayerId = 4881; //Lamar Jackson TODO: Move to appsettings?
        private const int _invalidPlayerId = 0;

        [Fact]
        public async void GetPlayer_WithValidPlayerId_ReturnsPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var player = await apiClient.GetPlayer("nfl", _validPlayerId);

            // Assert
            Assert.NotNull(player);
            Assert.Equal(_validPlayerId.ToString(), player.PlayerId);
        }

        [Fact]
        public async void GetPlayer_WithInvalidPlayerId_ReturnsPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var player = await apiClient.GetPlayer("nfl", _invalidPlayerId);

            // Assert
            Assert.Null(player.PlayerId);
        }
    }
}
