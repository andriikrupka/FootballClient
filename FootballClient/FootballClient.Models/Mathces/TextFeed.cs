using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FootballClient.Models
{
    [DataContract]
    public class TextFeed
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string ClassName { get; set; }
    }
}
