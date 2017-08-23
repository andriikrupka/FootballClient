using FootballClientForms.ViewModels;
using FootballClientForms.Views;
using Prism.Unity;
using Xamarin.Forms;
using Prism.Unity.Extensions;
using Microsoft.Practices.Unity;
using FootballClient.DataAccess.Providers;
using FootballClient.DataAccess;
using Prism.Navigation;

namespace FootballClientForms
{
    public partial class App : PrismApplication
    {
		public App() : this(null) { }
		public App(IPlatformInitializer initializer = null) : base(initializer) {

            Akavache.BlobCache.ApplicationName = "FootballClientForms";
        }

		protected override void OnInitialized()
		{
			InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
				NavigationService.NavigateAsync("ExtendedNavigationPage/MainPage");
			}
            else
            {
				NavigationService.NavigateAsync("NavigationPage/MainPage");
			}
		}

		protected override void RegisterTypes()
		{
            Container.RegisterType<IRestClient, RestClient>();
            Container.RegisterType<FeedNewsProvider>();

            Container.RegisterType<NewsPageViewModel>();

			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<ExtendedNavigationPage>();
			Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>();
			Container.RegisterTypeForNavigation<NewsDetailsPage, NewsDetailsPageViewModel>();
            Container.RegisterTypeForNavigation<CommentsPage, CommentsPageViewModel>();
		}

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType<INavigationService, TestNavigationService>();
		}

        protected override Prism.Navigation.INavigationService CreateNavigationService()
        {
            return Container.Resolve<INavigationService>();
        }
    }

    public class ExtendedNavigationPage : NavigationPage
    {
        
    }
}
