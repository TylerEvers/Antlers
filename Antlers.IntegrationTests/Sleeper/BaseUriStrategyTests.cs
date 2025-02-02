using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class BaseUriStrategyTests
    {
        [Fact]
        public void SleeperAppBaseUriStrategy_ReturnsCorrectBaseUri()
        {
            // Arrange
            var strategy = new SleeperAppBaseUriStrategy();

            // Act
            var result = strategy.GetBaseUri();

            // Assert
            Assert.Equal("https://api.sleeper.app/v1", result);
        }

        [Fact]
        public void SleeperWebBaseUriStrategy_ReturnsCorrectBaseUri()
        {
            // Arrange
            var strategy = new SleeperWebBaseUriStrategy();

            // Act
            var result = strategy.GetBaseUri();

            // Assert
            Assert.Equal("https://api.sleeper.com", result);
        }
    }
}
