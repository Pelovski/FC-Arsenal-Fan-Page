namespace FCArsenalFanPage.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
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
                GoalsDifference = team.GetProperty("goalDifference").GetInt32(),
                TeamLogo = team.GetProperty("team").GetProperty("crest").GetString(),
            }).ToList();

            return standings;
        }

        public async Task<List<MatchViewModel>> GetUpcomingMatchesAsync()
        {
            var response = await this.httpClient.GetAsync("competitions/2021/matches?status=SCHEDULED");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var matches = JsonDocument.Parse(jsonString)
                .RootElement
                .GetProperty("matches")
                .EnumerateArray()
                .Where(m => m.GetProperty("homeTeam").GetProperty("name").GetString() == "Arsenal FC" ||
                            m.GetProperty("awayTeam").GetProperty("name").GetString() == "Arsenal FC")
                .Select(m => new MatchViewModel
                {
                    // Преобразуваме UTC датата в DateTim
                    UtcDate = DateTime.ParseExact(m.GetProperty("utcDate").GetString(), "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                    HomeTeam = new TeamViewModel
                    {
                        Name = m.GetProperty("homeTeam").GetProperty("name").GetString(),
                        Crest = m.GetProperty("homeTeam").GetProperty("crest").GetString()
                    },
                    AwayTeam = new TeamViewModel
                    {
                        Name = m.GetProperty("awayTeam").GetProperty("name").GetString(),
                        Crest = m.GetProperty("awayTeam").GetProperty("crest").GetString()
                    }
                })
                .ToList();

            return matches;
        }
    }
}
