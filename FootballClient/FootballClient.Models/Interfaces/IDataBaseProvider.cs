using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClient.Models.Interfaces
{
    public interface IDataBaseProvider
    {
        void InsertFeedItem(FeedItem feedItem);
        bool IsItemExists<T>(object key) where T : new();
    }
}
