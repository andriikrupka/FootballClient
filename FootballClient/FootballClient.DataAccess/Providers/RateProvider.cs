//namespace FootballClient.DataAccess.Providers
//{
//    using FootballClient.Models;
//    using FootballClient.Core.Storage;
//    using System.Threading.Tasks;
//    using Windows.ApplicationModel;
    
//    public class RateProvider
//    {
//        private const string RateReviewKey = "RateReview";

//        private const int MaxAppLaunches = 4;

//        private const int FirstRunLaunch = 1;

//        public async Task IncrementLaunchesAsync()
//        {
//            var rate = await this.LoadExpirationRateAsync();
//            rate.LaunchesCount++;
//            await this.SaveExpirationRateAsync(rate);
//        }

//        public async Task ResetLaunchesAsync()
//        {
//            var rate = await this.LoadExpirationRateAsync();
//            rate.LaunchesCount = 0;
//            await this.SaveExpirationRateAsync(rate);
//        }

//        public async Task<bool> IsNeedDisplayAsync()
//        {
//            var result = false;
//            var rate = await this.LoadExpirationRateAsync();
//            if (!rate.IsAlreadyDisplayed)
//            {
//                if (rate.LaunchesCount >= MaxAppLaunches)
//                {
//                    result = true;
//                }
//            }

//            return result;
//        }

//        public async Task<bool> IsNeedDisplayNewwestAsync()
//        {
//            var result = false;
//            var rate = await this.LoadExpirationRateAsync();
//            if (!rate.IsAlreadyDisplayed)
//            {
//                if (rate.LaunchesCount >= FirstRunLaunch)
//                {
//                    result = true;
//                }
//            }

//            return result;
//        }

//        public async Task SetAsDisplayedAsync()
//        {
//            var rate = await this.LoadExpirationRateAsync();
//            rate.IsAlreadyDisplayed = true;
//            await this.SaveExpirationRateAsync(rate);
//        }

//        private async Task<ExpirationRate> LoadExpirationRateAsync()
//        {
//            var rate = await StorageProvider.Instance.ReadFromFileAsync<ExpirationRate>(RateReviewKey);

//            var appVersion = this.GetPackgeVersion();
//            if (rate == null || rate.ApplicationVersion != appVersion)
//            {
//                rate = new ExpirationRate();
//                rate.ApplicationVersion = appVersion;
//                await this.SaveExpirationRateAsync(rate);
//            }

//            return rate;
//        }

//        private async Task SaveExpirationRateAsync(ExpirationRate rate)
//        {
//            await StorageProvider.Instance.WriteToFileAsync<ExpirationRate>(RateReviewKey, rate);
//        }

//        private string GetPackgeVersion()
//        {
//            var package = Package.Current.Id;
//            return string.Format("{0}.{1}.{2}.{3}", package.Version.Major, package.Version.Minor, package.Version.Build, package.Version.Revision);
//        }
//    }
//}
