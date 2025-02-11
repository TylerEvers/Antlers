using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class LeagueTests
    {
        private const long _validLeagueId = 1069748062454652928;
        private const int _invalidLeagueId = 0;

        [Fact]
        public async Task GetLeague_WithValidLeagueId_ReturnsLeague()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperAppBaseUriStrategy());

            // Act
            var league = await apiClient.GetLeague(_validLeagueId);

            // Assert
            Assert.NotNull(league);
            Assert.Equal(_validLeagueId.ToString(), league.LeagueId);
        }

        [Fact]
        public async Task GetLeague_WithInvalidLeagueId_ReturnsNullLeague()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperAppBaseUriStrategy());

            // Act
            var league = await apiClient.GetLeague(_invalidLeagueId);

            // Assert
            Assert.Null(league.LeagueId);
        }
    }
}
