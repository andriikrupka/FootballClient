namespace FootballClient.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public class MatchesResponse
    {
        [DataMember]
        public int Message { get; set; }

        [DataMember]
        public string ViewDate { get; set; }

        [DataMember]
        public List<GameList> GameList { get; set; }
    }
}
