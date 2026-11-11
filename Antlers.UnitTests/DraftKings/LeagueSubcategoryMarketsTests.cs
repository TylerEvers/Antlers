using Antlers.DraftKings;
using Antlers.DraftKings.Models;
using Newtonsoft.Json;

namespace Antlers.UnitTests.DraftKings;

public class LeagueSubcategoryMarketsTests
{
    [Fact]
    public void DeserializeMarketsResponse_WithArrays_MapsGameAndPlayerLines()
    {
        const string json = """
            {
              "markets": [
                {
                  "id": "market-1",
                  "eventId": "event-1",
                  "name": "Passing Yards",
                  "clientMetadata": { "subCategoryId": 4518 },
                  "selections": [
                    {
                      "id": "selection-1",
                      "marketId": "market-1",
                      "label": "Over 250.5",
                      "line": "250.5",
                      "outcomeType": "Over",
                      "displayOdds": { "american": "-110" },
                      "milestoneValue": 250.5,
                      "participants": [{ "name": "Player One", "type": "Player" }]
                    }
                  ]
                }
              ],
              "events": [
                {
                  "id": "event-1",
                  "name": "Home vs Away",
                  "participants": [{ "name": "Home", "type": "Team" }]
                }
              ]
            }
            """;

        var result = JsonConvert.DeserializeObject<LeagueSubcategoryMarketsResponse>(json);

        Assert.NotNull(result);
        var market = Assert.Single(result.Markets);
        Assert.Equal(4518, market.ClientMetadata?.SubCategoryId);
        Assert.Equal("250.5", Assert.Single(market.Selections!).Line);
        Assert.Equal(250.5m, Assert.Single(market.Selections!).MilestoneValue);
        Assert.Equal("Player One", Assert.Single(market.Selections![0].Participants!).Name);
        Assert.Equal("Home vs Away", Assert.Single(result.Events).Name);
    }

    [Fact]
    public void DeserializeMarketsResponse_WithEntityMaps_MapsCollections()
    {
        const string json = """
            {
              "markets": {
                "market-1": { "id": "market-1", "name": "Spread" }
              },
              "events": {
                "event-1": { "id": "event-1", "name": "Home vs Away" }
              }
            }
            """;

        var result = JsonConvert.DeserializeObject<LeagueSubcategoryMarketsResponse>(json);

        Assert.NotNull(result);
        Assert.Equal("market-1", Assert.Single(result.Markets).Id);
        Assert.Equal("event-1", Assert.Single(result.Events).Id);
    }

    [Fact]
    public async Task GetLeagueSubcategoryMarkets_RejectsInvalidIds()
    {
        var client = new DraftKingsApiClient();

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            client.GetLeagueSubcategoryMarkets(0, 4518));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            client.GetLeagueSubcategoryMarkets(88808, 0));
    }

    [Fact]
    public async Task GetLeagueSubcategoryMarkets_RejectsInvalidJurisdictions()
    {
        var client = new DraftKingsApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetLeagueSubcategoryMarkets(88808, 4518, "CA"));
    }
}
