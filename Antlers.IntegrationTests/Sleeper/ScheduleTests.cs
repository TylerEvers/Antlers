using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class ScheduleTests
    {
        //public async Task<IEnumerable<Schedule>> GetSchedule(string sport, string segment, int year)

        [Fact]
        public async void GetSchedule_ValidRequest_ReturnsSchedule()
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
        public async void GetSchedule_WithInvalidYear_ReturnsEmptySchedule()
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
