using Antlers.Bovada.Models;
using RestSharp;

namespace Antlers.Bovada;

public sealed class BovadaApiClient : IDisposable
{
    private const string NflEventsPath =
        "/services/sports/event/v2/events/A/description/football/nfl?lang=en";

    private readonly RestClient _client = new("https://services.bovada.lv");

    public void Dispose() => _client.Dispose();

    public async Task<BovadaNflOddsResponse> GetNflOddsAsync(
        CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(NflEventsPath);
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Referer", "https://www.bovada.lv/sports/football/nfl");

        var response = await _client.ExecuteAsync(request, cancellationToken);
        if (!response.IsSuccessful)
        {
            throw new HttpRequestException(
                $"Bovada returned HTTP {(int)response.StatusCode} for the NFL odds feed.",
                null,
                response.StatusCode);
        }

        if (string.IsNullOrWhiteSpace(response.Content))
        {
            throw new InvalidDataException("Bovada returned an empty NFL odds payload.");
        }

        var capturedAtUtc = DateTime.UtcNow;
        var result = BovadaFeedParser.ParseNflOdds(response.Content, capturedAtUtc);
        result.RawPayloadJson = response.Content;
        return result;
    }
}
