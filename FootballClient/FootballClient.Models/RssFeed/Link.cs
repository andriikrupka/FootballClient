namespace FootballClient.Models
{
    using System.Xml.Serialization;

    [XmlTypeAttribute("link", Namespace = "http://www.w3.org/2005/Atom")]
    [XmlRootAttribute(Namespace = "http://www.w3.org/2005/Atom", IsNullable = false)]
    public partial class Link
    {
        [XmlAttributeAttribute()]
        public string href { get; set; }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string rel { get; set; }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string type { get; set; }
    }
}
