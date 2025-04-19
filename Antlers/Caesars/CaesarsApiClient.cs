using Antlers.Caesars.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Antlers.Caesars
{
    public class CaesarsApiClient
    {
        private RestClient _client;

        /// <summary>
        /// Creates a new instance of the CaesarsApiClient
        /// </summary>
        public CaesarsApiClient()
        {
            _client = new RestClient("https://api.americanwagering.com/regions/us/locations/co/brands/czr/sb/");

            _client.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:137.0) Gecko/20100101 Firefox/137.0"); //TODO: Figure out how to set this dynamically? nuget package?
            _client.AddDefaultHeader("x-app-version", "7.22.0"); //TODO: How to get this dynamically?
            _client.AddDefaultHeader("x-aws-waf-token", GetToken());
            _client.AddDefaultHeader("x-platform", "cordova-desktop");
            _client.AddDefaultHeader("x-unique-device-id", Guid.NewGuid().ToString());
        }

        private string GetToken()
        {
            var client = new RestClient();

            var request = new RestRequest($"https://sportsbook.caesars.com/us/co/bet/");
            var response = client.Execute(request);

            if (response == null || response.Cookies == null)
            {
                throw new Exception("Caesars AuthUrl returned an empty response");
            }

            return response.Cookies.First(x => x.Name == "aws-waf-token").Value;
        }

        /// <summary>
        /// Function to return sports menus, used to populate sport guids for subsequent calls.
        /// </summary>
        /// <returns>Returns the sports menu to gather sport names and Ids</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<Response> GetSportsMenu()
        {
            var request = new RestRequest("v3/sports-menu", Method.Get);
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
        /// Function to return tabs for a given sport and sport ID.
        /// </summary>
        /// <param name="sportName">The name of the sport to query</param>
        /// <param name="sportId">The id of the sport to query</param>
        /// <returns>Returns Caesars tabs for specified sport</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<Response> GetSportTabs(string sportName, string sportId)
        {
            var request = new RestRequest($"v4/sports/{sportName}/competitions/{sportId}/tabs", Method.Get);
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
        /// Function to return competitions for a given sport and tab ID.
        /// </summary>
        /// <param name="sportName">The name of the sport to query</param>
        /// <param name="sportId">The id of the sport to query</param>
        /// <param name="tabId">The id of the tab to query</param>
        /// <returns>Returns Caesars competitions for a given sport and tab</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<Response> GetTabCompetitions(string sportName, string sportId, string tabId)
        {
            var request = new RestRequest($"v4/sports/{sportName}/competitions/{sportId}/tabs/{tabId}", Method.Get);
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
