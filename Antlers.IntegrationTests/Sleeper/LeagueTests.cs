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
            var result = await apiClient.GetLeague(_validLeagueId);
            var league = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(league);
            Assert.Equal(_validLeagueId.ToString(), league.LeagueId);
            Assert.False(string.IsNullOrEmpty(rawJson));
        }

        [Fact]
        public async Task GetLeague_WithInvalidLeagueId_ReturnsNullLeague()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperAppBaseUriStrategy());

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() =>
                apiClient.GetLeague(_invalidLeagueId));
        }
    }
}
