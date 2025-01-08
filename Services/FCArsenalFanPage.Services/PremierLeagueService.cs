namespace FCArsenalFanPage.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PremierLeagueService : IPremierLeagueService
    {
        private readonly HttpClient httpClient;

        public PremierLeagueService(IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClientFactory.CreateClient("FootballData");
        }

        public async Task<string> GetStandingsAsync()
        {
            var response = await this.httpClient.GetAsync("competitions/2021/standings");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetUpcomingMatchesAsync()
        {
            var response = await this.httpClient.GetAsync("competitions/2021/matches?status=SCHEDULED");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
