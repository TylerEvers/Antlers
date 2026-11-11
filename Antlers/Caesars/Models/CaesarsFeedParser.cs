using System.Globalization;
using System.Text.Json;

namespace Antlers.Caesars.Models;

public static class CaesarsFeedParser
{
    private const string Sport = "americanfootball";
    private static readonly HashSet<string> CoreTabNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "Games",
        "TD Scorer",
        "Passing",
        "Rushing",
        "Receiving",
        "Game Specials"
    };

    public static IReadOnlyList<CaesarsTabFeed> ParseTabs(Guid competitionId, string rawPayload)
    {
        ValidateCompetitionId(competitionId);
        using var document = ParseDocument(rawPayload, "tabs");
        var tabs = new List<CaesarsTabFeed>();
        foreach (var tab in GetArray(document.RootElement, "tabs"))
        {
            var id = ReadString(tab, "id");
            if (string.IsNullOrWhiteSpace(id))
            {
                continue;
            }

            var decodedId = Uri.UnescapeDataString(id);
            var name = decodedId.Split('|').LastOrDefault();
            if (string.IsNullOrWhiteSpace(name) || !CoreTabNames.Contains(name))
            {
                continue;
            }

            var dataPath = ReadString(tab, "dataPath");
            if (string.IsNullOrWhiteSpace(dataPath))
            {
                throw new InvalidDataException($"Caesars did not provide a data path for the {name} tab.");
            }

            var expectedPathPrefix = $"/v4/sports/{Sport}/competitions/{competitionId:D}/tabs/";
            if (!dataPath.StartsWith(expectedPathPrefix, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException($"Caesars returned an unexpected data path for the {name} tab.");
            }

            tabs.Add(new CaesarsTabFeed(id, name, dataPath));
        }

        if (!tabs.Any(tab => string.Equals(tab.Name, "Games", StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidDataException("Caesars did not return the NFL Games tab.");
        }

        return tabs;
    }

    public static CaesarsCompetitionFeed ParseFeed(
        Guid competitionId,
        string rawTabsPayload,
        DateTime tabsCapturedAtUtc,
        IReadOnlyList<CaesarsRawFeed> tabFeeds)
    {
        ArgumentNullException.ThrowIfNull(tabFeeds);
        var tabs = ParseTabs(competitionId, rawTabsPayload);
        var feeds = new List<CaesarsRawFeed>
        {
            new("tabs", rawTabsPayload, tabsCapturedAtUtc)
        };
        var feedsByName = tabFeeds.ToDictionary(feed => feed.FeedName, StringComparer.OrdinalIgnoreCase);
        foreach (var tab in tabs)
        {
            if (!feedsByName.TryGetValue(tab.Name, out var feed))
            {
                throw new InvalidDataException($"Caesars did not return data for the {tab.Name} tab.");
            }

            feeds.Add(feed);
        }

        var events = new Dictionary<string, CaesarsEventFeed>(StringComparer.Ordinal);
        var markets = new Dictionary<(string EventId, string MarketId), CaesarsMarketFeed>();
        var selections = new Dictionary<(string EventId, string MarketId, string SelectionId), CaesarsSelectionFeed>();
        foreach (var feed in feeds)
        {
            using var document = ParseDocument(feed.RawPayloadJson, feed.FeedName);
            ReadPayload(document.RootElement, competitionId, events, markets, selections);
        }

        return new CaesarsCompetitionFeed(
            tabs,
            events.Values.ToArray(),
            markets.Values.ToArray(),
            selections.Values.ToArray(),
            feeds);
    }

    public static string SerializeRawFeed(CaesarsRawFeed feed)
    {
        ArgumentNullException.ThrowIfNull(feed);
        using var document = ParseDocument(feed.RawPayloadJson, feed.FeedName);
        return JsonSerializer.Serialize(new
        {
            tabId = feed.FeedName,
            payload = document.RootElement
        });
    }

    private static void ReadPayload(
        JsonElement root,
        Guid requestedCompetitionId,
        IDictionary<string, CaesarsEventFeed> events,
        IDictionary<(string EventId, string MarketId), CaesarsMarketFeed> markets,
        IDictionary<(string EventId, string MarketId, string SelectionId), CaesarsSelectionFeed> selections)
    {
        foreach (var competition in GetArray(root, "competitions"))
        {
            var competitionId = ReadString(competition, "id");
            if (!Guid.TryParse(competitionId, out var parsedCompetitionId)
                || parsedCompetitionId != requestedCompetitionId)
            {
                continue;
            }

            foreach (var eventItem in GetArray(competition, "events"))
            {
                var eventId = RequireString(eventItem, "id", "event");
                var eventData = new CaesarsEventFeed(
                    eventId,
                    ReadString(eventItem, "name"),
                    ReadDateTimeUtc(ReadString(eventItem, "startTime") ?? ReadString(eventItem, "startDate")),
                    ReadString(eventItem, "status"),
                    eventItem.GetRawText());
                events.TryAdd(eventId, eventData);

                foreach (var group in GetArray(eventItem, "keyMarketGroups"))
                {
                    foreach (var marketItem in GetArray(group, "markets"))
                    {
                        var marketId = RequireString(marketItem, "id", "market");
                        var metadata = GetProperty(marketItem, "metadata");
                        markets[(eventId, marketId)] = new CaesarsMarketFeed(
                            eventId,
                            marketId,
                            ReadString(marketItem, "type"),
                            ReadString(metadata, "marketCategory") ?? ReadString(metadata, "marketCode"),
                            CleanDisplayName(ReadString(marketItem, "displayName") ?? ReadString(marketItem, "name")),
                            ReadString(metadata, "flowSegment"),
                            ReadString(metadata, "period"),
                            marketItem.GetRawText());

                        foreach (var selectionItem in GetArray(marketItem, "selections"))
                        {
                            var selectionId = RequireString(selectionItem, "id", "selection");
                            var selectionMarketId = ReadString(selectionItem, "marketId");
                            if (selectionMarketId is not null
                                && !string.Equals(selectionMarketId, marketId, StringComparison.Ordinal))
                            {
                                throw new InvalidDataException(
                                    $"Caesars selection '{selectionId}' references a different market.");
                            }

                            var price = GetProperty(selectionItem, "price");
                            var selectionMetadata = GetProperty(selectionItem, "metadata");
                            var status = ReadString(selectionItem, "status");
                            var isSuspended = ReadBoolean(selectionItem, "isSuspended")
                                ?? string.Equals(status, "suspended", StringComparison.OrdinalIgnoreCase)
                                || ReadBoolean(selectionItem, "active") == false;
                            selections[(eventId, marketId, selectionId)] = new CaesarsSelectionFeed(
                                eventId,
                                marketId,
                                selectionId,
                                ReadString(selectionItem, "type") ?? ReadString(selectionItem, "outcomeType"),
                                CleanDisplayName(ReadString(selectionItem, "displayName") ?? ReadString(selectionItem, "name")),
                                ReadString(selectionItem, "sourceParticipantId")
                                    ?? ReadString(selectionItem, "participantId")
                                    ?? ReadString(selectionItem, "playerId")
                                    ?? ReadString(selectionMetadata, "participantId")
                                    ?? ReadString(selectionMetadata, "playerId"),
                                ReadString(selectionItem, "participantName")
                                    ?? ReadString(selectionItem, "playerName")
                                    ?? ReadString(selectionItem, "teamName"),
                                ReadNumber(selectionItem, "line")
                                    ?? ReadNumber(marketItem, "line")
                                    ?? ReadNumber(metadata, "line"),
                                ReadInteger(price, "a"),
                                ReadNumber(price, "d"),
                                isSuspended,
                                status,
                                selectionItem.GetRawText());
                        }
                    }
                }
            }
        }
    }

    private static JsonDocument ParseDocument(string rawPayload, string feedName)
    {
        if (string.IsNullOrWhiteSpace(rawPayload))
        {
            throw new InvalidDataException($"Caesars returned an empty payload for the {feedName} feed.");
        }

        return JsonDocument.Parse(rawPayload);
    }

    private static void ValidateCompetitionId(Guid competitionId)
    {
        if (competitionId == Guid.Empty)
        {
            throw new ArgumentException("A valid competition ID is required.", nameof(competitionId));
        }
    }

    private static string RequireString(JsonElement element, string propertyName, string entityName) =>
        ReadString(element, propertyName)
            ?? throw new InvalidDataException($"Caesars returned a {entityName} without an ID.");

    private static string? ReadString(JsonElement element, string propertyName)
    {
        var value = GetProperty(element, propertyName);
        return value.ValueKind switch
        {
            JsonValueKind.String => value.GetString(),
            JsonValueKind.Number => value.GetRawText(),
            _ => null
        };
    }

    private static JsonElement GetProperty(JsonElement element, string propertyName) =>
        element.ValueKind == JsonValueKind.Object
            && element.TryGetProperty(propertyName, out var value)
                ? value
                : default;

    private static JsonElement[] GetArray(JsonElement element, string propertyName)
    {
        var value = GetProperty(element, propertyName);
        return value.ValueKind == JsonValueKind.Array
            ? value.EnumerateArray().ToArray()
            : [];
    }

    private static decimal? ReadNumber(JsonElement element, string propertyName)
    {
        var value = GetProperty(element, propertyName);
        if (value.ValueKind == JsonValueKind.Number && value.TryGetDecimal(out var number))
        {
            return number;
        }

        return value.ValueKind == JsonValueKind.String
            && decimal.TryParse(
                value.GetString(),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out number)
                    ? number
                    : null;
    }

    private static int? ReadInteger(JsonElement element, string propertyName)
    {
        var value = GetProperty(element, propertyName);
        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt32(out var number))
        {
            return number;
        }

        return value.ValueKind == JsonValueKind.String
            && int.TryParse(value.GetString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out number)
                ? number
                : null;
    }

    private static bool? ReadBoolean(JsonElement element, string propertyName)
    {
        var value = GetProperty(element, propertyName);
        return value.ValueKind switch
        {
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            _ => null
        };
    }

    private static DateTime? ReadDateTimeUtc(string? value) =>
        DateTimeOffset.TryParse(
            value,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
            out var parsed)
                ? parsed.UtcDateTime
                : null;

    private static string? CleanDisplayName(string? value) =>
        string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim().Trim('|').Trim();
}
