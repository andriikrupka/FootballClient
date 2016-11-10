namespace FootballClient.DataAccess.Request.Parsers
{
    public class StringParser : IParserStrategy<string>
    {
        public string Parse(string data)
        {
            return data;
        }
    }
}
