using System.Globalization;
using System.Text.Json;

namespace Antlers.FanDuel.Models;

public static class FanDuelMarketsFeedParser
{
    private static readonly string[] CoreTabNames = ["Games", "Anytime TDs", "Player Props"];

    public static FanDuelContentPage ParseContentPage(
        string rawPayload,
        string feedName = "content-page",
        DateTime? capturedAtUtc = null)
    {
        ValidatePayload(rawPayload, feedName);
        using var document = JsonDocument.Parse(rawPayload);
        var payload = new FanDuelFeedPayload(feedName, rawPayload, capturedAtUtc ?? DateTime.UtcNow);
        var root = document.RootElement;
        var (markets, tabs) = ReadCoreMarkets(root);
        var events = ReadEvents(root, markets.Values);
        return new FanDuelContentPage(tabs, events, markets, payload);
    }

    public static FanDuelMarketPriceBatch ParseMarketPrices(
        string rawPayload,
        string feedName,
        DateTime? capturedAtUtc = null)
    {
        ValidatePayload(rawPayload, feedName);
        using var document = JsonDocument.Parse(rawPayload);
        var payload = new FanDuelFeedPayload(feedName, rawPayload, capturedAtUtc ?? DateTime.UtcNow);
        var prices = new Dictionary<string, FanDuelMarketPriceData>(StringComparer.Ordinal);
        foreach (var priceMarket in ReadMarketPriceEntries(document.RootElement))
        {
            var marketId = RequireString(priceMarket, "marketId", "price market");
            var runners = GetArray(priceMarket, "runnerDetails")
                .Select(runner => (Runner: runner, SelectionId: ReadString(runner, "selectionId")))
                .Where(item => item.SelectionId is not null)
                .ToDictionary(
                    item => item.SelectionId!,
                    item => ReadPriceRunner(item.Runner),
                    StringComparer.Ordinal);
            prices[marketId] = new FanDuelMarketPriceData(
                ReadString(priceMarket, "marketStatus"),
                runners);
        }

        return new FanDuelMarketPriceBatch(prices, payload);
    }

    public static FanDuelMarketsFeed BuildMarketsFeed(
        FanDuelContentPage content,
        IEnumerable<FanDuelMarketPriceBatch> priceBatches)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(priceBatches);

        var payloads = new List<FanDuelFeedPayload> { content.Payload };
        var pricesByMarketId = new Dictionary<string, FanDuelMarketPriceData>(StringComparer.Ordinal);
        foreach (var batch in priceBatches)
        {
            payloads.Add(batch.Payload);
            foreach (var priceMarket in batch.PricesByMarketId)
            {
                pricesByMarketId[priceMarket.Key] = priceMarket.Value;
            }
        }

        if (pricesByMarketId.Count == 0)
        {
            throw new InvalidDataException("FanDuel returned no prices for the selected NFL markets.");
        }

        var selections = ReadSelections(content.Markets, pricesByMarketId);
        if (content.Events.Count == 0 || selections.Count == 0)
        {
            throw new InvalidDataException("FanDuel returned no NFL events or selections for the selected tabs.");
        }

        return new FanDuelMarketsFeed(
            content.TabNames,
            content.Events,
            content.Markets,
            selections,
            payloads);
    }

    public static IReadOnlyDictionary<(string EventId, string MarketId, string SelectionId), FanDuelSelectionData>
        ReadSelections(
            IReadOnlyDictionary<string, FanDuelMarketData> markets,
            IReadOnlyDictionary<string, FanDuelMarketPriceData> pricesByMarketId)
    {
        var selections =
            new Dictionary<(string EventId, string MarketId, string SelectionId), FanDuelSelectionData>();
        foreach (var marketData in markets.Values)
        {
            using var marketDocument = JsonDocument.Parse(marketData.MetadataJson);
            var market = marketDocument.RootElement;
            var runners = GetArray(market, "runners");
            var hasPrices = pricesByMarketId.TryGetValue(marketData.ExternalMarketId, out var priceMarket);

            foreach (var runner in runners)
            {
                var selectionId = RequireString(runner, "selectionId", "runner");
                FanDuelPriceRunnerData? priceRunner = null;
                var hasPriceRunner = hasPrices
                    && priceMarket?.RunnersBySelectionId.TryGetValue(selectionId, out priceRunner) == true;
                var runnerStatus = hasPriceRunner
                    ? priceRunner?.RunnerStatus
                    : ReadString(runner, "runnerStatus");
                var marketStatus = priceMarket?.MarketStatus ?? ReadString(market, "marketStatus");
                var status = !hasPrices
                    ? "UNAVAILABLE"
                    : !hasPriceRunner
                        ? "UNAVAILABLE"
                        : runnerStatus ?? marketStatus ?? "UNKNOWN";
                var isSuspended = !hasPrices
                    || !hasPriceRunner
                    || !string.Equals(marketStatus, "OPEN", StringComparison.OrdinalIgnoreCase)
                    || !string.Equals(runnerStatus, "ACTIVE", StringComparison.OrdinalIgnoreCase);
                var runnerName = ReadString(runner, "runnerName");
                var participantName = GetParticipantName(
                    marketData.MarketType,
                    marketData.Name,
                    runnerName);

                selections[(marketData.EventId, marketData.ExternalMarketId, selectionId)] =
                    new FanDuelSelectionData(
                        marketData.EventId,
                        marketData.ExternalMarketId,
                        selectionId,
                        GetOutcomeKey(marketData.MarketType, runnerName),
                        runnerName,
                        participantName,
                        GetParticipantSourceId(marketData.MarketType, participantName),
                        ReadNumber(runner, "handicap"),
                        priceRunner?.AmericanOdds,
                        priceRunner?.DecimalOdds,
                        isSuspended,
                        status,
                        priceRunner?.RawJson ?? runner.GetRawText());
            }
        }

        return selections;
    }

    private static void ValidatePayload(string rawPayload, string feedName)
    {
        if (string.IsNullOrWhiteSpace(rawPayload))
        {
            throw new InvalidDataException($"FanDuel returned an empty {feedName} payload.");
        }
    }

    private static (Dictionary<string, FanDuelMarketData> Markets, IReadOnlyList<string> Tabs) ReadCoreMarkets(
        JsonElement root)
    {
        var layout = GetProperty(root, "layout");
        var tabs = GetProperty(layout, "tabs");
        var cards = GetProperty(layout, "cards");
        var coupons = GetProperty(layout, "coupons");
        if (tabs.ValueKind != JsonValueKind.Object
            || cards.ValueKind != JsonValueKind.Object
            || coupons.ValueKind != JsonValueKind.Object)
        {
            throw new InvalidDataException("FanDuel did not return the NFL tab and coupon layout.");
        }

        var selectedMarketIds = new HashSet<string>(StringComparer.Ordinal);
        var selectedEventMarketTypes = new HashSet<(string EventId, string MarketType)>();
        var selectedCompetitionMarketTypes = new HashSet<(string CompetitionId, string MarketType)>();
        var foundTabNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var tabProperty in tabs.EnumerateObject())
        {
            var tab = tabProperty.Value;
            var tabName = ReadString(tab, "title");
            if (tabName is null || !CoreTabNames.Contains(tabName, StringComparer.OrdinalIgnoreCase))
            {
                continue;
            }

            foundTabNames.Add(tabName);
            foreach (var cardReference in GetArray(tab, "cards"))
            {
                var cardId = ReadString(cardReference, "id");
                var card = cardId is null ? default : GetProperty(cards, cardId);
                if (card.ValueKind != JsonValueKind.Object)
                {
                    throw new InvalidDataException($"FanDuel returned a missing card for the {tabName} tab.");
                }

                foreach (var couponReference in GetArray(card, "coupons"))
                {
                    var couponId = ReadString(couponReference, "id");
                    var coupon = couponId is null ? default : GetProperty(coupons, couponId);
                    if (coupon.ValueKind != JsonValueKind.Object)
                    {
                        throw new InvalidDataException($"FanDuel returned a missing coupon for the {tabName} tab.");
                    }

                    var marketTypes = GetStringValues(coupon, "marketTypes");
                    var eventIds = GetStringValues(coupon, "eventIds");
                    var competitionIds = GetStringValues(coupon, "competitionIds");
                    foreach (var eventId in eventIds)
                    {
                        foreach (var marketType in marketTypes)
                        {
                            selectedEventMarketTypes.Add((eventId, marketType));
                        }
                    }

                    foreach (var competitionId in competitionIds)
                    {
                        foreach (var marketType in marketTypes)
                        {
                            selectedCompetitionMarketTypes.Add((competitionId, marketType));
                        }
                    }

                    foreach (var display in GetArray(coupon, "display"))
                    {
                        foreach (var row in GetArray(display, "rows"))
                        {
                            foreach (var marketId in GetStringValues(row, "marketIds"))
                            {
                                selectedMarketIds.Add(marketId);
                            }
                        }
                    }
                }
            }
        }

        var missingTabs = CoreTabNames.Where(name => !foundTabNames.Contains(name)).ToArray();
        if (missingTabs.Length > 0)
        {
            throw new InvalidDataException(
                $"FanDuel did not return the requested NFL tabs: {string.Join(", ", missingTabs)}.");
        }

        var attachedMarkets = GetProperty(GetProperty(root, "attachments"), "markets");
        if (attachedMarkets.ValueKind != JsonValueKind.Object)
        {
            throw new InvalidDataException("FanDuel did not return NFL market attachments.");
        }

        var markets = new Dictionary<string, FanDuelMarketData>(StringComparer.Ordinal);
        foreach (var marketProperty in attachedMarkets.EnumerateObject())
        {
            var market = marketProperty.Value;
            var marketId = ReadString(market, "marketId") ?? marketProperty.Name;
            var eventId = ReadString(market, "eventId");
            var competitionId = ReadString(market, "competitionId");
            var marketType = ReadString(market, "marketType");
            var selected = selectedMarketIds.Contains(marketId)
                || eventId is not null
                    && marketType is not null
                    && selectedEventMarketTypes.Contains((eventId, marketType))
                || competitionId is not null
                    && marketType is not null
                    && selectedCompetitionMarketTypes.Contains((competitionId, marketType));
            if (!selected || string.IsNullOrWhiteSpace(eventId))
            {
                continue;
            }

            var marketName = ReadString(market, "marketName");
            markets[marketId] = new FanDuelMarketData(
                eventId,
                marketId,
                marketType,
                GetMarketKey(marketType),
                GetMarketScope(marketType),
                marketName,
                market.GetRawText());
        }

        return (markets, CoreTabNames);
    }

    private static Dictionary<string, FanDuelEventData> ReadEvents(
        JsonElement root,
        IEnumerable<FanDuelMarketData> markets)
    {
        var neededEventIds = markets.Select(market => market.EventId)
            .ToHashSet(StringComparer.Ordinal);
        var attachedEvents = GetProperty(GetProperty(root, "attachments"), "events");
        if (attachedEvents.ValueKind != JsonValueKind.Object)
        {
            throw new InvalidDataException("FanDuel did not return NFL event attachments.");
        }

        var events = new Dictionary<string, FanDuelEventData>(StringComparer.Ordinal);
        foreach (var eventProperty in attachedEvents.EnumerateObject())
        {
            var eventItem = eventProperty.Value;
            var eventId = ReadString(eventItem, "eventId") ?? eventProperty.Name;
            if (!neededEventIds.Contains(eventId))
            {
                continue;
            }

            events[eventId] = new FanDuelEventData(
                eventId,
                ReadString(eventItem, "name"),
                ReadDateTimeUtc(ReadString(eventItem, "openDate")),
                ReadString(eventItem, "status"),
                eventItem.GetRawText());
        }

        var missingEventIds = neededEventIds.Where(eventId => !events.ContainsKey(eventId)).ToArray();
        if (missingEventIds.Length > 0)
        {
            throw new InvalidDataException(
                $"FanDuel returned markets without matching event details: {string.Join(", ", missingEventIds)}.");
        }

        return events;
    }

    private static IEnumerable<JsonElement> ReadMarketPriceEntries(JsonElement root)
    {
        if (root.ValueKind == JsonValueKind.Array)
        {
            return root.EnumerateArray().ToArray();
        }

        if (root.ValueKind == JsonValueKind.Object
            && GetProperty(root, "marketId").ValueKind != JsonValueKind.Undefined)
        {
            return [root];
        }

        throw new InvalidDataException("FanDuel returned an unexpected market-price payload.");
    }

    private static FanDuelPriceRunnerData ReadPriceRunner(JsonElement runner)
    {
        var odds = GetProperty(runner, "winRunnerOdds");
        return new FanDuelPriceRunnerData(
            ReadString(runner, "runnerStatus"),
            ReadInteger(GetProperty(odds, "americanDisplayOdds"), "americanOddsInt")
                ?? ReadInteger(GetProperty(odds, "americanDisplayOdds"), "americanOdds"),
            ReadNumber(GetProperty(odds, "decimalDisplayOdds"), "decimalOdds")
                ?? ReadNumber(GetProperty(GetProperty(odds, "trueOdds"), "decimalOdds"), "decimalOdds"),
            runner.GetRawText());
    }

    private static string RequireString(JsonElement element, string propertyName, string entityName) =>
        ReadString(element, propertyName)
            ?? throw new InvalidDataException($"FanDuel returned a {entityName} without {propertyName}.");

    private static string? ReadString(JsonElement element, string propertyName) =>
        ReadPrimitiveString(GetProperty(element, propertyName));

    private static string? ReadPrimitiveString(JsonElement value) =>
        value.ValueKind switch
        {
            JsonValueKind.String => value.GetString(),
            JsonValueKind.Number => value.GetRawText(),
            _ => null
        };

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

    private static string[] GetStringValues(JsonElement element, string propertyName) =>
        GetArray(element, propertyName)
            .Select(ReadPrimitiveString)
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => value!)
            .ToArray();

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
        var number = ReadNumber(element, propertyName);
        return number.HasValue && decimal.Truncate(number.Value) == number.Value
            ? decimal.ToInt32(number.Value)
            : null;
    }

    private static DateTime? ReadDateTimeUtc(string? value) =>
        DateTimeOffset.TryParse(
            value,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
            out var parsed)
                ? parsed.UtcDateTime
                : null;

    private static string? GetMarketKey(string? marketType) =>
        marketType?.ToUpperInvariant() switch
        {
            "MATCH_HANDICAP_(2-WAY)" => "spread",
            "MONEY_LINE" => "moneyline",
            "TOTAL_POINTS_(OVER/UNDER)" => "total_points",
            "ANY_TIME_TOUCHDOWN_SCORER" => "anytime_td",
            "PLAYER_X_ALT_PASSING_YARDS_HIGH" => "passing_yards",
            "PLAYER_X_ALT_RECEIVING_YARDS_HIGH" => "receiving_yards",
            _ => marketType?.ToLowerInvariant()
        };

    private static string? GetMarketScope(string? marketType) =>
        marketType?.StartsWith("PLAYER_", StringComparison.OrdinalIgnoreCase) == true
        || string.Equals(marketType, "ANY_TIME_TOUCHDOWN_SCORER", StringComparison.OrdinalIgnoreCase)
            ? "player"
            : "game";

    private static string? GetOutcomeKey(string? marketType, string? runnerName)
    {
        if (string.IsNullOrWhiteSpace(runnerName))
        {
            return null;
        }

        if (string.Equals(marketType, "TOTAL_POINTS_(OVER/UNDER)", StringComparison.OrdinalIgnoreCase))
        {
            return runnerName.Contains("over", StringComparison.OrdinalIgnoreCase)
                ? "over"
                : runnerName.Contains("under", StringComparison.OrdinalIgnoreCase)
                    ? "under"
                    : runnerName;
        }

        return runnerName;
    }

    private static string? GetParticipantName(string? marketType, string? marketName, string? runnerName)
    {
        if (string.Equals(marketType, "ANY_TIME_TOUCHDOWN_SCORER", StringComparison.OrdinalIgnoreCase)
            || string.Equals(marketType, "MONEY_LINE", StringComparison.OrdinalIgnoreCase)
            || string.Equals(marketType, "MATCH_HANDICAP_(2-WAY)", StringComparison.OrdinalIgnoreCase))
        {
            return runnerName;
        }

        if (marketType?.StartsWith("PLAYER_", StringComparison.OrdinalIgnoreCase) != true
            || string.IsNullOrWhiteSpace(marketName))
        {
            return null;
        }

        var separator = marketName.IndexOf(" - ", StringComparison.Ordinal);
        return separator > 0 ? marketName[..separator] : marketName;
    }

    private static string? GetParticipantSourceId(string? marketType, string? participantName)
    {
        if (string.IsNullOrWhiteSpace(participantName))
        {
            return null;
        }

        var participantType = marketType?.StartsWith("PLAYER_", StringComparison.OrdinalIgnoreCase) == true
            || string.Equals(marketType, "ANY_TIME_TOUCHDOWN_SCORER", StringComparison.OrdinalIgnoreCase)
                ? "player"
                : string.Equals(marketType, "MONEY_LINE", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(marketType, "MATCH_HANDICAP_(2-WAY)", StringComparison.OrdinalIgnoreCase)
                    ? "team"
                    : null;

        return participantType is null
            ? null
            : $"name:{participantType}:{participantName.Trim()}";
    }
}
