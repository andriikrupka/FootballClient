using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FootballClient.Models
{
    [DataContract]
    public class MatchFeed
    {
        public MatchFeed()
        {
            this.TextFeed = new TextFeed();
        }

        [DataMember]
        public string Time { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public TextFeed TextFeed { get; set; }
    }
}
