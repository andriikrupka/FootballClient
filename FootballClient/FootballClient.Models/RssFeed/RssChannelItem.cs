namespace FootballClient.Models
{
    using System.Xml.Serialization;

    [XmlTypeAttribute("rssChannelItem")]
    public partial class RssChannelItem
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string PubDate { get; set; }

        [XmlElement(ElementName = "id")]
        public uint Id { get; set; }

        [XmlElement(ElementName = "annotation")]
        public string Annotation { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "highlighted")]
        public byte Highlighted { get; set; }

        [XmlElement(ElementName = "category")]
        public string Category { get; set; }

        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "thumbnail")]
        public string Thumbnail { get; set; }

        [XmlElementAttribute("thumbnail-small")]
        public string ThumbnailSmall { get; set; }

        [XmlElementAttribute("thumbnail-big")]
        public string ThumbnailBig { get; set; }

        [XmlElementAttribute("thumbnail-large")]
        public string ThumbnailLarge { get; set; }

        [XmlElementAttribute("description-image")]
        public string DescriptionImage { get; set; }

        [XmlElementAttribute("description-image-note")]
        public string DescriptionImageNote { get; set; }
    }
}
