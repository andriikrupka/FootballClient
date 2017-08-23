using System;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;

namespace FootballClient.Models
{
    public class NewsItem
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string TextIntro { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
        public string DatePublish { get; set; }

        public string FormattedDatePublish { get; private set; }

        [IgnoreDataMember]
        public DateTimeOffset DateTimeOffsetPublish { get; private set; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext ctx)
        {
            Title = WebUtility.HtmlDecode(Title ?? string.Empty);
            TextIntro = WebUtility.HtmlDecode(TextIntro ?? string.Empty);

            if (DateTimeOffset.TryParse(DatePublish, out DateTimeOffset publishedDateTime))
            {
                DateTimeOffsetPublish = publishedDateTime;
                FormattedDatePublish = publishedDateTime.ToString("dd MMMM yyyy, HH:mm", new CultureInfo("ru-RU"));
            }
            else
            {
                FormattedDatePublish = DatePublish;
            }
        }
    }
}
