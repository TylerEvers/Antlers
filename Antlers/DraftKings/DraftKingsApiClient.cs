using Antlers.DraftKings.Models;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Antlers.DraftKings
{
    public class DraftKingsApiClient : IDisposable
    {
        private const string LeagueSubcategoryMarketsPath =
            "/api/sportscontent/controldata/league/leagueSubcategory/v1/markets";

        private RestClient _client;
        private readonly HttpClient _marketsHttpClient;

        /// <summary>
        /// Creates a new instance of the DraftKingsApiClient
        /// </summary>
        public DraftKingsApiClient()
        {
            _client = new RestClient("https://sportsbook-nash.draftkings.com/api/sportscontent/dkusia/v1");
            _marketsHttpClient = new HttpClient(new Http2RequestHandler(new SocketsHttpHandler()))
            {
                DefaultRequestVersion = HttpVersion.Version20,
                DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher
            };
        }

        public void Dispose()
        {
            _client.Dispose();
            _marketsHttpClient.Dispose();
        }

        /// <summary>
        /// Function to return a top level data and bets for a given league ID.
        /// </summary>
        /// <param name="leagueId">The ID of the league to retrieve: 88808 for NFL</param>
        /// <returns>Returns DraftKings response object for specified league</returns>
        public async Task<Response> GetLeague(int leagueId)
        {
            var request = new RestRequest($"/leagues/{leagueId}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch response: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                return new Response();
            }

            return JsonConvert.DeserializeObject<Response>(response.Content) ?? new Response();
        }

        /// <summary>
        /// Function to return category level data and bets for a given league ID.
        /// </summary>
        /// <param name="leagueId">The ID of the league to retrieve: 88808 for NFL</param>
        /// <param name="categoryId">The ID of the category to retrieve</param>
        /// <returns>Returns a league object for the specified league</returns>
        public async Task<Response> GetCategory(int leagueId, int categoryId)
        {
            var request = new RestRequest($"/leagues/{leagueId}/categories/{categoryId}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch response: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                return new Response();
            }

            return JsonConvert.DeserializeObject<Response>(response.Content) ?? new Response();
        }

        /// <summary>
        /// Function to return subcategory level data and bets for a given league ID.
        /// </summary>
        /// <param name="leagueId">The ID of the league to retrieve: 88808 for NFL</param>
        /// <param name="categoryId">The ID of the category to retrieve</param>
        /// <param name="subcategoryId">The ID of the subcategory to retrieve</param>
        /// <returns>Returns a league object for the specified league</returns>
        public async Task<Response> GetSubcategory(int leagueId, int categoryId, int subcategoryId)
        {
            var request = new RestRequest($"/leagues/{leagueId}/categories/{categoryId}/subcategories/{subcategoryId}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch response: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                return new Response();
            }

            return JsonConvert.DeserializeObject<Response>(response.Content) ?? new Response();
        }

        /// <summary>
        /// Retrieves markets and events for a league subcategory, including game and player selections.
        /// </summary>
        /// <param name="leagueId">The league ID to retrieve: 88808 for NFL.</param>
        /// <param name="subcategoryId">The configured subcategory ID to retrieve.</param>
        /// <param name="cancellationToken">A token that can cancel the request.</param>
        public async Task<LeagueSubcategoryMarketsResponse> GetLeagueSubcategoryMarkets(
            int leagueId,
            int subcategoryId,
            CancellationToken cancellationToken = default)
        {
            return await GetLeagueSubcategoryMarkets(
                leagueId,
                subcategoryId,
                "US-IA",
                cancellationToken);
        }

        /// <summary>
        /// Retrieves markets and events for a league subcategory in the specified US jurisdiction.
        /// </summary>
        public async Task<LeagueSubcategoryMarketsResponse> GetLeagueSubcategoryMarkets(
            int leagueId,
            int subcategoryId,
            string jurisdiction,
            CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(leagueId);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(subcategoryId);
            ArgumentException.ThrowIfNullOrWhiteSpace(jurisdiction);
            jurisdiction = jurisdiction.Trim().ToUpperInvariant();
            if (jurisdiction.Length != 5
                || !jurisdiction.StartsWith("US-", StringComparison.Ordinal)
                || jurisdiction[3] is < 'A' or > 'Z'
                || jurisdiction[4] is < 'A' or > 'Z')
            {
                throw new ArgumentException(
                    "Jurisdiction must use the US-XX format.",
                    nameof(jurisdiction));
            }

            var queryParameters = new Dictionary<string, string>
            {
                ["isBatchable"] = "false",
                ["templateVars"] = leagueId.ToString(),
                ["eventsQuery"] =
                    $"$filter=leagueId eq '{leagueId}' AND clientMetadata/Subcategories/any(s: s/Id eq '{subcategoryId}')",
                ["marketsQuery"] =
                    $"$filter=clientMetadata/subCategoryId eq '{subcategoryId}' AND tags/all(t: t ne 'SportcastBetBuilder')",
                ["include"] = "Events",
                ["entity"] = "events"
            };
            var queryString = string.Join(
                "&",
                queryParameters.Select(parameter =>
                    $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"));
            var requestUri = new Uri(
                $"https://sportsbook-nash.draftkings.com/sites/{jurisdiction}-SB{LeagueSubcategoryMarketsPath}?{queryString}");
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.TryAddWithoutValidation("Accept", "*/*");
            request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9");
            request.Headers.TryAddWithoutValidation("content-type", "application/json charset=utf-8");
            request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:159.0) Gecko/20100101 Firefox/159.0");
            request.Headers.TryAddWithoutValidation("Referer", "https://sportsbook.draftkings.com/");
            request.Headers.TryAddWithoutValidation("Origin", "https://sportsbook.draftkings.com");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-site");
            request.Headers.TryAddWithoutValidation("x-client-feature", "leagueSubcategory");
            request.Headers.TryAddWithoutValidation("x-client-name", "web");
            request.Headers.TryAddWithoutValidation("x-client-page", "league");
            request.Headers.TryAddWithoutValidation("x-client-version", "2644.4.1.10");
            request.Headers.TryAddWithoutValidation("x-client-widget-name", "cms");
            request.Headers.TryAddWithoutValidation("x-client-widget-version", "3.1.1");
            request.Headers.TryAddWithoutValidation("x-pe-cn", "web");
            request.Headers.TryAddWithoutValidation("x-pe-cv", "2644.4.1.10");
            request.Headers.TryAddWithoutValidation("x-pe-ep", "SB");
            request.Headers.TryAddWithoutValidation("x-pe-loc", jurisdiction);

            using var response = await _marketsHttpClient.SendAsync(request, cancellationToken);
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Failed to fetch DraftKings league subcategory markets: {response.StatusCode} - {responseContent}",
                    null,
                    response.StatusCode);
            }

            if (string.IsNullOrWhiteSpace(responseContent))
            {
                throw new InvalidDataException("DraftKings returned an empty league subcategory markets response.");
            }

            try
            {
                var result = JsonConvert.DeserializeObject<LeagueSubcategoryMarketsResponse>(responseContent)
                    ?? throw new InvalidDataException("DraftKings returned an empty league subcategory markets response.");
                result.RawPayloadJson = responseContent;
                return result;
            }
            catch (JsonException exception)
            {
                throw new InvalidDataException("DraftKings returned an invalid league subcategory markets response.", exception);
            }
        }

        private sealed class Http2RequestHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
        {
            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                request.Version = HttpVersion.Version20;
                request.VersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
                return base.SendAsync(request, cancellationToken);
            }
        }
    }
}
