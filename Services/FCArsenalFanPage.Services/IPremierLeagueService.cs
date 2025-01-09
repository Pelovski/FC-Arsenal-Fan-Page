namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels.Standings;

    public interface IPremierLeagueService
    {
        Task<List<TeamStandingsViewModel>> GetStandingsAsync();

        Task<string> GetUpcomingMatchesAsync();
    }
}
