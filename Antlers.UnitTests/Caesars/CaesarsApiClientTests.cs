using Antlers.Caesars;
using Antlers.Caesars.Models;

namespace Antlers.UnitTests.Caesars;

public class CaesarsApiClientTests
{
    [Theory]
    [InlineData("US-IA", "ia")]
    [InlineData("US-NJ", "nj")]
    public void BuildUri_UsesStateCodeWithoutCountryPrefix(string jurisdiction, string location)
    {
        var uri = CaesarsApiClient.BuildUri(jurisdiction, "/v4/sports/americanfootball/competitions/123/tabs?test=1");

        Assert.Equal($"https://api.americanwagering.com/regions/us/locations/{location}/brands/czr/sb/v4/sports/americanfootball/competitions/123/tabs?test=1", uri.ToString());
    }

    [Fact]
    public async Task GetCompetitionTabs_RejectsEmptyCompetitionId()
    {
        using var client = new CaesarsApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetCompetitionTabs(Guid.Empty, "US-IA"));
    }

    [Theory]
    [InlineData("US")]
    [InlineData("CA-ON")]
    public async Task GetCompetitionTabs_RejectsInvalidJurisdiction(string jurisdiction)
    {
        using var client = new CaesarsApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetCompetitionTabs(Guid.NewGuid(), jurisdiction));
    }

    [Fact]
    public async Task GetTabData_RejectsPathOutsideRequestedCompetition()
    {
        using var client = new CaesarsApiClient();
        var competitionId = Guid.NewGuid();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetTabData(
                competitionId,
                "US-IA",
                $"/v4/sports/americanfootball/competitions/{Guid.NewGuid():D}/tabs/games",
                "Games"));
    }

    [Fact]
    public async Task GetTabData_RejectsUnsupportedTabName()
    {
        using var client = new CaesarsApiClient();
        var competitionId = Guid.NewGuid();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.GetTabData(
                competitionId,
                "US-IA",
                $"/v4/sports/americanfootball/competitions/{competitionId:D}/tabs/games",
                "Unsupported"));
    }

    [Fact]
    public void ParseFeed_MapsProviderDataAndPreservesRawFeeds()
    {
        var competitionId = Guid.NewGuid();
        var capturedAtUtc = DateTime.UtcNow;
        var tabsJson = """
            {"tabs":[{"id":"NFL%7CGames","dataPath":"/v4/sports/americanfootball/competitions/COMPETITION/tabs/games"}]}
            """.Replace("COMPETITION", competitionId.ToString("D"), StringComparison.Ordinal);
        const string gamesJson = """
            {
              "competitions": [{
                "id": "COMPETITION",
                "events": [{
                  "id": "event-1",
                  "name": "Away at Home",
                  "startTime": "2026-11-10T20:00:00-06:00",
                  "status": "open",
                  "keyMarketGroups": [{
                    "markets": [{
                      "id": "market-1",
                      "type": "player-prop",
                      "displayName": "|Passing Yards|",
                      "metadata": {"marketCategory": "passing", "flowSegment": "player", "period": "full"},
                      "selections": [{
                        "id": "selection-1",
                        "type": "over",
                        "displayName": "|Over|",
                        "sourceParticipantId": "player-1",
                        "participantName": "Player One",
                        "line": "250.5",
                        "price": {"a": -110, "d": 1.91},
                        "status": "open"
                      }]
                    }]
                  }]
                }]
              }]
            }
            """;
        var rawGamesJson = gamesJson.Replace("COMPETITION", competitionId.ToString("D"), StringComparison.Ordinal);

        var feed = CaesarsFeedParser.ParseFeed(
            competitionId,
            tabsJson,
            capturedAtUtc,
            [new CaesarsRawFeed("Games", rawGamesJson, capturedAtUtc)]);

        Assert.Equal("Games", Assert.Single(feed.Tabs).Name);
        Assert.Equal(2, feed.RawFeeds.Count);
        Assert.Equal("tabs", feed.RawFeeds[0].FeedName);
        Assert.Equal(tabsJson, feed.RawFeeds[0].RawPayloadJson);
        Assert.Equal(capturedAtUtc, feed.RawFeeds[1].CapturedAtUtc);

        var eventData = Assert.Single(feed.Events);
        Assert.Equal("event-1", eventData.ExternalEventId);
        Assert.Equal("Away at Home", eventData.Name);
        Assert.Equal("open", eventData.Status);
        Assert.Contains("\"keyMarketGroups\"", eventData.MetadataJson);

        var market = Assert.Single(feed.Markets);
        Assert.Equal("passing", market.MarketKey);
        Assert.Equal("Passing Yards", market.Name);
        Assert.Contains("\"flowSegment\"", market.MetadataJson);

        var selection = Assert.Single(feed.Selections);
        Assert.Equal("player-1", selection.SourceParticipantId);
        Assert.Equal("Player One", selection.ParticipantName);
        Assert.Equal(250.5m, selection.Line!.Value);
        Assert.Equal(-110, selection.AmericanOdds!.Value);
        Assert.Equal(1.91m, selection.DecimalOdds!.Value);
        Assert.False(selection.IsSuspended);
        Assert.Contains("\"price\"", selection.RawJson);
        Assert.Contains("\"tabId\":\"Games\"", feed.RawFeeds[1].StoredPayloadJson);
    }

    [Fact]
    public void ParseFeed_RejectsSelectionReferencingAnotherMarket()
    {
        var competitionId = Guid.NewGuid();
        var tabsJson = """
            {"tabs":[{"id":"NFL%7CGames","dataPath":"/v4/sports/americanfootball/competitions/COMPETITION/tabs/games"}]}
            """.Replace("COMPETITION", competitionId.ToString("D"), StringComparison.Ordinal);
        var gamesJson = """
            {"competitions":[{"id":"COMPETITION","events":[{"id":"event-1","keyMarketGroups":[{"markets":[{"id":"market-1","selections":[{"id":"selection-1","marketId":"market-2"}]}]}]}]}]}
            """.Replace("COMPETITION", competitionId.ToString("D"), StringComparison.Ordinal);

        Assert.Throws<InvalidDataException>(() =>
            CaesarsFeedParser.ParseFeed(
                competitionId,
                tabsJson,
                DateTime.UtcNow,
                [new CaesarsRawFeed("Games", gamesJson, DateTime.UtcNow)]));
    }
}
