//using FootballClient.Models.Interfaces;
//using GalaSoft.MvvmLight.Ioc;

//namespace FootballClient.DataAccess.Providers
//{
//    public class DataProviders
//    {
//        static DataProviders()
//        {
//            FeedNewsProvider = new FeedNewsProvider();
//            ScheduleProvider = new ScheduleProvider();
//            FictionProvider = new FictionProvider();
//            AuthorsProvider = new AuthorsProvider();
//            PhotosProvider = new PhotosProvider();
//            CommentsProvider = new CommentsProvider();
//            MatchesProvider = new MatchesProvider();
//            RateProvider = new RateProvider();
//            //TileProvider = new TileProvider();
//            //SettingsProvider = new SettingsProvider();
//            MenuProvider = new MenuProvider();
//            TournamentsProvider = new TournamentsProvider();
//            VideosProvider = new VideosProvider();
//            DataBaseProvider = SimpleIoc.Default.GetInstance<IDataBaseProvider>();
//            ApplicationManager = SimpleIoc.Default.GetInstance<ApplicationManager>(); 
//        }

//        public static FeedNewsProvider FeedNewsProvider { get; private set; }

//        public static ScheduleProvider ScheduleProvider { get; private set; }

//        public static FictionProvider FictionProvider { get; private set; }

//        public static AuthorsProvider AuthorsProvider { get; private set; }

//        public static PhotosProvider PhotosProvider { get; private set; }

//        public static CommentsProvider CommentsProvider { get; private set; }

//        public static MatchesProvider MatchesProvider { get; private set; }

//        public static RateProvider RateProvider { get; private set; }

//        //public static TileProvider TileProvider { get; private set; }

//        //public static SettingsProvider SettingsProvider { get; private set; }

//        public static TournamentsProvider TournamentsProvider { get; private set; }

//        public static VideosProvider VideosProvider { get; private set; }
//        public static IDataBaseProvider DataBaseProvider { get; private set; }
//        public static MenuProvider MenuProvider { get; private set; }

//        public static ApplicationManager ApplicationManager { get; private set; }
//    }
//}
