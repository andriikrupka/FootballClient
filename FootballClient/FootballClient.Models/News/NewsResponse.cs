using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClient.Models
{
    public class NewsResponse
    {
        public string LastDatePublish { get; set; }
        public List<News> News { get; set; }
    }
}
