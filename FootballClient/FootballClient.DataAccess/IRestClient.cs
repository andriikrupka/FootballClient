using Akavache;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace FootballClient.DataAccess
{
    public enum RequestAccessMode
    {
        Server,

        Cache
    }

    public class RestSettings<T>
    {
        public HttpRequestMessage RequestMessage { get; private set; }
        public RequestAccessMode Mode { get; private set; }
        public IParserStrategy<T> Parser { get; private set; }

        public RestSettings<T> AddMode(RequestAccessMode mode)
        {
            Mode = mode;
            return this;
        }

        public RestSettings<T> AddRequestMessage(HttpRequestMessage requestMessage)
        {
            RequestMessage = requestMessage;
            return this;
        }

        public RestSettings<T> AddParser(IParserStrategy<T> parser)
        {
            Parser = parser;
            return this;
        }
    }

    public interface IRestClient
    {
        Task<T> SendAsync<T>(RestSettings<T> settigns, Func<Tuple<T, DateTimeOffset?>, bool> fetchPredicate);

        //Task<T> SendMessageAsync<T>(HttpRequestMessage request, IParserStrategy<T> parser, RequestAccessMode mode = RequestAccessMode.Server);
    }

    public class RestClient : IRestClient
    {
        private HttpClient _httpClient;
        private HttpClientHandler _httpClientHandler;

        public RestClient()
        {
            _httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip
            };
            _httpClient = new HttpClient(_httpClientHandler);
        }

        public Task<T> SendMessageAsync<T>(HttpRequestMessage request, IParserStrategy<T> parser, RequestAccessMode mode = RequestAccessMode.Server)
        {
            return SendAsync<T>(new RestSettings<T>().AddParser(parser).AddRequestMessage(request).AddMode(mode), null);
        }

        public async Task<T> SendAsync<T>(RestSettings<T> settigns, Func<Tuple<T, DateTimeOffset?>, bool> fetchPredicate)
        {
            var key = Utility.Md5Calculator.ComputeMd5(settigns.RequestMessage.RequestUri.OriginalString);

            var createdItem = await BlobCache.LocalMachine.GetObjectCreatedAt<T>(key);
            if (createdItem == null)
            {

            }

            var value = await BlobCache.LocalMachine.GetOrCreateObject<T>(key, () => default(T), DateTimeOffset.Now.AddDays(10));
            if (settigns.Mode == RequestAccessMode.Cache)
            {
                return value;
            }

            var reload = fetchPredicate?.Invoke(Tuple.Create(value, createdItem)) ?? true;
            if (reload)
            {
                value = await FetchData(settigns);
                await BlobCache.LocalMachine.InsertObject(key, value, DateTimeOffset.Now.AddDays(10));
            }

            return value;
        }

        public IObservable<T> FetchData<T>(RestSettings<T> settigns)
        {
            return Task.Run(async () =>
            {
                using (var httpResult = await _httpClient.SendAsync(settigns.RequestMessage))
                {
                    var data = await httpResult.Content.ReadAsStringAsync();
                    var parsedData = settigns.Parser.Parse(data);
                    return parsedData;
                }
            }).ToObservable();
        }
    }
}
