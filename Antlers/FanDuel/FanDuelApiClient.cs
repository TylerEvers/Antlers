using RestSharp;
using Antlers.FanDuel.Models;

namespace Antlers.FanDuel;

public sealed class FanDuelApiClient : IDisposable
{
    private const string ContentBaseUrl = "https://api.sportsbook.fanduel.com";
    private const string PriceBaseUrl = "https://smp.{0}.sportsbook.fanduel.com";
    private const string MarketPricesPath = "/api/sports/fixedodds/readonly/v1/getMarketPrices";
    private const string SportsbookOrigin = "https://sportsbook.fanduel.com";
    private const string UserAgent =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:159.0) Gecko/20100101 Firefox/159.0";
    private const string ContentPagePath =
        "/sbapi/content-managed-page?page=CUSTOM&customPageId=nfl&pbHorizontal=false&_ak={0}&timezone=America%2FChicago";
    public const string PublicApplicationId = "FhMFpcPWXMeyZxOx";

    private readonly RestClient _contentClient = new(ContentBaseUrl);
    private readonly string _applicationId;

    public void Dispose() => _contentClient.Dispose();

    public FanDuelApiClient()
        : this(PublicApplicationId)
    {
    }

    public FanDuelApiClient(string applicationId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationId);

        _applicationId = applicationId;
    }

    public async Task<string> GetMarketPrices(
        IEnumerable<string> marketIds,
        CancellationToken cancellationToken = default)
        => await GetMarketPrices("US-IA", marketIds, cancellationToken);

    public async Task<string> GetContentPage(
        string jurisdiction,
        CancellationToken cancellationToken = default)
    {
        var state = GetState(jurisdiction);
        var request = new RestRequest(
            string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                ContentPagePath,
                Uri.EscapeDataString(_applicationId)),
            Method.Get);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Accept-Language", "en-US,en;q=0.9");
        request.AddHeader("User-Agent", UserAgent);
        request.AddHeader("x-sportsbook-region", state);
        request.AddHeader("Origin", SportsbookOrigin);
        request.AddHeader("Referer", $"{SportsbookOrigin}/");
        request.AddHeader("Sec-Fetch-Dest", "empty");
        request.AddHeader("Sec-Fetch-Mode", "cors");
        request.AddHeader("Sec-Fetch-Site", "same-site");

        return await ExecuteAsync(_contentClient, request, "content-page", cancellationToken);
    }

    public async Task<string> GetMarketPrices(
        string jurisdiction,
        IEnumerable<string> marketIds,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(marketIds);
        var state = GetState(jurisdiction);

        var ids = marketIds.ToArray();
        if (ids.Length == 0)
        {
            throw new ArgumentException("At least one market ID is required.", nameof(marketIds));
        }

        if (ids.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("Market IDs cannot be empty.", nameof(marketIds));
        }

        var request = new RestRequest(MarketPricesPath, Method.Post);
        request.AddQueryParameter("priceHistory", "0");
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Accept-Language", "en-US,en;q=0.9");
        request.AddHeader("User-Agent", UserAgent);
        request.AddHeader("Origin", SportsbookOrigin);
        request.AddHeader("Referer", $"{SportsbookOrigin}/");
        request.AddHeader("X-Application", _applicationId);
        request.AddJsonBody(new { marketIds = ids });

        using var client = new RestClient(string.Format(
            System.Globalization.CultureInfo.InvariantCulture,
            PriceBaseUrl,
            state.ToLowerInvariant()));
        return await ExecuteAsync(client, request, "market prices", cancellationToken);
    }

    public async Task<FanDuelContentPage> GetContentPageDataAsync(
        string jurisdiction,
        CancellationToken cancellationToken = default)
        => FanDuelMarketsFeedParser.ParseContentPage(
            await GetContentPage(jurisdiction, cancellationToken));

    public async Task<FanDuelMarketPriceBatch> GetMarketPriceBatchAsync(
        string jurisdiction,
        IEnumerable<string> marketIds,
        string feedName,
        CancellationToken cancellationToken = default)
        => FanDuelMarketsFeedParser.ParseMarketPrices(
            await GetMarketPrices(jurisdiction, marketIds, cancellationToken),
            feedName);

    private static async Task<string> ExecuteAsync(
        RestClient client,
        RestRequest request,
        string feedName,
        CancellationToken cancellationToken)
    {
        var response = await client.ExecuteAsync(request, cancellationToken);
        if (!response.IsSuccessful)
        {
            throw new HttpRequestException(
                $"FanDuel returned HTTP {(int)response.StatusCode} for the {feedName} feed.",
                null,
                response.StatusCode);
        }

        if (string.IsNullOrWhiteSpace(response.Content))
        {
            throw new InvalidDataException($"FanDuel returned an empty {feedName} response.");
        }

        return response.Content;
    }

    private static string GetState(string jurisdiction)
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

        return normalized[3..];
    }
}
