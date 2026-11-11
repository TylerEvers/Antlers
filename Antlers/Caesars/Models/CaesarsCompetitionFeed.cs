namespace Antlers.Caesars.Models;

public sealed record CaesarsCompetitionFeed(
    IReadOnlyList<CaesarsTabFeed> Tabs,
    IReadOnlyList<CaesarsEventFeed> Events,
    IReadOnlyList<CaesarsMarketFeed> Markets,
    IReadOnlyList<CaesarsSelectionFeed> Selections,
    IReadOnlyList<CaesarsRawFeed> RawFeeds);

public sealed record CaesarsTabFeed(string Id, string Name, string DataPath);

public sealed record CaesarsRawFeed(string FeedName, string RawPayloadJson, DateTime CapturedAtUtc)
{
    public string StoredPayloadJson => CaesarsFeedParser.SerializeRawFeed(this);
}

public sealed record CaesarsEventFeed(
    string ExternalEventId,
    string? Name,
    DateTime? StartsAtUtc,
    string? Status,
    string MetadataJson);

public sealed record CaesarsMarketFeed(
    string EventId,
    string ExternalMarketId,
    string? MarketTypeId,
    string? MarketKey,
    string? Name,
    string? Scope,
    string? Period,
    string MetadataJson);

public sealed record CaesarsSelectionFeed(
    string EventId,
    string MarketId,
    string ExternalSelectionId,
    string? OutcomeKey,
    string? Label,
    string? SourceParticipantId,
    string? ParticipantName,
    decimal? Line,
    int? AmericanOdds,
    decimal? DecimalOdds,
    bool IsSuspended,
    string? Status,
    string RawJson);
