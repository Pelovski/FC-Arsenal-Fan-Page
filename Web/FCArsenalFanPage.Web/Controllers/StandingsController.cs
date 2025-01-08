namespace FCArsenalFanPage.Web.Controllers
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Services;
    using Microsoft.AspNetCore.Mvc;

    public class StandingsController : BaseController
    {
        private readonly IPremierLeagueService premierLeagueService;

        public StandingsController(IPremierLeagueService premierLeagueService)
        {
            this.premierLeagueService = premierLeagueService;
        }

        public async Task<IActionResult> Leaderboard()
        {
            var data = await this.premierLeagueService.GetStandingsAsync();
            return this.Ok(data);
        }

        public async Task<IActionResult> GetUpcomingMatches()
        {
            var data = await this.premierLeagueService.GetUpcomingMatchesAsync();
            return this.Ok(data);
        }
    }
}
