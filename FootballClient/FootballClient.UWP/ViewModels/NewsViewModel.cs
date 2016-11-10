using System;
using FootballClient.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FootballClient.DataAccess.Providers;
using System.Diagnostics;

namespace FootballClient.UWP.ViewModels
{
    public enum LoadingState
    {
        Normal,
        FirstLoading,
        Loading,
        ErrorWithEmptyList,
        Error
    }

    public class NewsViewModel : BaseViewModel
    {
        private FeedNewsProvider _feedNewsProvider;
        public NewsViewModel(FeedNewsProvider feedNewsProvider, Category category)
        {
            _feedNewsProvider = feedNewsProvider;
            Category = category;
            FeedItems = new IncrementalObservableCollection<FeedItem>(LoadMoreItemsAsync);
        }

        public IncrementalObservableCollection<FeedItem> FeedItems { get; set; }
        public Category Category { get; }

        public bool IsError { get; set; }

        public async void ReloadItemsAsync()
        {
            BusyCount++;
            FeedItems.HasMoreItems = false;
            FeedItems.Clear();
            //FeedItems.CanMoreItems();

            await FeedItems.LoadMoreItemsAsync(0);
            FeedItems.HasMoreItems = FeedItems.Count > 0;
            BusyCount--;
        }

        private async Task<IList<FeedItem>> LoadMoreItemsAsync()
        {
            BusyCount++;

            IsError = false;

            var items = new List<FeedItem>();

            try
            {
                var response = await _feedNewsProvider.LoadFeedNewsAsync(FeedItems.LastOrDefault(), this.Category.Code);
                items.AddRange(response);
            }
            catch (Exception)
            {
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
