namespace FCArsenalFanPage.Web.ViewModels.Standings
{
    using System;

    public class MatchViewModel
    {
        public DateTime UtcDate { get; set; }

        public TeamViewModel HomeTeam { get; set; }

        public TeamViewModel AwayTeam { get; set; }
    }
}
