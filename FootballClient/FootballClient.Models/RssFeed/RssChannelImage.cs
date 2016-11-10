namespace FootballClient.Models
{
    using System.Xml.Serialization;

    [XmlTypeAttribute("rssChannelImage")]
    public partial class RssChannelImage
    {
        [XmlAttributeAttribute("url")]
        public string Url { get; set; }

        [XmlAttributeAttribute("title")]
        public string Title { get; set; }

        [XmlAttributeAttribute("link")]
        public string Link { get; set; }
    }
}
