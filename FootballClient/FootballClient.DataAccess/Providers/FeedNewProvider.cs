using System;
using FootballClient.DataAccess.Request.Parsers;
using FootballClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Globalization;
using System.Reactive.Threading.Tasks;
using System.Reactive.Linq;
using FootballClient.DataAccess.Parsers;
using System.Xml.Serialization;

namespace FootballClient.DataAccess.Providers
{
    public class CategoryFinder
    {
        public string CategoryName { get; set; }

        public string PageId { get; set; }

        public override bool Equals(object obj)
        {
            var isEquals = false;
            var other = obj as CategoryFinder;
            isEquals = other?.PageId?.Equals(this.PageId) ?? false;

            return false;
        }

        public override int GetHashCode()
        {
            return this.PageId?.GetHashCode() ?? 0;
        }
    }

    public class FeedNewsProvider
    {
        private readonly IRestClient _restClient;

        public static List<CategoryFinder> CategoryFinderCollection = new List<CategoryFinder>();

        public FeedNewsProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IList<News>> LoadNewsAsync(DateTimeOffset dateTime, string code, RequestAccessMode mode)
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
            settings.AddMode(mode)
                    .AddParser(new JsonParser<NewsResponse>())
                    .AddRequestMessage(new HttpRequestMessage(HttpMethod.Get, uriBuider.BuildParametersUri()));

            var newsResponse = await _restClient.SendAsync(settings, null).ToObservable();
            try
            {
                var fillDetailsTask = newsResponse.News.Select(x => GetDetailsAsync(x.Id, x.DateTimeOffsetPublish));
                await Task.WhenAll(fillDetailsTask);
            }
            catch (Exception)
            {
                // ignore fill details task
            }

            return newsResponse.News;
        }

        public async Task<rssChannelItem> GetDetailsAsync(int id, DateTimeOffset publishedDate, bool requestIfExists = false)
        {
            var uriBuilder = new ParameterUriBuilder("http://football.ua/hnd/Android/NewsItem.ashx");
            uriBuilder.Add("news_id", id.ToString());
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuilder.BuildParametersUri());
            var settings = new RestSettings<rss>();
            settings.AddParser(new XmlParser<rss>())
                    .AddRequestMessage(requestMessage)
                    .AddMode(RequestAccessMode.Server);

            var result = await _restClient.SendAsync<rss>(settings, tupleData =>
            {
                if (!tupleData.Item2.HasValue)
                    return true;

                return requestIfExists && Math.Abs((publishedDate.UtcDateTime - tupleData.Item2.Value).TotalDays) < 1;
            });

            result.channel.item.id = id.ToString();
            return result?.channel?.item;
        }
    }

    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class rss
    {
        public rssChannel channel { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public partial class rssChannel
    {
        public rssChannelItem item { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public partial class rssChannelItem
    {
        public string id { get; set; }

        public string title { get; set; }

        public string link { get; set; }

        public uint date { get; set; }

        public string description { get; set; }

        public string category { get; set; }

        public string article { get; set; }

        public rssChannelItemImg img { get; set; }

        [XmlAttribute()]
        public string type { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public partial class rssChannelItemImg
    {
        [XmlAttribute()]
        public ushort height { get; set; }

        [XmlAttribute()]
        public ushort width { get; set; }

        [XmlText()]
        public string Value { get; set; }
    }
}