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
        Task<T> SendMessageAsync<T>(HttpRequestMessage requestMessage, IParserStrategy<T> parser, RequestAccessMode mode = RequestAccessMode.Server);
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

        public async Task<T> SendMessageAsync<T>(HttpRequestMessage requestMessage, IParserStrategy<T> parser, RequestAccessMode mode = RequestAccessMode.Server)
        {
            return await DetermineFunctions<T>(requestMessage, parser, mode);
        }

        private IObservable<T> DetermineFunctions<T>(HttpRequestMessage requestMessage, IParserStrategy<T> parser, RequestAccessMode mode = RequestAccessMode.Server)
        {
            var key = Utility.Md5Calculator.ComputeMd5(requestMessage.RequestUri.OriginalString);

            IObservable<T> observable = null;
            switch (mode)
            {
                case RequestAccessMode.Server:
                    observable = BlobCache.LocalMachine.GetAndUpdateObject(key, 
                                                                           () => FetchObservable(requestMessage, parser),
                                                                           DateTimeOffset.UtcNow.AddMinutes(2));
                    break;
                case RequestAccessMode.Cache:
                    observable = BlobCache.LocalMachine.GetObject<T>(key)
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
