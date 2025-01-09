namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels.Standings;

    public class PremierLeagueService : IPremierLeagueService
    {
        private readonly HttpClient httpClient;

        public PremierLeagueService(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("FootballData");
        }

        public async Task<List<TeamStandingsViewModel>> GetStandingsAsync()
        {
            var response = await this.httpClient.GetAsync("competitions/2021/standings");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            var standings = JsonDocument.Parse(jsonString)
            .RootElement
            .GetProperty("standings")
            .EnumerateArray()
            .First()
            .GetProperty("table")
            .EnumerateArray()
            .Select(team => new TeamStandingsViewModel
            {
                Position = team.GetProperty("position").GetInt32(),
                TeamName = team.GetProperty("team").GetProperty("name").GetString(),
                PlayedGames = team.GetProperty("playedGames").GetInt32(),
                Wins = team.GetProperty("won").GetInt32(),
                Draws = team.GetProperty("draw").GetInt32(),
                Losts = team.GetProperty("lost").GetInt32(),
                Points = team.GetProperty("points").GetInt32(),
                GoalsFor = team.GetProperty("goalsFor").GetInt32(),
                GoalsAgainst = team.GetProperty("goalsAgainst").GetInt32(),
            }).ToList();

            return standings;
        }

        public async Task<string> GetUpcomingMatchesAsync()
        {
            var response = await this.httpClient.GetAsync("competitions/2021/matches?status=SCHEDULED");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
