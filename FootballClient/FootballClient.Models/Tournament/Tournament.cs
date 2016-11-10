using System.Collections.Generic;

namespace FootballClient.Models
{
    using System.Collections.ObjectModel;

    public class Tournament
    {
        public GameList GameList { get; set; }

        public ObservableCollection<Championat> Championat { get; set; }

        public List<ITournamentRow> Rows { get; set; }
    }
}
