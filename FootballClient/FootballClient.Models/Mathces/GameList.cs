namespace FootballClient.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GameList : BindableObject
    {
        private string teamScoreOne;
        private string teamScoreTwo;

        [DataMember]
        public string Time { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember(Name = "Name1")]
        public string NameOne { get; set; }

        [DataMember(Name = "Name2")]
        public string NameTwo { get; set; }

        [DataMember(Name = "TeamScore1")]
        public string TeamScoreOne
        {
            get { return this.teamScoreOne; }
            set
            {
                if (value != this.teamScoreOne)
                {
                    this.teamScoreOne = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        [DataMember(Name = "TeamScore2")]
        public string TeamScoreTwo
        {
            get { return this.teamScoreTwo; }
            set
            {
                if (value != this.teamScoreTwo)
                {
                    this.teamScoreTwo = value;
                    base.RaisePropertyChanged();
                }
            }
        }

        [DataMember]
        public object CssClass { get; set; }

        [DataMember]
        public int GameId { get; set; }

        [DataMember(Name = "TeamId1")]
        public int TeamIdOne { get; set; }

        [DataMember(Name = "TeamId2")]
        public int TeamIdTwo { get; set; }

        [DataMember]
        public string MatchLink { get; set; }

        [DataMember]
        public string ChampName { get; set; }

        [DataMember]
        public string TourName { get; set; }

        [DataMember]
        public int ItemIndex { get; set; }
    }
}
