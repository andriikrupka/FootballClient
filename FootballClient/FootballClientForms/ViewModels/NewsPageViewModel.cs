using System;
using System.Collections.Generic;
using FootballClient.DataAccess.Providers;
using FootballClient.Models;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Navigation;
using System.Linq;

namespace FootballClientForms.ViewModels
{
    public class NewsPageViewModel : BindableBase, INavigationAware
    {
        private readonly FeedNewsProvider feedNewsProvider;
        private readonly INavigationService navigationService;

        public NewsPageViewModel(FeedNewsProvider feedNewsProvider, INavigationService navigationService)
        {
            this.feedNewsProvider = feedNewsProvider;
            this.navigationService = navigationService;
            NewsItems = new ObservableRangeCollection<NewsItem>();

            ViewDetailsCommand = new DelegateCommand<NewsItem>(ViewDetailExecute);
            LoadMoreCommand = new DelegateCommand(LoadMore);
        }

        private async void LoadMore()
        {
            try
            {
                var time = NewsItems.LastOrDefault()?.DateTimeOffsetPublish ?? DateTimeOffset.Now;
                var newItems = await feedNewsProvider.LoadNewsAsync(time, "1");
                NewsItems.AddRange(newItems);
            }
            catch (Exception ex)
            {

            }
        }

        private void ViewDetailExecute(NewsItem item)
        {
            var parameters = new NavigationParameters
            {
                { "NewsId", item.Id },
                { "PublishedDate", item.DateTimeOffsetPublish },
                { "PublishedDateString", item.FormattedDatePublish }
            };
            navigationService.NavigateAsync(PageSource.NewsDetails, parameters);  
        }


        public ObservableRangeCollection<NewsItem> NewsItems { get; private set; }
        public DelegateCommand<NewsItem> ViewDetailsCommand { get; private set; }
        public DelegateCommand LoadMoreCommand { get; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
			
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
		}

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
			if (parameters.GetNavigationMode() == NavigationMode.New)
			{
				var list = await feedNewsProvider.LoadNewsAsync(DateTimeOffset.Now, "1");
                NewsItems.AddRange(list);
			}
        }
    }
}