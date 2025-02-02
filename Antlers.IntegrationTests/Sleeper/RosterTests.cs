using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class RosterTests
    {
        private const long _validLeagueId = 1069748062454652928; //TODO: Move to appsettings?
        private const int _invalidLeagueId = 0;

        [Fact]
        public async void GetRoster_WithValidRosterId_ReturnsRoster()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperAppBaseUriStrategy());

            // Act
            var rosters = await apiClient.GetRosters(_validLeagueId);

            // Assert
            Assert.NotNull(rosters);
        }

        [Fact]
        public async void GetRosters_WithInvalidRosterId_ReturnsNullRoster()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperAppBaseUriStrategy());

            // Act
            var rosters = await apiClient.GetRosters(_invalidLeagueId);

            // Assert
            Assert.Empty(rosters);
        }
    }
}
