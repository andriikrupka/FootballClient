using Akavache;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace FootballClient.DataAccess
{
    internal enum RequestAccessMode
    {
        Server,

        Cache
    }

    public class RestSettings<T>
    {
        internal HttpRequestMessage RequestMessage { get; private set; }
        internal RequestAccessMode Mode { get; private set; }
        internal IParserStrategy<T> Parser { get; private set; }

        internal RestSettings<T> AddMode(RequestAccessMode mode)
        {
            Mode = mode;
            return this;
        }

        internal RestSettings<T> AddRequestMessage(HttpRequestMessage requestMessage)
        {
            RequestMessage = requestMessage;
            return this;
        }

        internal RestSettings<T> AddParser(IParserStrategy<T> parser)
        {
            Parser = parser;
            return this;
        }
    }

    public interface IRestClient
    {
        Task<T> SendAsync<T>(RestSettings<T> settigns,
                             Func<Tuple<T, DateTimeOffset?>, bool> fetchPredicate = null,
                             Action<T> beforeLoading = null,
                             Action<Exception> onError = null);

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

        public async Task<T> SendAsync<T>(RestSettings<T> settigns,
                                          Func<Tuple<T, DateTimeOffset?>, bool> fetchPredicate = null,
                                          Action<T> beforeLoading = null,
                                          Action<Exception> onError = null)
        {
            var key = Utility.Md5Calculator.ComputeMd5(settigns.RequestMessage.RequestUri.OriginalString);
            bool? reload = null;
            var cachedValue = default(T);
            var createdItem = await BlobCache.LocalMachine.GetObjectCreatedAt<T>(key);

            if (createdItem == null)
            {
                reload = fetchPredicate?.Invoke(Tuple.Create(default(T), createdItem)) ?? true;
            }
            else
            {
                cachedValue = await BlobCache.LocalMachine.GetObject<T>(key);
            }

            var value = cachedValue;
            if (settigns.Mode == RequestAccessMode.Cache)
            {
                return value;
            }

            reload = reload ?? fetchPredicate?.Invoke(Tuple.Create(value, createdItem)) ?? true;
            if (reload.Value)
            {
                try
                {
                    beforeLoading?.Invoke(cachedValue);
                    value = await FetchData(settigns);
                }
                catch (Exception ex)
                {
                    value = cachedValue;
                    onError?.Invoke(ex);
                }

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
