namespace Antlers.Bovada.Models;

public sealed class BovadaNflOddsResponse
{
    public IReadOnlyList<BovadaEventFeed> Events { get; init; } = [];
    public IReadOnlyList<BovadaMarketFeed> Markets { get; init; } = [];
    public IReadOnlyList<BovadaSelectionFeed> Selections { get; init; } = [];
    public DateTime CapturedAtUtc { get; init; }
    public string RawPayloadJson { get; set; } = string.Empty;
}

public sealed record BovadaEventFeed(
    string ExternalEventId,
    string? Name,
    DateTime? StartsAtUtc,
    string? Status,
    string MetadataJson);

public sealed record BovadaMarketFeed(
    string EventId,
    string ExternalMarketId,
    string? MarketTypeId,
    string? MarketKey,
    string? Name,
    string? Scope,
    string? Period,
    string MetadataJson);

public sealed record BovadaSelectionFeed(
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
