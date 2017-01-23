using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using FootballClient.Models;
using Prism.Windows.AppModel;
using Prism.Commands;
using Windows.System;
using FootballClient.DataAccess.Providers;

namespace FootballClient.UWP.ViewModels
{
    public class NewsDetailsPageViewModel : BaseViewModel
    {
        private readonly ISessionStateService _sessionStateService;
        private INavigationService _navigationService;
        private readonly FeedNewsProvider _feedNewsProvider;

        public NewsDetailsPageViewModel(ISessionStateService sessionStateService,
                                        INavigationService navigationService,
                                        FeedNewsProvider feedNewsProvider)
        {
            _navigationService = navigationService;
            _sessionStateService = sessionStateService;
            _feedNewsProvider = feedNewsProvider;
            ViewInWebCommand = new DelegateCommand(ViewInWebExecute);
            ViewCommentsCommand = new DelegateCommand(ViewCommentsExecute);
        }

        public DelegateCommand ViewInWebCommand { get; private set; }

        public DelegateCommand ViewCommentsCommand { get; private set; }

        [RestorableState]
        public News CurrentNews { get; private set; }
        public rssChannelItem NewsDetails { get; private set; }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            var news = _sessionStateService.SessionState["CurrentNews"] as News;
            NewsDetails = await _feedNewsProvider.GetDetailsAsync(news.Id, news.DateTimeOffsetPublish, true);
        }

        private void ViewInWebExecute()
        {
            var uri = new Uri(CurrentNews.Link);
            var ingore = Launcher.LaunchUriAsync(uri);
        }

        private void ViewCommentsExecute()
        {
            _navigationService.Navigate(NavigationPages.Comments, null);
        }
    }
}
