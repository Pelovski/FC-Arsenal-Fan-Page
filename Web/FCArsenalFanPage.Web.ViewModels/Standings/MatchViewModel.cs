namespace FCArsenalFanPage.Web.ViewModels.Standings
{
    using System;

    public class MatchViewModel
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public DateTime UtcDate { get; set; }

        public string HomeTeamLogo { get; set; }

        public string AwayTeamLogo { get; set; }
    }
}
