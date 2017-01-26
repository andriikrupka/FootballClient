using System;
using FootballClient.DataAccess.Request.Parsers;
using FootballClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Globalization;
using System.Reactive.Linq;
using FootballClient.DataAccess.Parsers;
using FootballClient.Models.News;

namespace FootballClient.DataAccess.Providers
{
    public class FeedNewsProvider
    {
        private readonly IRestClient _restClient;

        public FeedNewsProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IList<NewsItem>> LoadNewsAsync(DateTimeOffset dateTime, string code, Action<Exception> onError = null)
        {
            var uriBuider = new ParameterUriBuilder("http://services.football.ua/api/News/GetArchive");
            uriBuider.Add("pageId", code);
            uriBuider.Add("teamId", "0");
            uriBuider.Add("datePublish", dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture));
            uriBuider.Add("count", "20");
            uriBuider.Add("imageFormat", "s318x171");
            uriBuider.Add("callback", "");
            uriBuider.Add("_1480515720809", "");

            var settings = new RestSettings<NewsResponse>();
            settings.AddMode(RequestAccessMode.Server)
                    .AddParser(new JsonParser<NewsResponse>())
                    .AddRequestMessage(new HttpRequestMessage(HttpMethod.Get, uriBuider.BuildParametersUri()));

            var newsResponse = await _restClient.SendAsync(settings, onError: onError);
            try
            {
                var fillDetailsTask = newsResponse.News.Select(x => GetDetailsAsync(x.Id, x.DateTimeOffsetPublish, false));
                await Task.WhenAll(fillDetailsTask);
            }
            catch (Exception)
            {
                // ignore fill details task
            }

            return newsResponse.News;
        }

        public async Task<Models.News.RssNewsDetailsChannelItem> GetDetailsAsync(int id, DateTimeOffset publishedDate, bool requestIfExists = false)
        {
            var uriBuilder = new ParameterUriBuilder("http://football.ua/hnd/Android/NewsItem.ashx");
            uriBuilder.Add("news_id", id.ToString());
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuilder.BuildParametersUri());
            var settings = new RestSettings<rss>();
            settings.AddParser(new XmlParser<rss>())
                    .AddRequestMessage(requestMessage)
                    .AddMode(RequestAccessMode.Server);

            var result = await _restClient.SendAsync<rss>(settings, (Tuple<rss, DateTimeOffset?> tupleData) =>
            {
                if (!tupleData.Item2.HasValue)
                    return true;

                return requestIfExists && Math.Abs((publishedDate.UtcDateTime - tupleData.Item2.Value).TotalDays) < 1;
            });

        
            return result?.Channel?.Item;
        }
    }
}