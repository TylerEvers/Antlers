using Antlers.Bovada.Models;

namespace Antlers.UnitTests.Bovada;

public class BovadaFeedParserTests
{
    [Fact]
    public void ParseNflOdds_FlattensEventsMarketsAndSelections()
    {
        const string json = """
            [
              {
                "path": [{"id": "241", "description": "NFL"}],
                "events": [
                  {
                    "id": "event-1",
                    "description": "Away Team @ Home Team",
                    "status": "U",
                    "startTime": 1794532500000,
                    "competitors": [
                      {"id": "team-away", "name": "Away Team", "home": false},
                      {"id": "team-home", "name": "Home Team", "home": true}
                    ],
                    "displayGroups": [
                      {
                        "id": "group-1",
                        "description": "Game Lines",
                        "markets": [
                          {
                            "id": "market-1",
                            "description": "Point Spread",
                            "key": "2W-HCAP",
                            "marketTypeId": 120718,
                            "status": "O",
                            "period": {"description": "Game"},
                            "outcomes": [
                              {
                                "id": "selection-1",
                                "description": "Away Team",
                                "type": "A",
                                "competitorId": "team-away",
                                "status": "O",
                                "handicap": "+3.5",
                                "price": {"american": "-110", "decimal": "1.909"}
                              },
                              {
                                "id": "selection-2",
                                "description": "Home Team",
                                "type": "H",
                                "competitorId": "team-home",
                                "status": "S",
                                "handicap": "-3.5",
                                "price": {"american": "+105", "decimal": "2.050"}
                              }
                            ]
                          }
                        ]
                      }
                    ]
                  }
                ]
              }
            ]
            """;
        var capturedAtUtc = new DateTime(2026, 11, 10, 12, 0, 0, DateTimeKind.Utc);

        var result = BovadaFeedParser.ParseNflOdds(json, capturedAtUtc);

        var eventData = Assert.Single(result.Events);
        Assert.Equal("event-1", eventData.ExternalEventId);
        Assert.Equal("Away Team @ Home Team", eventData.Name);
        Assert.Equal("U", eventData.Status);
        Assert.Equal(
            DateTimeOffset.FromUnixTimeMilliseconds(1794532500000).UtcDateTime,
            eventData.StartsAtUtc);

        var market = Assert.Single(result.Markets);
        Assert.Equal("market-1", market.ExternalMarketId);
        Assert.Equal("Point Spread", market.Name);
        Assert.Equal("2W-HCAP", market.MarketKey);
        Assert.Equal("Game Lines", market.Scope);
        Assert.Equal("Game", market.Period);

        Assert.Equal(capturedAtUtc, result.CapturedAtUtc);
        Assert.Equal(json, result.RawPayloadJson);

        var selections = result.Selections.OrderBy(selection => selection.ExternalSelectionId).ToArray();
        Assert.Equal(2, selections.Length);
        Assert.Equal("team-away", selections[0].SourceParticipantId);
        Assert.Equal("Away Team", selections[0].ParticipantName);
        Assert.Equal(3.5m, selections[0].Line);
        Assert.Equal(-110, selections[0].AmericanOdds);
        Assert.Equal(1.909m, selections[0].DecimalOdds);
        Assert.False(selections[0].IsSuspended);
        Assert.True(selections[1].IsSuspended);
    }

    [Fact]
    public void ParseNflOdds_RejectsInvalidJson()
    {
        var exception = Assert.Throws<InvalidDataException>(() =>
            BovadaFeedParser.ParseNflOdds("{"));

        Assert.IsType<System.Text.Json.JsonException>(exception.InnerException);
    }

    [Fact]
    public void ParseNflOdds_RejectsOutcomeReferencingAnotherMarket()
    {
        const string json = """
            [
              {
                "events": [{
                  "id": "event-1",
                  "displayGroups": [{
                    "markets": [{
                      "id": "market-1",
                      "outcomes": [{"id": "selection-1", "marketId": "market-2"}]
                    }]
                  }]
                }]
              }
            ]
            """;

        Assert.Throws<InvalidDataException>(() => BovadaFeedParser.ParseNflOdds(json));
    }

    [Fact]
    public void ParseNflOdds_RejectsInvalidMarketOdds()
    {
        const string json = """
            [
              {
                "events": [{
                  "id": "event-1",
                  "displayGroups": [{
                    "markets": [{
                      "id": "market-1",
                      "outcomes": [{
                        "id": "selection-1",
                        "price": {"american": "even"}
                      }]
                    }]
                  }]
                }]
              }
            ]
            """;

        Assert.Throws<InvalidDataException>(() => BovadaFeedParser.ParseNflOdds(json));
    }
}
