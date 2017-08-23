using System;
using FootballClient.DataAccess.Providers;
using FootballClient.Models.News;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace FootballClientForms.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NewsDetailsPageViewModel : BindableBase, INavigationAware
    {
        private readonly FeedNewsProvider feedNewsProvider;
        private readonly INavigationService navigationService;
        private int newsId;
        private string publishedDateString;

        public NewsDetailsPageViewModel(FeedNewsProvider feedNewsProvider, INavigationService navigationService)
        {
            this.feedNewsProvider = feedNewsProvider;
            this.navigationService = navigationService;
            ViewCommentsCommand = new DelegateCommand(ViewCommentsExecute);
        }


        private void ViewCommentsExecute()
        {
            var parameters = new NavigationParameters
            {
                {"Id", newsId },
                {"Type", CommentType.News},
                {"Title", CurrentItem.Title},
                {"PublishedDateString", publishedDateString}
            };

            navigationService.NavigateAsync(PageSource.Comments, parameters);
        }

        public DelegateCommand ViewCommentsCommand { get; }

        public RssNewsDetailsChannelItem CurrentItem { get; private set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
			if (parameters.ContainsKey("NewsId"))
			{
				newsId = parameters.GetValue<int>("NewsId");
				var dateTime = parameters.GetValue<DateTimeOffset>("PublishedDate");
                publishedDateString = parameters.GetValue<string>("PublishedDateString");

				CurrentItem = await feedNewsProvider.GetDetailsAsync(newsId, dateTime);

				RaisePropertyChanged(nameof(CurrentItem));
			}
        }
    }
}
