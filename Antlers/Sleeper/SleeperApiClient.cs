using RestSharp;
using Newtonsoft.Json;
using Antlers.Sleeper.Models;

namespace Antlers.Sleeper
{
    public class SleeperApiClient
    {
        private IBaseUriStrategy _baseUriStrategy;
        private RestClient _client;

        /// <summary>
        /// Creates a new instance of the SleeperApiClient
        /// </summary>
        public SleeperApiClient(IBaseUriStrategy baseUriStrategy)
        {
            _baseUriStrategy = baseUriStrategy ?? throw new ArgumentNullException(nameof(baseUriStrategy));
            _client = new RestClient(_baseUriStrategy.GetBaseUri());
        }

        /// <summary>
        /// Function to return a specific league based on League Id
        /// Link: https://docs.sleeper.com/#get-a-specific-league
        /// </summary>
        /// <param name="leagueId">The ID of the league to retrieve</param>
        /// <returns>Returns a league object for the specified league</returns>
        public async Task<League> GetLeague(long leagueId)
        {
            var request = new RestRequest($"/league/{leagueId}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<League>(response.Content) ?? new League();
        }

        /// <summary>
        /// Function to return data for a specific player based on sport and player id
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="playerId">The ID of the player to retrieve</param>
        /// <returns>Returns a player object for the specified player</returns>
        public async Task<Player> GetPlayer(string sport, int playerId)
        {
            var request = new RestRequest($"/players/{sport}/{playerId}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<Player>(response.Content) ?? new Player();
        }

        /// <summary>
        /// Function to return all players from Sleeper (Limit calls to once per day at most to keep your player IDs updated. The average size of this query is 5MB.)
        /// Link: https://docs.sleeper.com/#players
        /// </summary>
        /// <param name="sport">Currently only supports "nfl".</param>
        /// <returns>Returns a list of the Player object containing all players of the provided sport</returns>
        public async Task<IEnumerable<Player>> GetPlayers(string sport)
        {
            var request = new RestRequest($"/players/{sport}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<Player>>(response.Content) ?? new List<Player>();
        }


        /// <summary>
        /// Function to return Trending Player data from Sleeper (requires attribution, they also provide an embed iframe for use on websites/blogs)
        /// Link: https://docs.sleeper.com/#trending-players
        /// </summary>
        /// <param name="sport">Currently only supports "nfl".</param>
        /// <param name="type">Either "add" or "drop"</param>
        /// <param name="lookbackHours">Number of hours to look back (defaults to 24)</param>
        /// <param name="limit">Number of players you want returned (defaults to 25)</param>
        /// <returns>Returns a list of the Player object containing Sleeper Trending data</returns>
        public async Task<IEnumerable<Player>> GetTrendingPlayers(string sport, string type, int lookbackHours = 24, int limit = 25)
        {
            var request = new RestRequest($"/players/{sport}/trending/{type}", Method.Get);
            request.AddQueryParameter("lookback_hours", lookbackHours.ToString());
            request.AddQueryParameter("limit", limit.ToString());

            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<Player>>(response.Content) ?? new List<Player>();
        }

        /// <summary>
        /// Function to return rosters for a specific league.
        /// </summary>
        /// <param name="leagueId">ID of the league to retrieve rosters from</param>
        /// <returns>Returns a list of the Roster object</returns>
        public async Task<IEnumerable<Roster>> GetRosters(long leagueId)
        {
            var request = new RestRequest($"/league/{leagueId}/rosters", Method.Get);
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<Roster>>(response.Content) ?? new List<Roster>();
        }

        /// <summary>
        /// Function to return schedule for a specific sport, segment  and year.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl".</param>
        /// <param name="segment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <returns>Returns a list of the Schedule object</returns>
        public async Task<IEnumerable<Schedule>> GetSchedule(string sport, string segment, int year)
        {
            var request = new RestRequest($"/schedule/{sport}/{segment}/{year}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<Schedule>>(response.Content) ?? new List<Schedule>();
        }

        /// <summary>
        /// Function to return stats for a specific player based on sport, player id, season type and year.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="playerId">ID of the player to return</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <param name="grouping">Optional parameter, default returns entire season, week returns by week</param>
        /// <returns>Returns a list of the Stats object for given player</returns>
        public async Task<StatsResponse> GetPlayerStats(string sport, int playerId, int year, string seasonSegment, int? week = null, string? grouping = null)
        {
            var request = new RestRequest($"/stats/{sport}/player/{playerId}", Method.Get);
            
            request.AddQueryParameter("season", year.ToString());
            request.AddQueryParameter("season_type", seasonSegment);
            if (week != null)
            {
                request.AddQueryParameter("week", week.ToString());
            }
            if (grouping != null)
            {
                request.AddQueryParameter("grouping", grouping);
            }

            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<StatsResponse>(response.Content) ?? new StatsResponse();
        }

        /// <summary>
        /// Function to return stats for all players based on sport, season type and year.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <param name="grouping">Optional parameter, default returns entire season, week returns by week</param>
        /// <param name="ordering">Optional parameter, (pts_ppr, pts_hppr, pts_std)</param>
        /// <returns>Returns a list of the Stats object for given player</returns>
        public async Task<IEnumerable<Stats>> GetTeamStats(string sport, int year, string seasonSegment, string? ordering = null)
        {
            var request = new RestRequest($"/stats/{sport}/{year}", Method.Get);
            request.AddQueryParameter("season_type", seasonSegment);
            request.AddQueryParameter("position[]", "TEAM");
            if (ordering != null)
            {
                request.AddQueryParameter("order_by", ordering);
            }

            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<Stats>>(response.Content) ?? new List<Stats>();
        }

        /// <summary>
        /// Function to return stats for all players based on sport, year, season type, and positions
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <param name="positions">Positions to return (TEAM, QB, WR, RB, TE, QB, DEF, DE, LB, DB, K)</param>
        /// <param name="ordering">Optional parameter, (pts_ppr, pts_hppr, pts_std)</param>
        /// <returns>Returns a list of the Stats object for given player</returns>
        public async Task<IEnumerable<Stats>> GetStatsByPosition(string sport, int year, string seasonSegment, string[] positions, string? ordering = null)
        {
            var request = new RestRequest($"/stats/{sport}/{year}", Method.Get);
            request.AddQueryParameter("season_type", seasonSegment);
            foreach (var position in positions)
            {
                request.AddQueryParameter("position[]", position);
            }
            if (ordering != null)
            {
                request.AddQueryParameter("order_by", ordering);
            }

            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<Stats>>(response.Content) ?? new List<Stats>();
        }

        /// <summary>
        /// Function to return projections for a specific player based on sport, player id, year, and season type.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="playerId">ID of the player to return</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <param name="grouping">Optional parameter, default returns entire season, week returns by week</param>
        /// <returns>Returns a list of the Stats object for given player</returns>
        public async Task<IEnumerable<Stats>> GetPlayerProjections(string sport, int playerId, int year, string seasonSegment, string? grouping = null)
        {
            var request = new RestRequest($"/projections/{sport}/player/{playerId}", Method.Get);
            request.AddQueryParameter("season", year.ToString());
            request.AddQueryParameter("season_type", seasonSegment);

            var response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<Stats>>(response.Content) ?? new List<Stats>();
        }
    }
}
