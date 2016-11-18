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

namespace FootballClient.UWP.ViewModels
{
    public class NewsDetailsPageViewModel : BaseViewModel
    {
        private readonly ISessionStateService _sessionStateService;
        private INavigationService _navigationService;

        public NewsDetailsPageViewModel(ISessionStateService sessionStateService,
                                        INavigationService navigationService)
        {
            _navigationService = navigationService;
            _sessionStateService = sessionStateService;
            ViewInWebCommand = new DelegateCommand(ViewInWebExecute);
            ViewCommentsCommand = new DelegateCommand(ViewCommentsExecute);
        }

        public DelegateCommand ViewInWebCommand { get; private set; }

        public DelegateCommand ViewCommentsCommand { get; private set; }

        [RestorableState]
        public FeedItem CurrentNews { get; private set; }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            CurrentNews = _sessionStateService.SessionState["CurrentNews"] as FeedItem;
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
