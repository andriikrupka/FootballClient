using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FootballClient.Models;
using FootballClient.DataAccess.Providers;
using System.Collections.Generic;
using System;
using FootballClient.DataAccess;

namespace FootballClient.UWP.ViewModels
{
    public class FictionViewModel : BaseViewModel
    {
        private FeedNewsProvider _feedNewsProvider;
        public FictionViewModel(FeedNewsProvider feedNewsProvider, Category category)
        {
            _feedNewsProvider = feedNewsProvider;
            Category = category;
            FeedItems = new IncrementalObservableCollection<News>(LoadMoreItemsAsync);
        }

        public IncrementalObservableCollection<News> FeedItems { get; set; }

        public Category Category { get; }
        public bool IsError { get; private set; }

        public async void ReloadItemsAsync()
        {
            FeedItems.Clear();
            FeedItems.CanMoreItems();
            await FeedItems.LoadMoreItemsAsync(0);
        }

        private async Task<IList<News>> LoadMoreItemsAsync()
        {
            BusyCount++;
            var items = new List<News>();
            IsError = false;
            var lastItem = FeedItems.LastOrDefault();
            var dateTime = lastItem == null ? DateTimeOffset.Now : DateTimeOffset.Parse(lastItem.DatePublish);

            try
            {
                var response = await _feedNewsProvider.LoadNewsAsync(dateTime, Category.Code);
                items.AddRange(response);
            }
            catch (System.Exception)
            {
                //var response = await _feedNewsProvider.LoadNewsAsync(FeedItems.LastOrDefault(), Category.Code, RequestAccessMode.Cache);
                //if (response != null)
                //{
                //    items.AddRange(response);
                //}
                IsError = true;

            }

            //foreach (var item in items)
            //{
            //    item.IsReaded = DataProviders.DataBaseProvider.IsItemExists<FeedItem>(item.Id);
            //}

            BusyCount--;
            return items;
        }
    }
}