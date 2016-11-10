namespace FootballClient.DataAccess
{
    public interface IParserStrategy<T>
    {
        T Parse(string data);
    }
}
