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

    public class FootballDataService : IFootballDataService
    {
        private readonly HttpClient httpClient;

        public FootballDataService(IHttpClientFactory httpClientFactory)
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
            var competitions = new[] { "PL", "CL" }; // "PL" - Premier League, "CL" - Champions League

            var tasks = competitions.Select(competition =>
                this.httpClient.GetAsync($"competitions/{competition}/matches?status=SCHEDULED")).ToArray();

            var responses = await Task.WhenAll(tasks);

            var matches = new List<MatchViewModel>();

            foreach (var response in responses)
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();

                    var competitionMatches = JsonDocument.Parse(jsonString)
                        .RootElement
                        .GetProperty("matches")
                        .EnumerateArray()
                        .Where(m => m.GetProperty("homeTeam").GetProperty("name").GetString() == "Arsenal FC" ||
                                    m.GetProperty("awayTeam").GetProperty("name").GetString() == "Arsenal FC")
                        .Select(m => new MatchViewModel
                        {
                            UtcDate = DateTime.ParseExact(m.GetProperty("utcDate").GetString(), "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                            HomeTeam = new TeamViewModel
                            {
                                Name = m.GetProperty("homeTeam").GetProperty("name").GetString(),
                                TeamLogo = m.GetProperty("homeTeam").GetProperty("crest").GetString(),
                            },
                            AwayTeam = new TeamViewModel
                            {
                                Name = m.GetProperty("awayTeam").GetProperty("name").GetString(),
                                TeamLogo = m.GetProperty("awayTeam").GetProperty("crest").GetString(),
                            },

                            TournamentLogo = m.GetProperty("competition").GetProperty("emblem").GetString(),
                        });

                    matches.AddRange(competitionMatches);
                }
            }

            var sortedMatches = matches.OrderBy(m => m.UtcDate).ToList();

            return sortedMatches;
        }

        public async Task<List<MatchResultViewModel>> GetTeamResultsAsync()
        {
            var endpoint = $"teams/57/matches?status=FINISHED&dateFrom=2024-08-16&dateTo=2025-08-01";
            var response = await this.httpClient.GetAsync(endpoint);

            var matches = new List<MatchResultViewModel>();

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var competitionMatches = JsonDocument
                     .Parse(jsonString)
                     .RootElement
                     .GetProperty("matches")
                     .EnumerateArray()
                     .Select(m => new MatchResultViewModel
                     {
                         UtcDate = DateTime.ParseExact(m.GetProperty("utcDate").GetString(), "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),

                         HomeTeam = new TeamViewModel
                         {
                             Name = m.GetProperty("homeTeam").GetProperty("name").GetString(),
                             TeamLogo = m.GetProperty("homeTeam").GetProperty("crest").GetString(),
                         },

                         AwayTeam = new TeamViewModel
                         {
                             Name = m.GetProperty("awayTeam").GetProperty("name").GetString(),
                             TeamLogo = m.GetProperty("awayTeam").GetProperty("crest").GetString(),
                         },

                         TournamentLogo = m.GetProperty("competition").GetProperty("emblem").GetString(),

                         HomeScore = m.GetProperty("score").GetProperty("fullTime").GetProperty("home").GetInt32(),
                         AwayScore = m.GetProperty("score").GetProperty("fullTime").GetProperty("away").GetInt32(),
                     });

                if (competitionMatches.Any())
                {
                    matches.AddRange(competitionMatches);
                }
            }

            var sortedMatches = matches.OrderByDescending(m => m.UtcDate).ToList();
            return sortedMatches;
        }
    }
}
