using FootballClient.Models.Video;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Linq;
using System.Net;

namespace FootballClient.DataAccess.Request.Parsers
{
    public class VideoFeedParser : IParserStrategy<List<VideoItem>>
    {
        public List<VideoItem> Parse(string data)
        {
            var response = new List<VideoItem>();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(data);
            var uiElement = htmlDocument.DocumentNode.Descendants("ul").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "archive-list");
            var uiElements = htmlDocument.DocumentNode.Descendants("ul").ToList();

            if (uiElement != null)
            {
                var liElement = uiElement.Descendants("li").ToList();
                foreach (var item in liElement)
                {
                    var videoItem = new VideoItem();
                    var classType = item.Descendants("p").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "type");
                    if (classType != null)
                    {
                        videoItem.Description = classType.InnerText.Trim();
                    }

                    var dateElement = item.Descendants("p").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "date");
                    if (dateElement != null)
                    {
                        videoItem.DateString = dateElement.InnerText.Trim();
                    }

                    var aElement = item.Descendants("a").FirstOrDefault(x => x.Attributes.Contains("href") && !string.IsNullOrEmpty(x.InnerText.Trim()));
                    if (aElement != null)
                    {
                        if (aElement.InnerText.ToUpperInvariant().Contains("LIVE"))
                        {
                            continue;
                        }

                        videoItem.Name = WebUtility.HtmlDecode(aElement.InnerText);
                        videoItem.DetailLink = aElement.Attributes["href"].Value;
                    }
                    else
                    {
                        continue;
                    }

                    var imageElement = item.Descendants("img").FirstOrDefault();
                    if (imageElement != null)
                    {


                        if (imageElement.Attributes.Contains("src"))
                        {
                            videoItem.Thumbnail = imageElement.Attributes["src"].Value;
                        }
                    }

                    response.Add(videoItem);
                }
            }

            return response;
        }
    }
}