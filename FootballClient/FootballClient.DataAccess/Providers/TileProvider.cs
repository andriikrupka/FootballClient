namespace FootballClient.DataAccess.Providers
{
    using FootballClient.DataAccess.Request;
    using FootballClient.Models;
    //using NotificationsExtensions.TileContent;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.UI.Notifications;

//    public class TileProvider
//    {
//        public void ClearMainTile()
//        {
//            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
//            tileUpdater.Clear();
//        }
//
//        public async Task UpdateMainTile()
//        {
//            var feedResponse = await DataProviders.FeedNewsProvider.LoadFeedNewsAsync(mode: RequestAccessMode.Server);
//            if (feedResponse.IsSuccess && feedResponse.Result.IsNotNull() && feedResponse.Result.Count > 5)
//            {
//                var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
//                tileUpdater.Clear();
//                tileUpdater.EnableNotificationQueue(true);
//                var newsSequence = feedResponse.Result.Take(5).ToList();
//
//                var firstNews = newsSequence[0];
//                var wideTile = this.Create350x150Tile(newsSequence);
//                var squareTile = TileContentFactory.CreateTileSquare150x150PeekImageAndText04();
//                squareTile.Image.Src = firstNews.DescriptionImage;
//                squareTile.Branding = TileBranding.Name;
//                squareTile.TextBodyWrap.Text = firstNews.Annotation;
//                wideTile.RequireSquare150x150Content = true;
//                wideTile.Square150x150Content = squareTile;
//                var tileNotification = wideTile.CreateNotification();
//                tileUpdater.Update(tileNotification);
//
//                foreach (var feedNew in newsSequence.Skip(1))
//                {
//                    squareTile = TileContentFactory.CreateTileSquare150x150PeekImageAndText04();
//                    squareTile.Image.Src = feedNew.DescriptionImage;
//                    squareTile.Branding = TileBranding.Logo;
//                    squareTile.TextBodyWrap.Text = feedNew.Annotation;
//                    tileNotification = squareTile.CreateNotification();
//                    tileUpdater.Update(tileNotification);
//                }
//            }
//        }
//
//        private ITileWide310x150PeekImageCollection01 Create350x150Tile(IReadOnlyList<FeedItem> feedResponse )
//        {
//            var collectionTiles = TileContentFactory.CreateTileWide310x150PeekImageCollection01();
//            collectionTiles.Branding = TileBranding.Logo;
//            collectionTiles.ImageMain.Src = feedResponse[0].DescriptionImage;
//            collectionTiles.ImageSmallColumn1Row1.Src = feedResponse[1].DescriptionImage;
//            collectionTiles.ImageSmallColumn1Row2.Src = feedResponse[2].DescriptionImage;
//            collectionTiles.ImageSmallColumn2Row1.Src = feedResponse[3].DescriptionImage;
//            collectionTiles.ImageSmallColumn2Row2.Src = feedResponse[4].DescriptionImage;
//
//            collectionTiles.TextHeading.Text = "Самые свежие новости";
//            collectionTiles.TextBodyWrap.Text = feedResponse[0].Title + 
//                                                Environment.NewLine +
//                                                feedResponse[1].Title +
//                                                Environment.NewLine + 
//                                                feedResponse[2].Title +
//                                                Environment.NewLine + 
//                                                feedResponse[3].Title;
//
//            return collectionTiles;
//        }
//    }
}
