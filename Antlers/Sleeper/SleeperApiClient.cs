using RestSharp;
using Newtonsoft.Json;
using Antlers.Sleeper.Models;

namespace Antlers.Sleeper
{
    public class SleeperApiClient
    {
        private const string _baseUri = "https://api.sleeper.app/v1";
        private RestClient _client;

        /// <summary>
        /// Creates a new instance of the SleeperApiClient
        /// </summary>
        public SleeperApiClient()
        {
            _client = new RestClient(_baseUri);
        }

        /// <summary>
        /// Function to return a specific league based on League Id
        /// Link: https://docs.sleeper.com/#get-a-specific-league
        /// </summary>
        /// <param name="leagueId">The ID of the league to retrieve</param>
        /// <returns>Returns a league object for the specified league</returns>
        public League GetLeague(int leagueId)
        {
            var request = new RestRequest($"/league/{leagueId}", Method.Get);
            var response = _client.Execute(request);
            return JsonConvert.DeserializeObject<League>(response.Content) ?? new League();
        }

        /// <summary>
        /// Function to return all players from Sleeper (Limit calls to once per day at most to keep your player IDs updated. The average size of this query is 5MB.)
        /// Link: https://docs.sleeper.com/#players
        /// </summary>
        /// <param name="sport">Currently only supports "nfl".</param>
        /// <returns>Returns a list of the Player object containing all players of the provided sport</returns>
        public IEnumerable<Player> GetPlayers(string sport)
        {
            var request = new RestRequest($"/players/{sport}", Method.Get);
            var response = _client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Player>>(response.Content) ?? new List<Player>();
        }


        /// <summary>
        /// Function to return Trending Player data from Sleeper (requires attribution, they also provide an embed iframe for use on websites/blogs)
        /// Link: https://docs.sleeper.com/#fetch-all-players
        /// </summary>
        /// <param name="sport">Currently only supports "nfl".</param>
        /// <param name="type">Either "add" or "drop"</param>
        /// <param name="lookbackHours">Number of hours to look back (defaults to 24)</param>
        /// <param name="limit">Number of players you want returned (defaults to 25)</param>
        /// <returns>Returns a list of the Player object containing Sleeper Trending data</returns>
        public IEnumerable<Player> GetTrendingPlayers(string sport, string type, int lookbackHours = 24, int limit = 25)
        {
            var request = new RestRequest($"/players/{sport}/trending/{type}?lookback_hours={lookbackHours}&limit={limit}", Method.Get);
            var response = _client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Player>>(response.Content) ?? new List<Player>();
        }

        /// <summary>
        /// Function to return rosters for a specific league.
        /// </summary>
        /// <param name="leagueId">ID of the league to retrieve rosters from</param>
        /// <returns>Returns a list of the Roster object</returns>
        public IEnumerable<Roster> GetRosters(string leagueId)
        {
            var request = new RestRequest($"/league/{leagueId}/rosters", Method.Get);
            var response = _client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Roster>>(response.Content) ?? new List<Roster>();
        }
    }
}
