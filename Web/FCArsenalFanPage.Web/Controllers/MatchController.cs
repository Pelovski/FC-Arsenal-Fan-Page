﻿namespace FCArsenalFanPage.Web.Controllers
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Services;
    using Microsoft.AspNetCore.Mvc;

    public class MatchController : BaseController
    {
        private readonly IFootballDataService premierLeagueService;

        public MatchController(
            IFootballDataService premierLeagueService)
        {
            this.premierLeagueService = premierLeagueService;
        }

        public async Task<IActionResult> Leaderboard()
        {
            var standings = await this.premierLeagueService.GetStandingsAsync();

            return this.View(standings);
        }

        public async Task<IActionResult> UpcomingMatches()
        {
            var matches = await this.premierLeagueService.GetUpcomingMatchesAsync();

            return this.View(matches);
        }

        public async Task<IActionResult> TeamResults()
        {
            var viewModel = await this.premierLeagueService.GetTeamResultsAsync();

            return this.View(viewModel);
        }
    }
}
