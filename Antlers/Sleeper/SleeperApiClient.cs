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
        /// <returns>Returns a tuple of player object and raw JSON for the specified player</returns>
        public async Task<Tuple<League,string>> GetLeague(long leagueId)
        {
            var request = new RestRequest($"/league/{leagueId}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch league: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<League, string>(new League(), string.Empty);
            }

            var league = JsonConvert.DeserializeObject<League>(response.Content) ?? new League();
            return new Tuple<League, string>(league, response.Content);
        }

        /// <summary>
        /// Function to return data for a specific player based on sport and player id
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="playerId">The ID of the player to retrieve</param>
        /// <returns>Returns a tuple of player object and raw JSON for the specified player</returns>
        public async Task<Tuple<Player, string>> GetPlayer(string sport, int playerId)
        {
            var request = new RestRequest($"/players/{sport}/{playerId}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch player: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<Player, string>(new Player(), string.Empty);
            }

            var player = JsonConvert.DeserializeObject<Player>(response.Content) ?? new Player();
            return new Tuple<Player, string>(player, response.Content);
        }

        /// <summary>
        /// Function to return all players from Sleeper (Limit calls to once per day at most to keep your player IDs updated. The average size of this query is 5MB.)
        /// Link: https://docs.sleeper.com/#players
        /// </summary>
        /// <param name="sport">Currently only supports "nfl".</param>
        /// <returns>Returns a tuple of list of Player objects and raw JSON containing all players of the provided sport</returns>
        public async Task<Tuple<IEnumerable<Player>, string>> GetPlayers(string sport)
        {
            var request = new RestRequest($"/players/{sport}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch players: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<IEnumerable<Player>, string>(new List<Player>(), string.Empty);
            }

            var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(response.Content) ?? new List<Player>();
            return new Tuple<IEnumerable<Player>, string>(players, response.Content);
        }

        /// <summary>
        /// Function to return Trending Player data from Sleeper (requires attribution, they also provide an embed iframe for use on websites/blogs)
        /// Link: https://docs.sleeper.com/#trending-players
        /// </summary>
        /// <param name="sport">Currently only supports "nfl".</param>
        /// <param name="type">Either "add" or "drop"</param>
        /// <param name="lookbackHours">Number of hours to look back (defaults to 24)</param>
        /// <param name="limit">Number of players you want returned (defaults to 25)</param>
        /// <returns>Returns a tuple of list of Player objects and raw JSON containing Sleeper Trending data</returns>
        public async Task<Tuple<IEnumerable<Player>, string>> GetTrendingPlayers(string sport, string type, int lookbackHours = 24, int limit = 25)
        {
            var request = new RestRequest($"/players/{sport}/trending/{type}", Method.Get);
            request.AddQueryParameter("lookback_hours", lookbackHours.ToString());
            request.AddQueryParameter("limit", limit.ToString());

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch players: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<IEnumerable<Player>, string>(new List<Player>(), string.Empty);
            }

            var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(response.Content) ?? new List<Player>();
            return new Tuple<IEnumerable<Player>, string>(players, response.Content);
        }

        /// <summary>
        /// Function to return rosters for a specific league.
        /// </summary>
        /// <param name="leagueId">ID of the league to retrieve rosters from</param>
        /// <returns>Returns a tuple of list of Roster objects and raw JSON</returns>
        public async Task<Tuple<IEnumerable<Roster>, string>> GetRosters(long leagueId)
        {
            var request = new RestRequest($"/league/{leagueId}/rosters", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch rosters: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<IEnumerable<Roster>, string>(new List<Roster>(), string.Empty);
            }

            var rosters = JsonConvert.DeserializeObject<IEnumerable<Roster>>(response.Content) ?? new List<Roster>();
            return new Tuple<IEnumerable<Roster>, string>(rosters, response.Content);
        }

        /// <summary>
        /// Function to return schedule for a specific sport, segment  and year.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl".</param>
        /// <param name="segment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <returns>Returns a tuple of list of Schedule objects and raw JSON</returns>
        public async Task<Tuple<IEnumerable<Schedule>, string>> GetSchedule(string sport, string segment, int year)
        {
            var request = new RestRequest($"/schedule/{sport}/{segment}/{year}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch schedule: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null" || response.Content.Trim().ToLower() == "[]")
            {
                return new Tuple<IEnumerable<Schedule>, string>(new List<Schedule>(), string.Empty);
            }

            var schedule = JsonConvert.DeserializeObject<IEnumerable<Schedule>>(response.Content) ?? new List<Schedule>();
            return new Tuple<IEnumerable<Schedule>, string>(schedule, response.Content);
        }

        /// <summary>
        /// Function to return stats for a specific player based on sport, player id, season type and year.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="playerId">ID of the player to return</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <param name="grouping">Optional parameter, default returns entire season, week returns by week</param>
        /// <returns>Returns a tuple of Stats object and raw JSON for given player</returns>
        public async Task<Tuple<Stats, string>> GetPlayerStats(string sport, int playerId, int year, string seasonSegment, int? week = null, string? grouping = null)
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

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch stats: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<Stats, string>(new Stats(), string.Empty);
            }

            var stats = JsonConvert.DeserializeObject<Stats>(response.Content) ?? new Stats();
            return new Tuple<Stats, string>(stats, response.Content);
        }

        /// <summary>
        /// Function to return stats for all players based on sport, season type and year.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <param name="ordering">Optional parameter, (pts_ppr, pts_hppr, pts_std)</param>
        /// <returns>Returns a tuple of list of Stats objects and raw JSON</returns>
        public async Task<Tuple<IEnumerable<Stats>, string>> GetTeamStats(string sport, int year, string seasonSegment, string? ordering = null)
        {
            var request = new RestRequest($"/stats/{sport}/{year}", Method.Get);
            request.AddQueryParameter("season_type", seasonSegment);
            request.AddQueryParameter("position[]", "TEAM");
            if (ordering != null)
            {
                request.AddQueryParameter("order_by", ordering);
            }

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch stats: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<IEnumerable<Stats>, string>(new List<Stats>(), string.Empty);
            }

            var stats = JsonConvert.DeserializeObject<IEnumerable<Stats>>(response.Content) ?? new List<Stats>();
            return new Tuple<IEnumerable<Stats>, string>(stats, response.Content);
        }

        /// <summary>
        /// Function to return stats for all players based on sport, year, season type, and positions
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <param name="positions">Positions to return (TEAM, QB, WR, RB, TE, QB, DEF, DE, LB, DB, K)</param>
        /// <param name="ordering">Optional parameter, (pts_ppr, pts_hppr, pts_std)</param>
        /// <returns>Returns a tuple of list of Stats objects and raw JSON for given player</returns>
        public async Task<Tuple<IEnumerable<StatsMetrics>, string>> GetStatsByPosition(string sport, int year, string seasonSegment, string[] positions, string? ordering = null)
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

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch stats: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<IEnumerable<StatsMetrics>, string>(new List<StatsMetrics>(), string.Empty);
            }

            var stats = JsonConvert.DeserializeObject<IEnumerable<StatsMetrics>>(response.Content) ?? new List<StatsMetrics>();
            return new Tuple<IEnumerable<StatsMetrics>, string>(stats, response.Content);
        }

        /// <summary>
        /// Function to return projections for all players based on sport, year, season type, and positions.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="year">Year to return</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="positions">Positions to return (DB, DL, K, LB, QB, RB, TE, WR)</param>
        /// <param name="ordering">Optional parameter for ordering results (for example adp_idp)</param>
        /// <returns>Returns a tuple of list of Projection objects and raw JSON</returns>
        public async Task<Tuple<IEnumerable<Projection>, string>> GetPlayerProjections(string sport, int year, string seasonSegment, string[] positions, string? ordering = null)
        {
            var request = new RestRequest($"/projections/{sport}/{year}", Method.Get);
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

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch projections: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<IEnumerable<Projection>, string>(new List<Projection>(), string.Empty);
            }

            var projections = JsonConvert.DeserializeObject<IEnumerable<Projection>>(response.Content) ?? new List<Projection>();
            return new Tuple<IEnumerable<Projection>, string>(projections, response.Content);
        }

        /// <summary>
        /// Function to return player values for a sport, season segment, year, and scoring type.
        /// </summary>
        /// <param name="sport">Currently only supports "nfl"</param>
        /// <param name="seasonSegment">Season segment, (pre, regular, post)</param>
        /// <param name="year">Year to return</param>
        /// <param name="scoring">Scoring format (ppr, half_ppr, std)</param>
        /// <param name="idp">Whether to include IDP values</param>
        /// <param name="isDynasty">Whether to use dynasty values</param>
        /// <returns>Returns a tuple of player values by player id and raw JSON</returns>
        public async Task<Tuple<Dictionary<string, decimal>, string>> GetPlayerValues(string sport, string seasonSegment, int year, string scoring, bool idp = true, bool isDynasty = true)
        {
            var request = new RestRequest($"/players/{sport}/values/{seasonSegment}/{year}/{scoring}", Method.Get);
            request.AddQueryParameter("idp", idp.ToString().ToLowerInvariant());
            request.AddQueryParameter("is_dynasty", isDynasty.ToString().ToLowerInvariant());

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Failed to fetch player values: {response.StatusCode} - {response.Content}");
            }

            if (string.IsNullOrEmpty(response.Content) || response.Content.Trim().ToLower() == "null")
            {
                return new Tuple<Dictionary<string, decimal>, string>(new Dictionary<string, decimal>(), string.Empty);
            }

            var playerValues = JsonConvert.DeserializeObject<Dictionary<string, decimal>>(response.Content) ?? new Dictionary<string, decimal>();
            return new Tuple<Dictionary<string, decimal>, string>(playerValues, response.Content);
        }

    }
}
