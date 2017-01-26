using Newtonsoft.Json;

namespace FootballClient.DataAccess.Parsers
{
    public class JsonParser<T> : IParserStrategy<T>
    {
        public T Parse(string data)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<T>(data);
                return response;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
