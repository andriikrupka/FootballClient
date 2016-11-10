using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FootballClient.Models.Schedule
{

    [XmlTypeAttribute("rss")]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class RssSchedule
    {

        [XmlArrayItemAttribute("item", IsNullable = false)]
        public ScheduleItem[] channel { get; set; }

        public decimal version { get; set; }
    }

    /// <remarks/>
    [XmlTypeAttribute("rssItem")]
    public partial class ScheduleItem
    {
        [XmlElement(ElementName = "id")]
        public ushort Id { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string PubDate { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElementAttribute("description-image")]
        public string DescriptionImage { get; set; }

        [XmlElement(ElementName = "thumbnail")]
        public string Thumbnail { get; set; }

        [XmlElement(ElementName = "highlighted")]
        public byte Highlighted { get; set; }

        [XmlIgnoreAttribute()]
        public bool highlightedSpecified { get; set; }
    }


}
