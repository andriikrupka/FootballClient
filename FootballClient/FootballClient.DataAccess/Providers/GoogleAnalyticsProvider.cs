//namespace FootballClient.DataAccess.Providers
//{
//    using FootballClient.Core.Navigation;
//    using FootballClient.Core.Navigation.NavigationEventArgs;
//    using GoogleAnalytics.Core;

//    public class GoogleAnalyticsProvider
//    {
//        private const string TrackingPattern = "Page: {0}";

//        private static GoogleAnalyticsProvider instance;

//        private static Tracker analyticsTracker;

//        public static GoogleAnalyticsProvider Instance
//        {
//            get
//            {
//                if (instance == null)
//                {
//                    instance = new GoogleAnalyticsProvider();
//                }

//                return instance;
//            }
//        }

//        public void SetTraker(Tracker tracker)
//        {
//            analyticsTracker = tracker;
//        }

//        public void SetStartSession(bool value)
//        {
//#if !DEBUG
//            analyticsTracker.SetStartSession(value);
//#endif
//        }

//        public void SetEndSession(bool value)
//        {
//#if !DEBUG
//            analyticsTracker.SetEndSession(value);
//#endif
//        }

//        public void SendView(string screenName)
//        {
//#if !DEBUG
//            analyticsTracker.SendView(screenName);
//#endif
//        }

//        public void SendException(string exception)
//        {
//#if !DEBUG
//            analyticsTracker.SendException(exception, true);
//#endif
//        }


//        public void SendEvent(string category, string action, string label)
//        {
//#if !DEBUG
//            this.SendEvent(category, action, label, 0);      
//#endif
//        }


//        public void SendEvent(string category, string action, string label, long value)
//        {
//#if !DEBUG
//            analyticsTracker.SendEvent(category, action, label, value);
//#endif
//        }

//        private void SendEvent(string category, string action, string label, int value)
//        {
//#if !DEBUG
//            analyticsTracker.SendEvent(category, action, label, value);
//#endif
//        }

//        public void StartListenPages()
//        {
//            NavigationProvider.Instance.Navigated += this.OnNavigatedAnalyticPage;
//        }

//        private void OnNavigatedAnalyticPage(object sender, NavigationProviderEventArgs e)
//        {
//#if !DEBUG
//            var view = string.Format(TrackingPattern, e.ToSource.AssociatedSource);
//            analyticsTracker.SendView(view);
//#endif
//        }
//    }
//}
