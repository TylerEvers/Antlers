using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class PlayerValuesTests
    {
        [Fact]
        public async Task GetPlayerValues_WithValidRequest_ReturnsPlayerValues()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var result = await apiClient.GetPlayerValues("nfl", "regular", 2026, "ppr", true, true);
            var playerValues = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(playerValues);
            Assert.NotEmpty(playerValues);
            Assert.False(string.IsNullOrEmpty(rawJson));

            var firstValue = playerValues.First();
            Assert.False(string.IsNullOrWhiteSpace(firstValue.Key));
        }
    }
}
