//namespace FootballClient.DataAccess.Providers
//{
//    using FootballClient.DataAccess.Request;
//    using FootballClient.Core.Utils;
//    using FootballClient.DataAccess.Request.Parsers;
//    using FootballClient.Models.RssPhoto;
//    using System.Threading.Tasks;
//    using System.Text.RegularExpressions;
//    public class PhotosProvider
//    {
//        private Regex SpaceRegex = new Regex("[ ]{2,}", RegexOptions.None);
//        private const string PhotosPattern = "Photos_{0}";
//        public async Task<Response<RssPhoto>> LoadPhotosFeedAsync(RequestAccessMode mode = RequestAccessMode.ServerOrCache, string before = "")
//        {
//            var request = new Request<RssPhoto>();
//            request.RequestAddress = "http://football.ua/handlers/stanfy/galleries.ashx";
//            request.ResponseParser = new XmlParser<RssPhoto>();
//            if (!string.IsNullOrEmpty(before))
//            {
//                request.Parameters.Add("before", before);
//            }

//            var photosResponse = await request.SendAsync(RequestAccessMode.ServerOrCache, string.Format(PhotosPattern, before));

//            if (photosResponse.IsSuccess && photosResponse.Result != null &&
//                photosResponse.Result.Channel.IsNotNull() && photosResponse.Result.Channel.PhotoItems.IsNotNull()
//                && photosResponse.Result.Channel.PhotoItems.IsNotNull())
//            {
//                foreach (var item in photosResponse.Result.Channel.PhotoItems)
//                {
//                    item.Title = item.Title.Replace("\t", " ");
//                    item.Title = SpaceRegex.Replace(item.Title, " ");
//                    foreach (var rssChannelItemGalleryPhoto in item.Gallery.Photos)
//                    {
//                        rssChannelItemGalleryPhoto.OnDeseriazlized(item.Gallery.UrlPrefix, item.Gallery.Thumb2Prefix,
//                            item.Gallery.Url2Prefix, item.Gallery.Thumb075Prefix);
//                    }
//                }
//            }

//            return photosResponse;
//        }
//    }
//}
