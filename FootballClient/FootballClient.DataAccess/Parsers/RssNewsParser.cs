using System.Threading.Tasks;

namespace FootballClient.DataAccess.Request.Parsers
{
    using FootballClient.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml.Serialization;

    public class RssFeedParser : IParserStrategy<List<FeedItem>>
    {
        public List<FeedItem> Parse(string data)
        {
            var response = new List<FeedItem>();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            XmlSerializer serializer = new XmlSerializer(typeof(Rss));
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(data ?? ""));
            var result = (Rss)serializer.Deserialize(stream);

            foreach (var item in result.Channel.Items)
            {
                var feedItem = new FeedItem
                {
                    Annotation = WebUtility.HtmlDecode(item.Annotation),
                    Author = WebUtility.HtmlDecode(item.Author),
                    Category = WebUtility.HtmlDecode(item.Category),
                    Description = WebUtility.HtmlDecode(item.Description),
                    DescriptionImage = WebUtility.HtmlDecode(item.DescriptionImage),
                    DescriptionImagenote = WebUtility.HtmlDecode(item.DescriptionImageNote),
                    Highlighted = item.Highlighted,
                    Id = item.Id,
                    Link = WebUtility.HtmlDecode(item.Link),
                    PubDate = WebUtility.HtmlDecode(item.PubDate),
                    Thumbnail = WebUtility.HtmlDecode(item.Thumbnail),
                    ThumbnailBig = WebUtility.HtmlDecode(item.ThumbnailBig),
                    ThumbnailLarge = WebUtility.HtmlDecode(item.ThumbnailLarge),
                    ThumbnailSmall = WebUtility.HtmlDecode(item.ThumbnailSmall),
                    Title = WebUtility.HtmlDecode(item.Title)
                };

                response.Add(feedItem);
            }

            return response;
        }
    }
}
