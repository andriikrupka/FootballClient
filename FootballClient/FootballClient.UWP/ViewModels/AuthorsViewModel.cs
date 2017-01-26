using FootballClient.DataAccess.Providers;
using FootballClient.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System;
using FootballClient.DataAccess;

namespace FootballClient.UWP.ViewModels
{
    public class AuthorsViewModel : BaseViewModel
    {
        private readonly AuthorsProvider _authorsProvider;
        public IncrementalObservableCollection<FeedItem> FeedItems { get; set; }
        public AuthorsViewModel(AuthorsProvider authorsProvider, Category category)
        {
            _authorsProvider = authorsProvider;
            Category = category;
            FeedItems = new IncrementalObservableCollection<FeedItem>(LoadMoreItemsAsync);
        }

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
                var response = await _authorsProvider.LoadAuthorsFeedAsync(FeedItems.LastOrDefault(), this.Category.Code);
                items.AddRange(response);
            }
            catch (Exception)
            {

                //IsError = true;

                var response = await _authorsProvider.LoadAuthorsFeedAsync(FeedItems.LastOrDefault(), this.Category.Code);
                if (response != null)
                {
                    items.AddRange(response);
                }
            }

            //foreach (var item in items)
            //{
            //    item.IsReaded = DataProviders.DataBaseProvider.IsItemExists<FeedItem>(item.Id);
            //}

            base.BusyCount--;
            return items;
        }
    }
}
