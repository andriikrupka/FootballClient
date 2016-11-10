using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FootballClient.Models.RssPhoto
{

    [XmlTypeAttribute("rss")]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class RssPhoto
    {
        [XmlElement(ElementName = "channel")]
        public RssChannel Channel { get; set; }
    }

    [XmlTypeAttribute("rssChannel")]
    public class RssChannel
    {
        [XmlElementAttribute("item")]
        public RssChannelPhotoItem[] PhotoItems { get; set; }
    }

    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
    [XmlRootAttribute(Namespace = "http://www.w3.org/2005/Atom", IsNullable = false)]
    public partial class link
    {

        [XmlAttributeAttribute()]
        public string href { get; set; }

        [XmlAttributeAttribute()]
        public string rel { get; set; }

        [XmlAttributeAttribute()]
        public string type { get; set; }
    }

    [XmlTypeAttribute("rssChannelImage")]
    public class RssChannelImage
    {
        public string rl { get; set; }

        public string title { get; set; }

        public string link { get; set; }
    }

    [XmlTypeAttribute("rssChannelItem")]
    public class RssChannelPhotoItem
    {
        private readonly CultureInfo ruInfo = new CultureInfo("ru-RU");

        [XmlElementAttribute("title")]
        public string Title { get; set; }

        [XmlElementAttribute("link")]
        public string Link { get; set; }

        [XmlElementAttribute("pubDate")]
        public string PubDate { get; set; }

        [IgnoreDataMember]
        public DateTime Date
        {
            get
            {
                return DateTime.Parse(this.PubDate);
            }
        }

        [IgnoreDataMember]
        public string PublishedDate
        {
            get
            {
                return this.Date.ToString("dd MMMM yyyy, HH:mm", ruInfo);
            }
        }

        [XmlElementAttribute("id")]
        public ushort Id { get; set; }

        [XmlElementAttribute("thumbnail")]
        public string Thumbnail { get; set; }

        [XmlElementAttribute("thumbnail-big")]
        public string ThumbnailBig { get; set; }

        [XmlElementAttribute("gallery")]
        public RssChannelItemGallery Gallery { get; set; }

    }

    [XmlTypeAttribute("rssChannelItemGallery")]
    public class RssChannelItemGallery
    {
        [XmlElementAttribute("photo")]
        public RssChannelItemGalleryPhoto[] Photos { get; set; }

        [XmlAttributeAttribute("optimized")]
        public byte Optimized { get; set; }

        [XmlAttributeAttribute("thumb0.75xPrefix")]
        public string Thumb075Prefix { get; set; }

        [XmlAttributeAttribute("thumbPrefix")]
        public string ThumbPrefix { get; set; }

        [XmlAttributeAttribute("thumb1.5xPrefix")]
        public string Thumb15Prefix { get; set; }

        [XmlAttributeAttribute("thumb2xPrefix")]
        public string Thumb2Prefix { get; set; }

        [XmlAttributeAttribute("urlPrefix")]
        public string UrlPrefix { get; set; }

        [XmlAttributeAttribute("url2xPrefix")]
        public string Url2Prefix { get; set; }
    }

    [XmlTypeAttribute("rssChannelItemGalleryPhoto")]
    public class RssChannelItemGalleryPhoto
    {
        [XmlAttributeAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttributeAttribute(AttributeName = "caption")]
        public string Caption { get; set; }


        public string Small { get; set; }

        public string Medium { get; set; }

        public string Big { get; set; }

        public string Large { get; set; }

        public void OnDeseriazlized(string big, string medium, string large, string small)
        {
            if (!string.IsNullOrEmpty(big))
            {
                this.Big = big + this.Id;
            }

            if (!string.IsNullOrEmpty(large))
            {
                this.Large = large + this.Id;
            }

            if (!string.IsNullOrEmpty(medium))
            {
                this.Medium = medium + this.Id;
            }

            if (!string.IsNullOrEmpty(small))
            {
                this.Small = small + this.Id;
            }
        }
    }
}
