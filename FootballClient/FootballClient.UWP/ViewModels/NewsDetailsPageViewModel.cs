using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using FootballClient.Models;
using Prism.Windows.AppModel;

namespace FootballClient.UWP.ViewModels
{
    public class NewsDetailsPageViewModel : BaseViewModel
    {
        private readonly ISessionStateService _sessionStateService;
        public NewsDetailsPageViewModel(ISessionStateService sessionStateService)
        {
            _sessionStateService = sessionStateService;
        }

        [RestorableState]
        public FeedItem CurrentNews { get; private set; }
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            CurrentNews = _sessionStateService.SessionState["CurrentNews"] as FeedItem;
        }
    }
}
