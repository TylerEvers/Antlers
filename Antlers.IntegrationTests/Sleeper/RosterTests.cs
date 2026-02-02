using Antlers.Sleeper;
using Xunit.Sdk;

namespace Antlers.IntegrationTests.Sleeper
{
    public class RosterTests
    {
        private const long _validLeagueId = 1069748062454652928;
        private const int _invalidLeagueId = 0;

        [Fact]
        public async Task GetRoster_WithValidRosterId_ReturnsRoster()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperAppBaseUriStrategy());

            // Act
            var result = await apiClient.GetRosters(_validLeagueId);
            var rosters = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(rosters);
            Assert.False(string.IsNullOrEmpty(rawJson));
        }

        [Fact]
        public async Task GetRosters_WithInvalidRosterId_ReturnsNullRoster()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperAppBaseUriStrategy());

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await apiClient.GetRosters(_invalidLeagueId);
            });
        }
    }
}
