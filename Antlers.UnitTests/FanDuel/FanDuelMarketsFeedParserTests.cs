using Antlers.FanDuel.Models;

namespace Antlers.UnitTests.FanDuel;

public class FanDuelMarketsFeedParserTests
{
    [Fact]
    public void ParseContentAndPrices_MapsProviderFeedData()
    {
        const string contentJson = """
            {
              "layout": {
                "tabs": {
                  "games": { "title": "Games", "cards": [{ "id": "card" }] },
                  "touchdowns": { "title": "Anytime TDs", "cards": [{ "id": "card" }] },
                  "props": { "title": "Player Props", "cards": [{ "id": "card" }] }
                },
                "cards": { "card": { "coupons": [{ "id": "coupon" }] } },
                "coupons": {
                  "coupon": {
                    "marketTypes": ["PLAYER_X_ALT_PASSING_YARDS_HIGH"],
                    "eventIds": ["event-1"],
                    "display": [{ "rows": [{ "marketIds": ["market-1"] }] }]
                  }
                }
              },
              "attachments": {
                "markets": {
                  "market-1": {
                    "marketId": "market-1",
                    "eventId": "event-1",
                    "marketType": "PLAYER_X_ALT_PASSING_YARDS_HIGH",
                    "marketName": "J. Doe - Passing Yards",
                    "marketStatus": "OPEN",
                    "runners": [{
                      "selectionId": "selection-1",
                      "runnerName": "Over 250.5",
                      "runnerStatus": "ACTIVE",
                      "handicap": 250.5
                    }]
                  }
                },
                "events": {
                  "event-1": {
                    "eventId": "event-1",
                    "name": "Home vs Away",
                    "openDate": "2026-11-15T18:00:00Z",
                    "status": "NOT start"
                  }
                }
              }
            }
            """;
        const string pricesJson = """
            [{
              "marketId": "market-1",
              "marketStatus": "OPEN",
              "runnerDetails": [{
                "selectionId": "selection-1",
                "runnerStatus": "ACTIVE",
                "winRunnerOdds": {
                  "americanDisplayOdds": { "americanOddsInt": 105 },
                  "decimalDisplayOdds": { "decimalOdds": 2.05 }
                }
              }]
            }]
            """;

        var capturedAtUtc = new DateTime(2026, 11, 10, 12, 0, 0, DateTimeKind.Utc);
        var content = FanDuelMarketsFeedParser.ParseContentPage(
            contentJson,
            "content-page",
            capturedAtUtc);
        var prices = FanDuelMarketsFeedParser.ParseMarketPrices(
            pricesJson,
            "market-prices-1",
            capturedAtUtc);
        var selections = FanDuelMarketsFeedParser.ReadSelections(content.Markets, prices.PricesByMarketId);
        var market = Assert.Single(content.Markets.Values);
        var eventData = Assert.Single(content.Events.Values);
        var selection = Assert.Single(selections.Values);

        Assert.Equal(new[] { "Games", "Anytime TDs", "Player Props" }, content.TabNames);
        Assert.Equal("passing_yards", market.MarketKey);
        Assert.Equal("player", market.Scope);
        Assert.True(eventData.StartsAtUtc.HasValue);
        Assert.Equal(
            DateTime.Parse("2026-11-15T18:00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
            eventData.StartsAtUtc.GetValueOrDefault());
        Assert.Equal("Over 250.5", selection.OutcomeKey);
        Assert.Equal("J. Doe", selection.ParticipantName);
        Assert.Equal("name:player:J. Doe", selection.SourceParticipantId);
        Assert.Equal(250.5m, selection.Line);
        Assert.Equal(105, selection.AmericanOdds);
        Assert.Equal(2.05m, selection.DecimalOdds);
        Assert.False(selection.IsSuspended);
        Assert.Equal("ACTIVE", selection.Status);
        Assert.Equal("content-page", content.Payload.FeedName);
        Assert.Equal(contentJson, content.Payload.RawPayloadJson);
        Assert.Equal("market-prices-1", prices.Payload.FeedName);
        Assert.Equal(pricesJson, prices.Payload.RawPayloadJson);
        Assert.Equal(capturedAtUtc, content.Payload.CapturedAtUtc);
        Assert.Equal(capturedAtUtc, prices.Payload.CapturedAtUtc);
    }

    [Fact]
    public void ParseContentPage_RejectsMissingCoreTabs()
    {
        const string contentJson = """{"layout":{"tabs":{},"cards":{},"coupons":{}}}""";

        var exception = Assert.Throws<InvalidDataException>(() =>
            FanDuelMarketsFeedParser.ParseContentPage(contentJson));

        Assert.Contains("requested NFL tabs", exception.Message);
    }

    [Fact]
    public void ParseMarketPrices_RejectsUnexpectedPayloadShape()
    {
        const string pricesJson = """{"prices":[]}""";

        var exception = Assert.Throws<InvalidDataException>(() =>
            FanDuelMarketsFeedParser.ParseMarketPrices(pricesJson, "market-prices-1"));

        Assert.Contains("unexpected market-price payload", exception.Message);
    }
}
