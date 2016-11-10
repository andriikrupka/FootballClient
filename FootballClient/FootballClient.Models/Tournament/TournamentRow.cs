namespace FootballClient.Models
{
    public class TournamentRow : ITournamentRow
    {
        public int Position { get; set; }

        public string TeamName { get; set; }

        public string Games { get; set; }

        public string Wins { get; set; }

        public string Draws { get; set; }

        public string Loses { get; set; }

        public string GoalsScored { get; set; }

        public string GoalsConceded { get; set; }

        public string GoalsDifference { get; set; }

        public string Points { get; set; }
    }
}
