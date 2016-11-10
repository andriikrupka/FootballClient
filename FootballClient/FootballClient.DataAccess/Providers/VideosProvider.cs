//using FootballClient.DataAccess.Request;
//using FootballClient.DataAccess.Request.Parsers;
//using FootballClient.Models.Video;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FootballClient.DataAccess.Providers
//{
//    public class VideosProvider
//    {
//        public async Task<Response<List<VideoItem>>> LoadVideosAsync(int page)
//        {
//            var request = new Request<List<VideoItem>>();
//            request.ResponseParser= new VideoFeedParser();
//            request.RequestAddress = string.Format("http://football.ua/video/page{0}.html", page);

//            return await request.SendAsync(RequestAccessMode.Server);
//        }

//        public Task<Response<string>> LoadVideoLinkAsync(string videoLink)
//        {
//            var request = new Request<string>();
//            request.ResponseParser = new VideoDetailsParser();
//            request.RequestAddress = videoLink;

//            return request.SendAsync(RequestAccessMode.Server);
//        }
//    }
//}
