using System.Collections.Generic;
using System.Xml.Serialization;

namespace FootballClient.Models
{
    [XmlType("responseData")]
    public partial class ResponseData
    {
        [XmlElement("filter-param")]
        public List<Category> FilterParam
        {
            get;
            set;
        }
    }
}
