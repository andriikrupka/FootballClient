using System.Xml.Serialization;

namespace FootballClient.Models
{
    [XmlType("response")]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ResponseCategory
    {
        [XmlElement(ElementName = "data")]
        public ResponseData Data
        {
            get;
            set;
        }
    }
}
