using System.Diagnostics;
using Windows.Data.Html;
using FootballClient.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballClient.DataAccess.Request.Parsers
{
    public class MatchDetailsParser : IParserStrategy<MatchDetails>
    {
        public MatchDetails Parse(string data)
        {
            var response = new MatchDetails();
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(data);

            this.TryGetOnlineFeeds(htmlDocument.DocumentNode, response);
            this.TryGetReport(htmlDocument.DocumentNode, response);
            this.TryGetMatchDetailsTable(htmlDocument.DocumentNode, response);
            this.TryGetTeamsLogo(htmlDocument.DocumentNode, response);
            this.TryGetScoreLogo(htmlDocument.DocumentNode, response);
            this.TryGetPreview(htmlDocument.DocumentNode, response);

            return response;
        }

        private void TryGetPreview(HtmlNode document, MatchDetails matchDetails)
        {
            try
            {
                var element = document.Descendants("article").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "announcement");
                matchDetails.Preview = element?.InnerHtml;
            }
            catch (Exception ex)
            {
            }
        }

        private void TryGetScoreLogo(HtmlNode htmlNode, MatchDetails matchDetails)
        {
            try
            {
                var progressNode = htmlNode.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.StartsWith("score"));
                var spansNode = progressNode?.Descendants("span").ToList();
                if (spansNode?.Count == 2)
                {
                    matchDetails.LeftScore = HtmlUtilities.ConvertToText(spansNode[0].InnerHtml.Trim());
                    matchDetails.RightScore = HtmlUtilities.ConvertToText(spansNode[1].InnerHtml.Trim());
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void TryGetTeamsLogo(HtmlNode htmlNode, MatchDetails matchDetails)
        {
            try
            {
                var leftTeamNode = htmlNode.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "left-logo");
                if (leftTeamNode != null)
                {
                    var leftImage = leftTeamNode.Descendants("img").FirstOrDefault(x => x.Attributes.Contains("src"));
                    if (leftImage != null)
                    {
                        matchDetails.LeftTeamImage = leftImage.Attributes["src"].Value;
                        if (matchDetails.LeftTeamImage.Contains("0x35"))
                        {
                            matchDetails.LeftTeamImage = matchDetails.LeftTeamImage.Replace("0x35", "0x200");
                        }
                    }
                }

                var rightTeamNode = htmlNode.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "right-logo");
                if (rightTeamNode != null)
                {
                    var rightImage = rightTeamNode.Descendants("img").FirstOrDefault(x => x.Attributes.Contains("src"));
                    if (rightImage != null)
                    {
                        matchDetails.RightTeamImage = rightImage.Attributes["src"].Value;
                        if (matchDetails.RightTeamImage.Contains("0x35"))
                        {
                            matchDetails.RightTeamImage = matchDetails.RightTeamImage.Replace("0x35", "0x200");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void TryGetMatchDetailsTable(HtmlNode document, MatchDetails matchDetails)
        {
            try
            {
                var element = document.Descendants("table").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "match-details-table");
                if (element != null)
                {
                    var rowNodes = element.Descendants("tr").ToList();
                    foreach (var rowNode in rowNodes)
                    {
                        var row = new MatchDetailsRow();

                        var columnNodes = rowNode.Descendants("td").ToList();
                        if (columnNodes.Count == 0)
                        {
                            row.ColumnSpan = 0;
                            row.TableColumns.Add(new MatchDetailsColumn()
                            {
                                Text = HtmlUtilities.ConvertToText(rowNode.InnerHtml)
                            });
                        }
                        else
                        {
                            row.ColumnSpan = columnNodes.Count - 1;

                            foreach (var columnNode in columnNodes)
                            {
                                var matchDetailsColumn = new MatchDetailsColumn();

                                var iconNode =
                                    columnNode.Descendants("i").FirstOrDefault(x => x.Attributes.Contains("class"));
                                if (iconNode != null)
                                {
                                    matchDetailsColumn.Icon = this.GetIconImageFromIconClassName(iconNode.Attributes["class"].Value);
                                }

                                var minuteNode = columnNode.Descendants("span").FirstOrDefault();
                                if (minuteNode != null)
                                {

                                    matchDetailsColumn.Minute = HtmlUtilities.ConvertToText(minuteNode.InnerHtml);
                                }

                                var playerNode = columnNode.Descendants("a").FirstOrDefault();
                                if (playerNode != null)
                                {
                                    try
                                    {
                                        matchDetailsColumn.Text = HtmlUtilities.ConvertToText(playerNode.InnerHtml);
                                    }
                                    catch (Exception)
                                    {
                                    }

                                }
                                else
                                {
                                    if (columnNodes.IndexOf(columnNode) == 0)
                                    {
                                        playerNode = columnNode.Descendants().LastOrDefault();
                                        if (playerNode != null)
                                        {
                                            var playerText = playerNode.InnerHtml;
                                            matchDetailsColumn.Text = HtmlUtilities.ConvertToText(playerText);
                                        }
                                    }
                                    else if (columnNodes.IndexOf(columnNode) == 1)
                                    {
                                        playerNode = columnNode.Descendants().FirstOrDefault();
                                        if (playerNode != null)
                                        {
                                            try
                                            {
                                                var playerText = playerNode.InnerHtml;
                                                matchDetailsColumn.Text = HtmlUtilities.ConvertToText(playerText);
                                            }
                                            catch (Exception)
                                            {

                                                throw;
                                            }

                                        }
                                    }
                                }

                                row.TableColumns.Add(matchDetailsColumn);
                            }
                        }

                        matchDetails.MatchDetailsRows.Add(row);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void TryGetReport(HtmlNode document, MatchDetails matchDetails)
        {
            try
            {
                var element = document.Descendants("article").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "report");
                if (element != null)
                {
                    foreach (var nodes in element.ChildNodes)
                    {
                        var innerHtml = nodes.InnerHtml;
                        //if (innerHtml != "\r\n\r\n" && innerHtml.StartsWith("\r\n"))
                        //{
                        //    innerHtml = innerHtml.Remove(0,2);

                        //    if (innerHtml.EndsWith("\r\n") && innerHtml.Length - 3 > 0)
                        //    {
                        //        innerHtml = innerHtml.Substring(0, innerHtml.Length - 3);
                        //    }
                        //}

                        matchDetails.Report += innerHtml.Replace("\t", "");
                    }

                }

            }
            catch (Exception ex)
            {
            }
        }

        private void TryGetOnlineFeeds(HtmlNode document, MatchDetails matchDetails)
        {
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                var article =
                    document.Descendants("article")
                        .FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "online-match");
                if (article != null)
                {
                    var element = article.Descendants("ul").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "online-list");
                    if (element != null)
                    {
                        Debug.WriteLine("Find node: " + sw.Elapsed);
                        var matchFeeds = new List<MatchFeed>();
                        foreach (var htmlNode in element.Descendants("li"))
                        {
                            var swFeed = new Stopwatch();
                            swFeed.Start();
                            var feed = new MatchFeed();
                            var time = htmlNode.Descendants("p").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "time");
                            if (time != null)
                            {
                                feed.Time = HtmlUtilities.ConvertToText(time.InnerText).Trim();
                            }

                            var text = htmlNode.Descendants("div").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.StartsWith("text "));
                            if (text != null)
                            {
                                var innerHtml = text.InnerHtml;
                                if (innerHtml.StartsWith("<div>\r\n"))
                                {
                                    innerHtml = innerHtml.Remove(5, 2);

                                    if (innerHtml.EndsWith("\r\n") && innerHtml.Length - 3 > 0)
                                    {
                                        innerHtml = innerHtml.Substring(0, innerHtml.Length - 3);
                                    }
                                }

                                feed.TextFeed.Text = HtmlUtilities.ConvertToText(innerHtml.Replace("\t", ""));

                                var className = text.Attributes.FirstOrDefault(x => x.Value.StartsWith("text "));
                                if (className != null)
                                {
                                    feed.TextFeed.ClassName = className.Value;
                                }
                            }

                            var icon =
                                htmlNode.Descendants("p")
                                    .FirstOrDefault(
                                        x =>
                                            x.Attributes.Contains("class") &&
                                            x.Attributes["class"].Value.StartsWith("icon"));
                            if (icon != null)
                            {
                                var iconNode = icon.Attributes.FirstOrDefault(x => x.Value.StartsWith("icon"));
                                if (iconNode != null)
                                {
                                    feed.Icon = "ms-appx://" + this.GetIconImageFromIconClassName(iconNode.Value.Trim());
                                }
                            }
                            matchFeeds.Add(feed);

                            Debug.WriteLine("swFeed: " + swFeed.Elapsed);
                        }

                        if (matchFeeds.Any())
                        {
                            matchDetails.MatchFeeds = matchFeeds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            Debug.WriteLine("Parse feeds: " + sw.Elapsed);
        }

        private string GetIconImageFromIconClassName(string className)
        {
            var result = string.Empty;
            switch (className)
            {
                case "icon icon1":
                    result = "/Assets/MatchDetails/whistle.png";
                    break;

                case "icon icon7":
                case "icon icon12":
                case "goal":
                    result = "/Assets/MatchDetails/ball.png";
                    break;

                case "icon icon4":
                case "yellow-card":
                    result = "/Assets/MatchDetails/yellow_card.png";
                    break;

                case "icon icon9":
                    result = "/Assets/MatchDetails/timer.png";
                    break;

                case "icon icon3":
                    result = "/Assets/MatchDetails/change.png";
                    break;

                case "icon icon5":
                case "red-card":
                    result = "/Assets/MatchDetails/red_card.png";
                    break;

            }

            return result;
        }
    }
}
