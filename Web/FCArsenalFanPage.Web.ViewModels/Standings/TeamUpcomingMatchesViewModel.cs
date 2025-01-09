namespace FCArsenalFanPage.Web.ViewModels.Standings
{
    using System.Collections.Generic;

    public class TeamUpcomingMatchesViewModel
    {
        public IEnumerable<MatchViewModel> Matches { get; set; }
    }
}
