﻿namespace FCArsenalFanPage.Web.ViewModels.Match
{
    public class TeamStandingsViewModel
    {
        public int Position { get; set; }

        public string TeamName { get; set; }

        public int PlayedGames { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Losts { get; set; }

        public int Points { get; set; }

        public int GoalsFor { get; set; }

        public int GoalsAgainst { get; set; }

        public int GoalsDifference { get; set; }

        public string TeamLogo { get; set; }
    }
}
