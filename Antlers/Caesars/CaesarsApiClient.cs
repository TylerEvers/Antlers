using RestSharp;
using Antlers.Caesars.Models;

namespace Antlers.Caesars;

public sealed class CaesarsApiClient : IDisposable
{
    private const string ApiBaseUrl = "https://api.americanwagering.com";
    private const string Sport = "americanfootball";
    private const string BrandPath = "/brands/czr/sb";
    private static readonly HashSet<string> AllowedTabNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "Games",
        "TD Scorer",
        "Passing",
        "Rushing",
        "Receiving",
        "Game Specials"
    };

    private readonly RestClient _client = new(ApiBaseUrl);

    public void Dispose() => _client.Dispose();

    public async Task<CaesarsCompetitionFeed> GetCompetitionFeedAsync(
        Guid competitionId,
        string jurisdiction,
        CancellationToken cancellationToken = default)
    {
        ValidateCompetitionId(competitionId);
        var state = ValidateJurisdiction(jurisdiction);
        var tabsPath = $"/v4/sports/{Sport}/competitions/{competitionId:D}/tabs";
        var rawTabsPayload = await GetAsync(BuildUri(state, tabsPath), "tabs", cancellationToken);
        var tabsCapturedAtUtc = DateTime.UtcNow;
        var tabs = CaesarsFeedParser.ParseTabs(competitionId, rawTabsPayload);
        var tabFeeds = new List<CaesarsRawFeed>(tabs.Count);

        foreach (var tab in tabs)
        {
            var rawPayload = await GetTabData(
                competitionId,
                state,
                tab.DataPath,
                tab.Name,
                cancellationToken);
            tabFeeds.Add(new CaesarsRawFeed(tab.Name, rawPayload, DateTime.UtcNow));
        }

        return CaesarsFeedParser.ParseFeed(
            competitionId,
            rawTabsPayload,
            tabsCapturedAtUtc,
            tabFeeds);
    }

    public async Task<string> GetCompetitionTabs(
        Guid competitionId,
        string jurisdiction,
        CancellationToken cancellationToken = default)
    {
        ValidateCompetitionId(competitionId);
        var state = ValidateJurisdiction(jurisdiction);
        var path = $"/v4/sports/{Sport}/competitions/{competitionId:D}/tabs";
        return await GetAsync(BuildUri(state, path), "tabs", cancellationToken);
    }

    public async Task<string> GetTabData(
        Guid competitionId,
        string jurisdiction,
        string dataPath,
        string tabName,
        CancellationToken cancellationToken = default)
    {
        ValidateCompetitionId(competitionId);
        var state = ValidateJurisdiction(jurisdiction);
        ArgumentException.ThrowIfNullOrWhiteSpace(dataPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(tabName);

        var expectedPrefix = $"/v4/sports/{Sport}/competitions/{competitionId:D}/tabs/";
        if (!dataPath.StartsWith(expectedPrefix, StringComparison.OrdinalIgnoreCase)
            || dataPath.Contains("..", StringComparison.Ordinal)
            || dataPath.Contains('\\')
            || dataPath.Contains('#')
            || !AllowedTabNames.Contains(tabName))
        {
            throw new ArgumentException("The Caesars tab path or name is invalid.", nameof(dataPath));
        }

        return await GetAsync(
            BuildUri(state, dataPath),
            Uri.UnescapeDataString(tabName),
            cancellationToken);
    }

    private async Task<string> GetAsync(
        Uri uri,
        string feedName,
        CancellationToken cancellationToken)
    {
        var request = new RestRequest(uri.PathAndQuery);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Referer", "https://sportsbook.caesars.com/");
        request.AddHeader("x-platform", "rn-web");

        var response = await _client.ExecuteAsync(request, cancellationToken);
        if (!response.IsSuccessful)
        {
            throw new HttpRequestException(
                $"Caesars returned HTTP {(int)response.StatusCode} for the {feedName} feed.",
                null,
                response.StatusCode);
        }

        if (string.IsNullOrWhiteSpace(response.Content))
        {
            throw new InvalidDataException($"Caesars returned an empty payload for the {feedName} feed.");
        }

        return response.Content;
    }

    internal static Uri BuildUri(string jurisdiction, string path) =>
        new(new Uri(ApiBaseUrl), $"/regions/us/locations/{jurisdiction[3..].ToLowerInvariant()}{BrandPath}{path}");

    private static string ValidateJurisdiction(string jurisdiction)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(jurisdiction);
        var normalized = jurisdiction.Trim().ToUpperInvariant();
        if (normalized.Length != 5
            || !normalized.StartsWith("US-", StringComparison.Ordinal)
            || normalized[3] is < 'A' or > 'Z'
            || normalized[4] is < 'A' or > 'Z')
        {
            throw new ArgumentException("Jurisdiction must use the US-XX format.", nameof(jurisdiction));
        }

        return normalized;
    }

    private static void ValidateCompetitionId(Guid competitionId)
    {
        if (competitionId == Guid.Empty)
        {
            throw new ArgumentException("A valid competition ID is required.", nameof(competitionId));
        }
    }
}
