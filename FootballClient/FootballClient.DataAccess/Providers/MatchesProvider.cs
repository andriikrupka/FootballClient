//using System.Threading;

//namespace FootballClient.DataAccess.Providers
//{
//    using FootballClient.DataAccess.Request;
//    using FootballClient.DataAccess.Request.Parsers;
//    using FootballClient.Models;
//    using System;
//    using System.Threading.Tasks;

//    public class MatchesProvider
//    {
//        public async Task<Response<MatchesResponse>> LoadMatchesAsync(DateTime date, RequestAccessMode mode)
//        {
//            return await this.LoadMatchesAsync(date, mode, CancellationToken.None);
//        }
//        public async Task<Response<MatchesResponse>> LoadMatchesAsync(DateTime date, RequestAccessMode mode, CancellationToken cancellationToken)
//        {
//            var request = new Request<MatchesResponse>();
//            var month = date.Month;
//            var year = date.Year;
//            var day = date.Day - 1;
//            if (day == 0)
//            {
//                month -= 1;
//                if (month == 0)
//                {
//                    year -= 1;
//                    month = 12;
//                }

//                day = DateTime.DaysInMonth(date.Year, month);
//            }
//            var dtKey = string.Format("{0}-{1}-{2}T00:00:00", year, month, day);
//            request.Parameters.Add("dt", dtKey);
//            request.Parameters.Add("direction", "false");
//            request.Parameters.Add("callback", "");
//            request.Parameters.Add("_1413706301734", "");
//            request.ResponseParser = new JsonParser<MatchesResponse>();
//            request.RequestAddress = "http://services.football.ua/api/FootballGameBlockMain/GameList";
//            return await request.SendAsync(cancellationToken, mode, dtKey);
//        }

//        public async Task<Response<MatchDetails>> LoadMatchDetailsAsync( RequestAccessMode mode, string detailsLink, int gameId)
//        {
//            var request = new Request<MatchDetails>();
//            request.RequestAddress = detailsLink;
//            request.ResponseParser = new MatchDetailsParser();
//            //request.ResponseParser = new MatchDetailsAngleParser();
//            System.Diagnostics.Debug.WriteLine("LoadMatchDetailsAsync:");
//            return  await request.SendAsync(mode, string.Format("Mathc_{0}", gameId));
//        }
//    }
//}
