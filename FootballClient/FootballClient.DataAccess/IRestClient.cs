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

    public interface IRestClient
    {
        Task<T> SendMessageAsync<T>(HttpRequestMessage requestMessage, IParserStrategy<T> parser, RequestAccessMode mode = RequestAccessMode.Server, Func<DateTimeOffset, bool> fetchPredicate = null);
        Task<T> SendMessageAsync<T>(RequestSettings<T> settings);
    }

    public class RequestSettings<T>
    {
        public HttpRequestMessage RequestMessage { get; private set; }
        public RequestAccessMode Mode { get; private set; }
        public IParserStrategy<T> Parser { get; private set; }

        public Func<DateTimeOffset, bool> FetchPredicate { get; private set; }

        public RequestSettings<T> AddMode(RequestAccessMode mode)
        {
            Mode = mode;
            return this;
        }

        public RequestSettings<T> AddRequestMessage(HttpRequestMessage requestMessage)
        {
            RequestMessage = requestMessage;
            return this;
        }

        public RequestSettings<T> AddParser(IParserStrategy<T> parser)
        {
            Parser = parser;
            return this;
        }

        public RequestSettings<T> AddFetchPredicate(Func<DateTimeOffset, bool> fetchPredicate)
        {
            FetchPredicate = fetchPredicate;
            return this;
        }
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

        public Task<T> SendMessageAsync<T>(RequestSettings<T> settings)
        {
            return SendMessageAsync<T>(settings.RequestMessage, settings.Parser, settings.Mode, settings.FetchPredicate);
        }

        public async Task<T> SendMessageAsync<T>(HttpRequestMessage requestMessage, IParserStrategy<T> parser, RequestAccessMode mode = RequestAccessMode.Server, Func<DateTimeOffset, bool> fetchPredicate = null)
        {
            return await DetermineFunctions<T>(requestMessage, parser, mode, fetchPredicate);
        }

        private IObservable<T> DetermineFunctions<T>(HttpRequestMessage requestMessage, IParserStrategy<T> parser, RequestAccessMode mode = RequestAccessMode.Server, Func<DateTimeOffset, bool> fetchPredicate = null)
        {
            var key = Utility.Md5Calculator.ComputeMd5(requestMessage.RequestUri.OriginalString);

            IObservable<T> observable = null;
            switch (mode)
            {
                case RequestAccessMode.Server:
                    observable = BlobCache.LocalMachine.GetAndUpdateObject(key, 
                                                                           () => FetchObservable(requestMessage, parser));
                    break;
                case RequestAccessMode.Cache:
                    observable = BlobCache.LocalMachine.GetAndFetchLatest<T>(key, () => FetchObservable(requestMessage, parser), date =>

                    {
                        var res = fetchPredicate?.Invoke(date) ?? true;
                        return res;
                    })
                                                       .Catch(Observable.Return(default(T)));
                    break;
                default:
                    break;
            }

            return observable;
        }

        private IObservable<T> FetchObservable<T>(HttpRequestMessage requestMessage, IParserStrategy<T> parser)
        {
            return Task.Run(async () =>
            {
                using (var httpResult = await _httpClient.SendAsync(requestMessage))
                {
                    var data = await httpResult.Content.ReadAsStringAsync();
                    var parsedData = parser.Parse(data);
                    return parsedData;
                }
            }).ToObservable();
        }
    }
}
