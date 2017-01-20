using Prism.Windows.Mvvm;
using System;
using System.Collections.Generic;
using Prism.Windows.Navigation;
using FootballClient.Models;
using Prism.Commands;
using Prism.Windows.AppModel;
using Newtonsoft.Json;
using FootballClient.DataAccess.Providers;

namespace FootballClient.UWP.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly NewsListViewModel _newsListViewModel;
        private readonly INavigationService _navigationService;
        private readonly FictionListViewModel _fictionListViewModel;
        private readonly AuthorsListViewModel _authorsListViewModel;
        private readonly ISessionStateService _sessionStateService;

        private bool _isInitialized;
        private readonly CommonViewModel _commonViewModel;

        public MainPageViewModel(INavigationService navigationService,
                                 ISessionStateService sessionStateService,
                                 NewsListViewModel newsListViewModel,
                                 FictionListViewModel fictionListViewModel,
                                 AuthorsListViewModel authorsListViewModel,
                                 CommonViewModel commonViewModel)
        {
            _sessionStateService = sessionStateService;
            _fictionListViewModel = fictionListViewModel;
            _navigationService = navigationService;
            _authorsListViewModel = authorsListViewModel;
            _newsListViewModel = newsListViewModel;
            _commonViewModel = commonViewModel;
            ViewDetailsCommand = new DelegateCommand<FeedItem>(ViewDetailsExecute);
            RefreshTabCommand = new DelegateCommand(RefreshExecute);
        }

        private void RefreshExecute()
        {
            if (SelectedTabIndex == 0)
            {
                NewsListViewModel.SelectedViewModel?.ReloadItemsAsync();
            }
            else if (SelectedTabIndex == 1)
            {
                FictionListViewModel.SelectedViewModel?.ReloadItemsAsync();

            }
            else if (SelectedTabIndex == 2)
            {
                AuthorsListViewModel.SelectedViewModel.ReloadItemsAsync();
            }
        }

        public int SelectedTabIndex { get; set; }

        public DelegateCommand<FeedItem> ViewDetailsCommand { get; }

        public DelegateCommand RefreshTabCommand { get; }
        public NewsListViewModel NewsListViewModel
        {
            get
            {
                return _newsListViewModel;
            }
        }

        public FictionListViewModel FictionListViewModel
        {
            get
            {
                return _fictionListViewModel;
            }
        }

        public AuthorsListViewModel AuthorsListViewModel
        {
            get
            {
                return _authorsListViewModel;
            }
        }

        public CommonViewModel CommonViewModel
        {
            get
            {
                return _commonViewModel;
            }
        }

        private void ViewDetailsExecute(FeedItem item)
        {
            _sessionStateService.SessionState["CurrentNews"] = item;
            _navigationService.Navigate(NavigationPages.NewsDetails, null);
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            if (!_isInitialized)
            {
                _isInitialized = true;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                FictionListViewModel.InitializeViewModelAsync();
                AuthorsListViewModel.InitializeViewModelAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }
    }
}
