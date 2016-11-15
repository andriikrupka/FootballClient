using System;
using System.Threading.Tasks;
using Prism.Unity.Windows;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.Unity;
using Prism.Windows.AppModel;
using Windows.ApplicationModel.Resources;
using Prism.Mvvm;
using FootballClient.DataAccess;
using FootballClient.DataAccess.Providers;
using FootballClient.UWP.ViewModels;
using Prism.Events;
using System.Globalization;
using Prism.Windows.Navigation;
using Windows.UI.ViewManagement;
using FootballClient.Models;
using Windows.Foundation.Metadata;
using Windows.UI;
using Akavache;

namespace FootballClient.UWP
{
    sealed partial class App : PrismUnityApplication
    {
        private IFrameFacade _rootFrameFacade;

        public App()
        {
            this.InitializeComponent();
            BlobCache.ApplicationName = "FootballClient";
        }

        protected override INavigationService OnCreateNavigationService(IFrameFacade rootFrame)
        {
            _rootFrameFacade = rootFrame;
            Container.RegisterInstance<IFrameFacade>(rootFrame);
            return base.OnCreateNavigationService(rootFrame);
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            Container.RegisterInstance<IEventAggregator>(new EventAggregator());
            Container.RegisterInstance<IRestClient>(new RestClient());
            Container.RegisterType<FeedNewsProvider>();
            Container.RegisterType<FictionProvider>();
            Container.RegisterType<AuthorsProvider>();
            Container.RegisterType<CommentsProvider>();

            Container.RegisterType<CommonViewModel>(new ContainerControlledLifetimeManager());
            Container.RegisterType<NewsListViewModel>();
            Container.RegisterType<FictionListViewModel>();
            Container.RegisterType<AuthorsListViewModel>();
            
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewModelTypeName = string.Format(CultureInfo.InvariantCulture, "FootballClient.UWP.ViewModels.{0}ViewModel, FootballClient.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", viewType.Name);
                var viewModelType = Type.GetType(viewModelTypeName);
                return viewModelType;
            });

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = Colors.Green;
                    statusBar.ForegroundColor = Colors.White;
                }
            }

            return base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            NavigationService.Navigate(NavigationPages.Main, null);
            return Task.FromResult(true);
        }

        protected override void OnRegisterKnownTypesForSerialization()
        {
            base.OnRegisterKnownTypesForSerialization();
            SessionStateService.RegisterKnownType(typeof(FeedItem));
        }
    }
}
