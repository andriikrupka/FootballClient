using System.Collections.Generic;

namespace FootballClient.Models
{
    public class NewsResponse
    {
        public string LastDatePublish { get; set; }
        public List<NewsItem> News { get; set; }
    }
}
