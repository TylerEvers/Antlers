using Antlers.UnitTests.Sleeper.Mocks;
using Newtonsoft.Json;

namespace Antlers.UnitTests.Sleeper
{
    public class ScheduleTests
    {
        [Fact]
        public void Schedule_Serialization_ReturnsCorrectJson()
        {
            // Arrange
            var validSchedule = new ScheduleFaker().GenerateValidSchedule();
            
            // Act
            var json = JsonConvert.SerializeObject(validSchedule);
            
            // Assert
            string expectedJson = $"{{\"game_id\":\"{validSchedule.GameId}\"," +
                                  $"\"week\":{validSchedule.Week}," +
                                  $"\"date\":\"{validSchedule.Date}\"," +
                                  $"\"home\":\"{validSchedule.Home}\"," +
                                  $"\"away\":\"{validSchedule.Away}\"," +
                                  $"\"status\":\"{validSchedule.Status}\"}}";

            Assert.Equal(expectedJson, json);
        }
    }
}
