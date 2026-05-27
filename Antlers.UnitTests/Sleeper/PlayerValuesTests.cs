using Antlers.UnitTests.Sleeper.Mocks;
using Newtonsoft.Json;

namespace Antlers.UnitTests.Sleeper
{
    public class PlayerValuesTests
    {
        [Fact]
        public void PlayerValues_SerializationAndDeserialization_ReturnsCorrectData()
        {
            // Arrange
            var playerValues = new PlayerValuesFaker().GenerateValidPlayerValues();

            // Act
            var json = JsonConvert.SerializeObject(playerValues);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(json);

            // Assert
            Assert.NotNull(deserialized);
            Assert.Equal(playerValues.Count, deserialized.Count);

            foreach (var kvp in playerValues)
            {
                Assert.True(deserialized.ContainsKey(kvp.Key));
                Assert.Equal(kvp.Value, deserialized[kvp.Key]);
            }
        }
    }
}
