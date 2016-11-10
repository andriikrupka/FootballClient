using System.Threading.Tasks;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace FootballClient.DataAccess.Request.Parsers
{
    public class XmlParser<T> : IParserStrategy<T>
    {
        public T Parse(string data)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data ?? "")))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
