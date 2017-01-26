using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Windows.Navigation;
using FootballClient.UWP.Views;
using Prism.Commands;

namespace FootballClient.UWP.ViewModels
{
    public class MenuItem
    {
        public Type NavigationPageType { get; set; }

        public string Title { get; set; }

        public string IconPathData { get; set; }

        public string PageToken { get; set; }
    }

    public class AppShellViewModel : BaseViewModel
    {
        private Type _currentPage;
        private readonly IFrameFacade _frameFacade;
        private readonly INavigationService _navigationService;

        public AppShellViewModel(INavigationService navigationService, IFrameFacade frameFacade)
        {
            _navigationService = navigationService;
            _frameFacade = frameFacade;
            _frameFacade.NavigatedTo += OnNavigatedTo;

            NavigationItems = new List<MenuItem>()
            {
                new MenuItem
                {
                    IconPathData = "F1M12,31C12,31 35,31 35,31 35,31 35,34 35,34 35,34 12,34 12,34 12,34 12,31 12,31z M12,25C12,25 35,25 35,25 35,25 35,28 35,28 35,28 12,28 12,28 12,28 12,25 12,25z M26,19C26,19 35,19 35,19 35,19 35,22 35,22 35,22 26,22 26,22 26,22 26,19 26,19z M26,13C26,13 35,13 35,13 35,13 35,16 35,16 35,16 26,16 26,16 26,16 26,13 26,13z M26,7C26,7 35,7 35,7 35,7 35,10 35,10 35,10 26,10 26,10 26,10 26,7 26,7z M12,7C12,7 23,7 23,7 23,7 23,22 23,22 23,22 12,22 12,22 12,22 12,7 12,7z M9,4C9,4 9,34 9,34 9,34 7,34 7,34 7,34 7,10 7,10 7,10 4,10 4,10 4,10 4,38 4,38 4,38 38,38 38,38 38,38 38,4 38,4 38,4 9,4 9,4z M7,2C7,2 40,2 40,2 40,2 40,40 40,40 40,40 2,40 2,40 2,40 2,8 2,8 2,8 7,8 7,8 7,8 7,2 7,2z",
                    Title = "Новости",
                    NavigationPageType = typeof(MainPage),
                    PageToken = NavigationPages.Main
                },

                new MenuItem
                {
                    IconPathData = "F1M19,24C19,24 22,24 22,24 22,24 22,27 22,27 22,27 19,27 19,27 19,27 19,24 19,24z M19,16C19,16 22,16 22,16 22,16 22,19 22,19 22,19 19,19 19,19 19,19 19,16 19,16z M26,14C26,14,29.516,14,32,14L32.152,14 32.469,14C32.75,14,33.125,14,33.5,14L33.594,14 34.531,14 34.613,14 35,14C35,14,35,14.188,35,14.469L35,14.645 35,15.5 35,16.344 35,16.531 35,17 35,18.746C35,20.064 35,21.515 35,20 35,20 35,23 35,22.817L35,23 35,24.624C35,25.167 35,25.637 35,26 35,26 35,26.75 35,26.656L35,27.5 35,28.355C35,28.561 35,28.722 35,28.531 35,28.766 35,28.824 35,28.773L35,29 34.823,29 34.613,29C34.789,29,34.672,29,34.531,29L33.934,29 33.909,29 33.594,29C33.875,29,33.688,29,33.5,29L32.79,29C32.835,29,32.823,29,32.772,29L32.469,29C32.671,29 32.416,29 32.152,29 32.188,29 32,29 32,29 29.516,29 26,29 26,29 26,29 26,26 26,26 26,26 28.25,26 30.5,26L32,26 32,24.254 32,23 31.5,23C29.75,23 28,23 28,23 28,23 28,20 28,20 28,20 29.75,20 31.5,20L32,20 32,18.746 32,17 30.5,17C28.25,17 26,17 26,17 26,17 26,14 26,14z M12,14C12,14 15,14 15,14 15,14 15,16.109 15,18 15,18 15,18.188 15,18.469L15,18.746 15,19.5C15,19.875,15,20.25,15,20.531L15,21 15,21.5C15,21.734,15,21.969,15,22L15,22.167C15,22.365,15,22.527,15,22.469L15,22.617 15,22.634 15,23.5C15,23.875 15,24.25 15,24.254 15,24.364 15,24.473 15,24.531L15,24.534 15,24.868 15,25 15,25.211C15,25.321,15,25.324,15,25.255L15,26 15,26.469C15,26.539 15,26.615 15,26.656 15,26.715 15,26.703 15,26.659L15,26.844 15,27.5 15,27.509C15,27.598,15,27.652,15,27.655L15,28.029C15,28.183 15,28.303 15,28.355 15,28.458 15,28.55 15,28.531L15,28.592 15,28.773 15,29C15,29 14.812,29 14.531,29 14.25,29 13.875,29 13.5,29 13.125,29 12.75,29 12.469,29 12.188,29 12,29 12,29 12,29 12,28.812 12,28.531L12,28.355 12,27.5 12,26.656 12,26.469C12,26.188,12,26,12,26L12,25 12,24.531 12,24.254 12,23.5 12,22.839 12,22.654 12,22.634C12,22.725 12,22.707 12,22.617 12,22.615 12,22.539 12,22.469 12,22.527 12,22.365 12,22.167L12,22C12,21.969,12,21.734,12,21.5L12,21 12,20.531 12,20C12,20 9,20 9,20 9,20 9,17 9,17 9,17 12,17 12,17L12,16.344C12,14.938,12,14,12,14z M4,4C4,4 4,38 4,38 4,38 40,38 40,38 40,38 40,4 40,4 40,4 4,4 4,4z M2,2C2,2 42,2 42,2 42,2 42,40 42,40 42,40 2,40 2,40 2,40 2,2 2,2z",
                    Title = "Матчи",
                    NavigationPageType = typeof(MatchesPage),
                    PageToken = NavigationPages.Matches
                },

                new MenuItem
                {
                    IconPathData = "F1M12,37C12,37 36,37 36,37 36,37 36,40 36,40 36,40 12,40 12,40 12,40 12,37 12,37z M24,6C24,6 25.975,10.881 25.975,10.881 25.975,10.881 31,11.347 31,11.347 31,11.347 27.196,14.83 27.196,14.83 27.196,14.83 28.326,19.999 28.326,19.999 28.326,19.999 24,17.27 24,17.27 24,17.27 19.674,19.999 19.674,19.999 19.674,19.999 20.805,14.83 20.805,14.83 20.805,14.83 17,11.347 17,11.347 17,11.347 22.025,10.881 22.025,10.881 22.025,10.881 24,6 24,6z M38,5C38,5 46,5 46,5 46,5 42,20 42,20 42,20 38,20 38,20 38,20 38,19.766 38,19.355L38,18 38.5,18C39.25,18 40,18 40,18 40,18 43,7 43,7 43,7 41.5,7 40,7L38,7 38,5.645C38,5.234,38,5,38,5z M2,5C2,5 10,5 10,5 10,5 10,5.938 10,7 10,7 5,7 5,7 5,7 8,18 8,18 8,18 10,18 10,18L10,19.355C10,19.766 10,20 10,20 10,20 6,20 6,20 6,20 2,5 2,5z M14,4C14,4 14,22 14,22 14,22 24.03,28 24.03,28 24.03,28 34,22 34,22 34,22 34,4 34,4 34,4 14,4 14,4z M12,2C12,2 36,2 36,2 36,2 36,22.9 36,22.9 36,22.9 29.215,27.456 25.822,29.734L25,30.286 25,31.5 25,33 26.252,33C28.363,33 30.052,33 30.052,33 30.052,33 32,36 32,36 32,36 16,36 16,36 16,36 18.043,33 18.043,33 18.043,33 19.732,33 21.843,33L23,33 23,31.5 23,30.38 22.072,29.766C18.715,27.544 12,23.1 12,23.1 12,23.1 12,2 12,2z",
                    Title = "Турниры",
                    NavigationPageType = typeof(TournamentsPage),
                    PageToken = NavigationPages.Tournaments
                },

                new MenuItem
                {
                    IconPathData = "F1M29.885,16.008C29.885,16.008 34.995,21.139 34.995,21.139 34.995,21.139 34.938,35 34.938,35 34.938,35 7,35 7,35 7,35 7.006,30.056 7.006,30.056 7.006,30.056 13.052,22.776 13.052,22.776 13.052,22.776 18.401,27.466 18.401,27.466 18.401,27.466 29.885,16.008 29.885,16.008z M12,9C10.343,9 9,10.343 9,12 9,13.657 10.343,15 12,15 13.657,15 15,13.657 15,12 15,10.343 13.657,9 12,9z M12,7C14.761,7 17,9.239 17,12 17,14.761 14.761,17 12,17 9.239,17 7,14.761 7,12 7,9.239 9.239,7 12,7z M4,4C4,4 4,38 4,38 4,38 38,38 38,38 38,38 38,4 38,4 38,4 4,4 4,4z M2,2C2,2 40,2 40,2 40,2 40,40 40,40 40,40 2,40 2,40 2,40 2,2 2,2z",
                    Title = "Фотографии",
                    NavigationPageType = typeof(PhotosPage),
                    PageToken = NavigationPages.Photos
                }
            };

            BackPressedCommand = new DelegateCommand(BackPressedExecute);
        }

        private void BackPressedExecute()
        {
            if (_navigationService.CanGoBack())
            {
                _navigationService.GoBack();
            }
        }

        public List<MenuItem> NavigationItems { get; set; }

        public DelegateCommand BackPressedCommand { get; set; }
        public MenuItem SelectedItem { get; set; }
        public event EventHandler<MenuItem> SelectedItemChanged;
        public void OnSelectedItemChanged()
        {
            if (SelectedItem != null && _currentPage != SelectedItem.NavigationPageType)
            {
                _navigationService.ClearHistory();
                _navigationService.Navigate(SelectedItem.PageToken, null);
            }

            SelectedItemChanged?.Invoke(this, SelectedItem);
        }

        private void OnNavigatedTo(object sender, NavigatedToEventArgs e)
        {
            _currentPage = e.SourcePageType;
            SelectedItem = NavigationItems.FirstOrDefault(x => x.NavigationPageType == _currentPage);
        }
    }
}
