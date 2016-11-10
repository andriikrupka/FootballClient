namespace FootballClient.Models
{
    using System.Xml.Serialization;

    [XmlTypeAttribute("rss")]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Rss
    {
        [XmlElement(ElementName = "channel")]
        public RssChannel Channel { get; set; }
    }
}
