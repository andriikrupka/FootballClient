namespace FootballClient.Models
{
    using System.Collections.Generic;

    public class Championat
    {
        public string Name { get; set; }

        public string TourName { get; set; }

        public List<GameList> GameLists { get; set; }
    }
}
