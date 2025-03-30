using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class ScheduleTests
    {
        [Fact]
        public async Task GetSchedule_ValidRequest_ReturnsSchedule()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var schedule = await apiClient.GetSchedule("nfl", "pre", 2024);

            // Assert
            Assert.NotNull(schedule);
            Assert.Equal(schedule?.Count(), 48);
        }

        [Fact]
        public async Task GetSchedule_WithInvalidYear_ReturnsEmptySchedule()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var schedule = await apiClient.GetSchedule("nfl", "pre", 1);

            // Assert
            Assert.NotNull(schedule);
            Assert.Equal(schedule?.Count(), 0);
        }
    }
}
