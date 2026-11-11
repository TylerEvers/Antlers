namespace Antlers.FanDuel.Models;

public sealed record FanDuelMarketsFeed(
    IReadOnlyList<string> TabNames,
    IReadOnlyDictionary<string, FanDuelEventData> Events,
    IReadOnlyDictionary<string, FanDuelMarketData> Markets,
    IReadOnlyDictionary<(string EventId, string MarketId, string SelectionId), FanDuelSelectionData> Selections,
    IReadOnlyList<FanDuelFeedPayload> Payloads);

public sealed record FanDuelContentPage(
    IReadOnlyList<string> TabNames,
    IReadOnlyDictionary<string, FanDuelEventData> Events,
    IReadOnlyDictionary<string, FanDuelMarketData> Markets,
    FanDuelFeedPayload Payload);

public sealed record FanDuelMarketPriceBatch(
    IReadOnlyDictionary<string, FanDuelMarketPriceData> PricesByMarketId,
    FanDuelFeedPayload Payload);

public sealed record FanDuelFeedPayload(
    string FeedName,
    string RawPayloadJson,
    DateTime CapturedAtUtc);

public sealed record FanDuelEventData(
    string ExternalEventId,
    string? Name,
    DateTime? StartsAtUtc,
    string? Status,
    string MetadataJson);

public sealed record FanDuelMarketData(
    string EventId,
    string ExternalMarketId,
    string? MarketType,
    string? MarketKey,
    string? Scope,
    string? Name,
    string MetadataJson);

public sealed record FanDuelSelectionData(
    string EventId,
    string MarketId,
    string ExternalSelectionId,
    string? OutcomeKey,
    string? Label,
    string? ParticipantName,
    string? SourceParticipantId,
    decimal? Line,
    int? AmericanOdds,
    decimal? DecimalOdds,
    bool IsSuspended,
    string? Status,
    string RawJson);

public sealed record FanDuelMarketPriceData(
    string? MarketStatus,
    IReadOnlyDictionary<string, FanDuelPriceRunnerData> RunnersBySelectionId);

public sealed record FanDuelPriceRunnerData(
    string? RunnerStatus,
    int? AmericanOdds,
    decimal? DecimalOdds,
    string RawJson);
