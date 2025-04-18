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
    }
}
