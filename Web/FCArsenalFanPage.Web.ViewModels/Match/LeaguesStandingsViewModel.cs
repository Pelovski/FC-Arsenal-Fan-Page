namespace FCArsenalFanPage.Web.ViewModels.Match
{
    using System.Collections.Generic;

    public class LeaguesStandingsViewModel
    {
        public IEnumerable<TeamStandingsViewModel> PremierLeague { get; set; }

        public IEnumerable<TeamStandingsViewModel> ChampionsLeague { get; set; }
    }
}
