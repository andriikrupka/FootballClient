using System;
using System.Net;
using System.Runtime.Serialization;

namespace FootballClient.Models
{
    public class NewsItem
    {
        private string _formattedDatePublish;

        public int Id { get; set; }
        public int PageId { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string TextIntro { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
        public string DatePublish { get; set; }

        public string FormattedDatePublish
        {
            get
            {
                if (string.IsNullOrEmpty(_formattedDatePublish))
                {
                    DateTimeOffset publishedDateTime;
                    if (DateTimeOffset.TryParse(DatePublish, out publishedDateTime))
                    {
                        _formattedDatePublish = publishedDateTime.ToString("dd MMMM yyyy, HH:mm");
                    }
                    else
                    {
                        _formattedDatePublish = DatePublish;
                    }
                }

                return _formattedDatePublish;
            }
        }

        [IgnoreDataMember]
        public DateTimeOffset DateTimeOffsetPublish { get; private set; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext ctx)
        {
            Title = WebUtility.HtmlDecode(Title ?? string.Empty);
            TextIntro = WebUtility.HtmlDecode(TextIntro ?? string.Empty);
        }
    }
}
