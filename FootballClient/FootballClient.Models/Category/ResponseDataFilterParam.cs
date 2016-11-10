using System.Xml.Serialization;

namespace FootballClient.Models
{
    [XmlType("responseDataFilterparam")]
    public partial class Category
    {
        [XmlElement(ElementName = "name")]
        public string Name
        {
            get;
            set;
        }

        [XmlAttributeAttribute("code")]
        public string Code
        {
            get;
            set;
        }
    }
}
