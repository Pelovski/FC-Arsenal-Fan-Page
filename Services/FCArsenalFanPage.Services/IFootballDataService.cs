namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels.Standings;

    public interface IFootballDataService
    {
        Task<List<TeamStandingsViewModel>> GetStandingsAsync();

        Task<List<MatchViewModel>> GetUpcomingMatchesAsync();

        Task<List<MatchResultViewModel>> GetTeamResultsAsync();
    }
}
