using System.Globalization;
using System.Text.Json;

namespace Antlers.Bovada.Models;

public static class BovadaFeedParser
{
    public static BovadaNflOddsResponse ParseNflOdds(
        string rawPayloadJson,
        DateTime? capturedAtUtc = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(rawPayloadJson);

        using var document = ParseDocument(rawPayloadJson);
        if (document.RootElement.ValueKind != JsonValueKind.Array)
        {
            throw new InvalidDataException("Bovada returned an unexpected NFL odds payload.");
        }

        var events = new Dictionary<string, BovadaEventFeed>(StringComparer.Ordinal);
        var markets = new Dictionary<(string EventId, string MarketId), BovadaMarketFeed>();
        var selections = new Dictionary<(string EventId, string MarketId, string SelectionId), BovadaSelectionFeed>();

        foreach (var group in document.RootElement.EnumerateArray())
        {
            foreach (var eventItem in GetArray(group, "events"))
            {
                var eventId = RequireString(eventItem, "id", "event");
                var eventData = new BovadaEventFeed(
                    eventId,
                    ReadString(eventItem, "description"),
                    ReadUnixMilliseconds(eventItem, "startTime"),
                    ReadString(eventItem, "status"),
                    eventItem.GetRawText());
                events[eventId] = eventData;

                var competitorNames = GetArray(eventItem, "competitors")
                    .Select(competitor => (
                        Id: ReadString(competitor, "id"),
                        Name: ReadString(competitor, "name")))
                    .Where(competitor => !string.IsNullOrWhiteSpace(competitor.Id))
                    .ToDictionary(
                        competitor => competitor.Id!,
                        competitor => competitor.Name,
                        StringComparer.Ordinal);

                foreach (var displayGroup in GetArray(eventItem, "displayGroups"))
                {
                    var scope = ReadString(displayGroup, "description");
                    foreach (var marketItem in GetArray(displayGroup, "markets"))
                    {
                        var marketId = RequireString(marketItem, "id", "market");
                        var marketStatus = ReadString(marketItem, "status");
                        markets[(eventId, marketId)] = new BovadaMarketFeed(
                            eventId,
                            marketId,
                            ReadString(marketItem, "marketTypeId"),
                            ReadString(marketItem, "key"),
                            ReadString(marketItem, "description"),
                            scope,
                            ReadString(GetProperty(marketItem, "period"), "description"),
                            marketItem.GetRawText());

                        foreach (var outcome in GetArray(marketItem, "outcomes"))
                        {
                            var selectionId = RequireString(outcome, "id", "outcome");
                            var outcomeMarketId = ReadString(outcome, "marketId");
                            if (outcomeMarketId is not null
                                && !string.Equals(outcomeMarketId, marketId, StringComparison.Ordinal))
                            {
                                throw new InvalidDataException(
                                    $"Bovada outcome '{selectionId}' references a different market.");
                            }

                            var status = ReadString(outcome, "status");
                            var participantId = ReadString(outcome, "competitorId");
                            competitorNames.TryGetValue(participantId ?? string.Empty, out var participantName);
                            var price = GetProperty(outcome, "price");
                            selections[(eventId, marketId, selectionId)] = new BovadaSelectionFeed(
                                eventId,
                                marketId,
                                selectionId,
                                ReadString(outcome, "type"),
                                ReadString(outcome, "description"),
                                participantId,
                                participantName,
                                ReadDecimal(outcome, "handicap"),
                                ReadAmericanOdds(price),
                                ReadDecimal(price, "decimal"),
                                !IsOpen(status) || !IsOpen(marketStatus),
                                status,
                                outcome.GetRawText());
                        }
                    }
                }
            }
        }

        return new BovadaNflOddsResponse
        {
            Events = events.Values.ToArray(),
            Markets = markets.Values.ToArray(),
            Selections = selections.Values.ToArray(),
            CapturedAtUtc = capturedAtUtc?.ToUniversalTime() ?? DateTime.UtcNow,
            RawPayloadJson = rawPayloadJson
        };
    }

    private static JsonDocument ParseDocument(string rawPayloadJson)
    {
        try
        {
            return JsonDocument.Parse(rawPayloadJson);
        }
        catch (JsonException exception)
        {
            throw new InvalidDataException("Bovada returned invalid NFL odds JSON.", exception);
        }
    }

    private static IEnumerable<JsonElement> GetArray(JsonElement parent, string propertyName)
    {
        var value = GetProperty(parent, propertyName);
        if (value.ValueKind is JsonValueKind.Undefined or JsonValueKind.Null)
        {
            return [];
        }

        if (value.ValueKind != JsonValueKind.Array)
        {
            throw new InvalidDataException($"Bovada returned an invalid '{propertyName}' collection.");
        }

        return value.EnumerateArray().ToArray();
    }

    private static JsonElement GetProperty(JsonElement parent, string propertyName) =>
        parent.ValueKind == JsonValueKind.Object
        && parent.TryGetProperty(propertyName, out var value)
            ? value
            : default;

    private static string RequireString(JsonElement parent, string propertyName, string entityName) =>
        ReadString(parent, propertyName)
        ?? throw new InvalidDataException($"Bovada returned an {entityName} without an ID.");

    private static string? ReadString(JsonElement parent, string propertyName)
    {
        var value = GetProperty(parent, propertyName);
        return value.ValueKind switch
        {
            JsonValueKind.String => value.GetString(),
            JsonValueKind.Number => value.GetRawText(),
            _ => null
        };
    }

    private static DateTime? ReadUnixMilliseconds(JsonElement parent, string propertyName)
    {
        var value = ReadString(parent, propertyName);
        if (value is null)
        {
            return null;
        }

        if (!long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var milliseconds))
        {
            throw new InvalidDataException($"Bovada returned an invalid '{propertyName}' timestamp.");
        }

        try
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;
        }
        catch (ArgumentOutOfRangeException exception)
        {
            throw new InvalidDataException($"Bovada returned an invalid '{propertyName}' timestamp.", exception);
        }
    }

    private static decimal? ReadDecimal(JsonElement parent, string propertyName)
    {
        var value = ReadString(parent, propertyName);
        if (value is null)
        {
            return null;
        }

        if (decimal.TryParse(
                value,
                NumberStyles.Number | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out var result))
        {
            return result;
        }

        throw new InvalidDataException($"Bovada returned an invalid '{propertyName}' value.");
    }

    private static int? ReadAmericanOdds(JsonElement price)
    {
        var value = ReadString(price, "american");
        if (value is null)
        {
            return null;
        }

        if (int.TryParse(value, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var result))
        {
            return result;
        }

        throw new InvalidDataException("Bovada returned invalid American odds.");
    }

    private static bool IsOpen(string? status) =>
        string.Equals(status, "O", StringComparison.OrdinalIgnoreCase);
}
