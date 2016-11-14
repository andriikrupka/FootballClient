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
        private FictionProvider _fictionProvider;
        public FictionViewModel(FictionProvider fictionProvider, Category category)
        {
            _fictionProvider = fictionProvider;
            Category = category;
            FeedItems = new IncrementalObservableCollection<FeedItem>(LoadMoreItemsAsync);
        }

        public IncrementalObservableCollection<FeedItem> FeedItems { get; set; }

        public Category Category { get; }
        public bool IsError { get; private set; }

        public async void ReloadItemsAsync()
        {
            FeedItems.Clear();
            FeedItems.CanMoreItems();
            await FeedItems.LoadMoreItemsAsync(0);
        }

        private async Task<IList<FeedItem>> LoadMoreItemsAsync()
        {
            BusyCount++;
            var items = new List<FeedItem>();
            IsError = false;

            try
            {
                var response = await _fictionProvider.LoadFictionAsync(FeedItems.LastOrDefault(), Category.Code);
                items.AddRange(response);
            }
            catch (System.Exception)
            {
                var response = await _fictionProvider.LoadFictionAsync(FeedItems.LastOrDefault(), Category.Code, DataAccessMode.Cache);
                if (response != null)
                {
                    items.AddRange(response);
                }
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