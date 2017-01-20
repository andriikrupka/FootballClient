using System;
using FootballClient.DataAccess.Request;
using FootballClient.DataAccess.Request.Parsers;
using FootballClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections;
using System.Text;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
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

        //public static List<CategoryFinder> CategoryFinderCollection = new List<CategoryFinder>();

        public FeedNewsProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }



        public async Task<IList<News>> LoadNewsAsync(DateTimeOffset dateTime, string code)
        {
            var uriBuider = new ParameterUriBuilder("http://services.football.ua/api/News/GetArchive");
            uriBuider.Add("pageId", code);
            uriBuider.Add("teamId", "0");
            uriBuider.Add("datePublish", dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture));
            uriBuider.Add("count", "25");
            uriBuider.Add("imageFormat", "s318x171");
            uriBuider.Add("callback", "");
            uriBuider.Add("_1480515720809", "");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuider.BuildParametersUri());
            var task = await _restClient.SendMessageAsync(requestMessage, new JsonParser<NewsResponse>()).ToObservable();

            try
            {
                var fillDetailsTask = task.News.Select(x => GetDetails(x.Id, x.DateTimeOffsetPublish));
                var rsss = await Task.WhenAll(fillDetailsTask);
                //foreach(var item in task.News.Where(x=> CategoryFinderCollection.FirstOrDefault(c=> c.PageId == x.PageId.ToString()) == null))
                //{
                //    var rssItem = rsss.FirstOrDefault(x => x.channel.item.Id == item.Id.ToString());
                //    if (rssItem != null)
                //    {
                //        CategoryFinderCollection.Add(new CategoryFinder()
                //        {
                //            PageId = item.PageId.ToString(),
                //            CategoryName = rssItem.channel.item.category
                //        });
                //    }
                //}
            }
            catch (Exception ex)
            {

            }

            return task.News;
        }

        public Task<rss> GetDetails(int id, DateTimeOffset publishedDate)
        {
            var uriBuilder = new ParameterUriBuilder("http://football.ua/hnd/Android/NewsItem.ashx");
            uriBuilder.Add("news_id", id.ToString());
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuilder.BuildParametersUri());
            var settings = new RequestSettings<rss>();
            settings.AddParser(new XmlParser<rss>())
                    .AddRequestMessage(requestMessage)
                    .AddMode(RequestAccessMode.Server)
                    .AddFetchPredicate(cachedDate =>
            {
                var needFetch = false;
                var diffBetweenNow = cachedDate - DateTimeOffset.UtcNow;
                var isFreshCache = Math.Abs(diffBetweenNow.TotalMinutes) < 60;
                if (isFreshCache)
                {
                    var absMinutes = Math.Abs((publishedDate.UtcDateTime - DateTimeOffset.UtcNow).TotalMinutes);
                    if (absMinutes > 100)
                    {
                        needFetch = false;
                    }
                    else
                    {
                        needFetch = true;
                    }
                }
                else
                {
                    var diffBetweenPublished = publishedDate.UtcDateTime - cachedDate;
                    needFetch = Math.Abs(diffBetweenPublished.TotalMinutes) < 60;
                }

                return needFetch;
            });

            var result = _restClient.SendMessageAsync<rss>(settings);

            return result;
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