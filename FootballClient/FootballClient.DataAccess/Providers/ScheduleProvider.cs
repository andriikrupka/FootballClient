//using FootballClient.DataAccess.Request;
//using FootballClient.DataAccess.Request.Parsers;
//using FootballClient.Models.SQLiteAttributes;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Runtime.Serialization;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

//namespace FootballClient.DataAccess.Providers
//{

//    public class ScheduleProvider
//    {
//        private const string SchedulePattern = "Schedule_{0}_{1}";
        
//        private const string ScheduleCategories = "ScheduleCategories";


//        public async Task<Response<RssSchedule>> LoadScheduleAsync(string offsetDate = "", string filterCode = "",
//        RequestAccessMode mode = RequestAccessMode.ServerOrCache)
//        {
//            var request = new Request<RssSchedule>();
//            request.ResponseParser = new XmlParser<RssSchedule>();
//            request.RequestAddress = "http://football.ua/handlers/stanfy/tv.ashx";
//            if (!string.IsNullOrEmpty(offsetDate))
//            {
//                request.Parameters.Add("before", offsetDate);
//            }

//            if (!string.IsNullOrEmpty(filterCode))
//            {
//                request.Parameters.Add("filterCode", filterCode);
//            }

//            var result = await request.SendAsync();
//            return result;
//        }
//    }
//}
