namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels.Match;

    public interface IFootballDataService
    {
        Task<IEnumerable<TeamStandingsViewModel>> GetStandingsAsync(string competitionCode);

        Task<IEnumerable<MatchViewModel>> GetUpcomingMatchesAsync();

        Task<IEnumerable<MatchResultViewModel>> GetTeamResultsAsync();

        Task<LeaguesStandingsViewModel> GetLeaguesStandingsAsync();
    }
}
