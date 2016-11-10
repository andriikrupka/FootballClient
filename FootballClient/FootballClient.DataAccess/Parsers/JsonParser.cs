using Newtonsoft.Json;

namespace FootballClient.DataAccess.Parsers
{
    public class JsonParser<T> : IParserStrategy<T>
    {
        public T Parse(string data)
        {
            var response = JsonConvert.DeserializeObject<T>(data);
            return response;
        }
    }
}
