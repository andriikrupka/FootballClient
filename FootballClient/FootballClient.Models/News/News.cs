using System;
using System.Runtime.Serialization;

namespace FootballClient.Models
{
    public class News
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string TextIntro { get; set; }
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }
        public string DatePublish { get; set; }

        [IgnoreDataMember]
        public DateTimeOffset DateTimeOffsetPublish { get; private set; }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext ctx)
        {
            DateTimeOffset publishedDateTime;
            if (DateTimeOffset.TryParse(DatePublish, out publishedDateTime))
            {
                DateTimeOffsetPublish = publishedDateTime;
            }
        }
    }
}
