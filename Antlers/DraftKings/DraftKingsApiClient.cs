using Antlers.DraftKings.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Antlers.DraftKings
{
    class DraftKingsApiClient
    {
        private RestClient _client;

        /// <summary>
        /// Creates a new instance of the DraftKingsApiClient
        /// </summary>
        public DraftKingsApiClient()
        {
            _client = new RestClient("https://sportsbook-nash.draftkings.com/api/sportscontent/dkusia/v1");
        }

        /// <summary>
        /// Function to return a top level data and bets for a given league ID.
        /// </summary>
        /// <param name="leagueId">The ID of the league to retrieve: 88808 for NFL</param>
        /// <returns>Returns a league object for the specified league</returns>
        public async Task<League> GetLeague(long leagueId)
        {
            var request = new RestRequest($"/leagues/{leagueId}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<League>(response.Content) ?? new League();
        }
    }
}
