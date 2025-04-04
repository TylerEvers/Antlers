﻿using Antlers.Sleeper;

namespace Antlers.IntegrationTests.Sleeper
{
    public class StatsTests
    {
        private const int _validPlayerId = 4881;
        private const int _invalidPlayerId = 0;

        [Fact]
        public async Task GetPlayerStats_WithValidPlayerId_ReturnsPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var stats = await apiClient.GetPlayerStats("nfl", _validPlayerId, 2024, "regular");

            // Assert
            Assert.NotNull(stats);
            Assert.Equal(_validPlayerId.ToString(), stats.PlayerId);
        }

        [Fact]
        public async Task GetPlayer_WithInvalidPlayerId_ReturnsNullPlayer()
        {
            // Arrange
            var apiClient = new SleeperApiClient(new SleeperWebBaseUriStrategy());

            // Act
            var stats = await apiClient.GetPlayerStats("nfl", _invalidPlayerId, 2024, "regular");

            // Assert
            Assert.Null(stats.PlayerId);
        }
    }
}
