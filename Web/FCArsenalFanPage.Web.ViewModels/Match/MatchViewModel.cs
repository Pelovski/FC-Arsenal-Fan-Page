namespace FCArsenalFanPage.Web.ViewModels.Match
{
    using System;

    public class MatchViewModel
    {
        public DateTime UtcDate { get; set; }

        public TeamViewModel HomeTeam { get; set; }

        public TeamViewModel AwayTeam { get; set; }

        public string TournamentLogo { get; set; }
    }
}
