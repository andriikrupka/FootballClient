using System.Xml.Serialization;

namespace FootballClient.Models.News
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class rss
    {
        [XmlElement(ElementName = "channel")]
        public RssNewsDetailsChannel Channel { get; set; }
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, TypeName = "rssChannel")]
    public partial class RssNewsDetailsChannel
    {
        [XmlElement(ElementName = "item")]
        public RssNewsDetailsChannelItem Item { get; set; }
    }

    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, TypeName = "rssChannelItem")]
    public partial class RssNewsDetailsChannelItem
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "date")]
        public uint Date { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "category")]
        public string Category { get; set; }

        [XmlElement(ElementName = "article")]
        public string Article { get; set; }

        [XmlElement(ElementName = "img")]
        public RssNewsDetailsChannelItemImg Img { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }

    [XmlType(AnonymousType = true, TypeName = "rssChannelItemImg")]
    public partial class RssNewsDetailsChannelItemImg
    {
        [XmlAttribute("height")]
        public ushort Height { get; set; }

        [XmlAttribute("Width")]
        public ushort Width { get; set; }

        [XmlText()]
        public string Value { get; set; }
    }


}
