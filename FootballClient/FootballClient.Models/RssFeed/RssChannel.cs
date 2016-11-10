namespace FootballClient.Models
{
    using System.Xml.Serialization;

    [XmlTypeAttribute("rssChannel")]
    public partial class RssChannel
    {

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "language")]
        public string Language { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string PubDate { get; set; }

        [XmlElement(ElementName = "image")]
        public RssChannelImage Image { get; set; }

        [XmlElementAttribute("item")]
        public RssChannelItem[] Items { get; set; }
    }
}
