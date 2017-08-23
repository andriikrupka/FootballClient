using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FootballClient.Models
{
    [DataContract]
    public class MatchDetails 
    {
        public MatchDetails()
        {
            this.MatchFeeds = new List<MatchFeed>();
            this.MatchDetailsRows = new List<MatchDetailsRow>();
        }

        [DataMember]
        public List<MatchFeed> MatchFeeds { get; set; }

        [DataMember]
        public string Report { get; set; }

        [DataMember]
        public string Preview { get; set; }

        [DataMember]
        public List<MatchDetailsRow> MatchDetailsRows { get; set; }


        [DataMember]
        public string LeftTeamImage { get; set; }

        [DataMember]
        public string RightTeamImage { get; set; }

        [DataMember]
        public string LeftScore { get; set; }

        [DataMember]
        public string RightScore { get; set; }

   }

    [DataContract]
    public class MatchDetailsColumn
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public string Minute { get; set; }
    }

    [DataContract]
    public class MatchDetailsRow
    {
        public MatchDetailsRow()
        {
            this.TableColumns = new List<MatchDetailsColumn>();
        }

        [DataMember]
        public int ColumnSpan { get; set; }

        [DataMember]
        public List<MatchDetailsColumn> TableColumns { get; set; }
    }
}
