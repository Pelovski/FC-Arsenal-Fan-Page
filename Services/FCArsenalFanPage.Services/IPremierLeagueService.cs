using System.Threading.Tasks;

namespace FCArsenalFanPage.Services
{
    public interface IPremierLeagueService
    {
        Task<string> GetStandingsAsync();

        Task<string> GetUpcomingMatchesAsync();
    }
}
