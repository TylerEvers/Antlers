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
            var result = await apiClient.GetSchedule("nfl", "pre", 2024);
            var schedule = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(schedule);
            Assert.Equal(48, schedule.Count());
            Assert.False(string.IsNullOrEmpty(rawJson));
        }

        [Fact]
        public async Task GetSchedule_WithInvalidYear_ReturnsEmptySchedule()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var result = await apiClient.GetSchedule("nfl", "pre", 1);
            var schedule = result.Item1;
            var rawJson = result.Item2;

            // Assert
            Assert.NotNull(schedule);
            Assert.Empty(schedule);
            Assert.True(string.IsNullOrEmpty(rawJson));
        }
    }
}
