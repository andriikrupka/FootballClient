using FootballClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Html;

namespace FootballClient.DataAccess.Request.Parsers
{
    //public class MatchDetailsAngleParser : Parser<MatchDetails>
    //{
    //    public override Response<MatchDetails> ParseSafe(Stream stream)
    //    {
    //        var response = new Response<MatchDetails>
    //        {
    //            Result = new MatchDetails()
    //        };

    //        var htmlParser = new HtmlParser();
    //        var content = string.Empty;
    //        using (var streamReader = new StreamReader(stream))
    //        {
    //            content = streamReader.ReadToEnd();
    //        }

            
    //        var document = htmlParser.Parse(content);
    //        TryGetOnlineFeeds(document, response.Result);
    //        TryGetReport(document, response.Result);
    //        TryGetMatchDetailsTable(document, response.Result);
    //        //TryGetTeamsLogo(document, response.Result);
    //        //TryGetScoreLogo(document, response.Result);
    //        //TryGetPreview(document, response.Result);

    //        return response;
    //    }

    //    private void TryGetMatchDetailsTable(IHtmlDocument document, MatchDetails result)
    //    {
    //        var element = document.QuerySelector("table.match-details-table");
    //        if (element != null)
    //        {
    //            var rowNodes = element.QuerySelectorAll("tr");
    //            foreach (var rowNode in rowNodes)
    //            {
    //                var row = new MatchDetailsRow();
    //                var columnNodes = rowNode.QuerySelectorAll("td").ToList();
    //                if (columnNodes.Count == 0)
    //                {
    //                    row.ColumnSpan = 0;
    //                    row.TableColumns.Add(new MatchDetailsColumn()
    //                    {
    //                        Text = HtmlUtilities.ConvertToText(rowNode.InnerHtml)
    //                    });
    //                }
    //                else
    //                {
    //                    row.ColumnSpan = columnNodes.Count - 1;

    //                    foreach (var columnNode in columnNodes)
    //                    {
    //                        var matchDetailsColumn = new MatchDetailsColumn();

    //                        var iconNode = columnNode.QuerySelector("i");
    //                        //columnNode.Descendants("i").FirstOrDefault(x => x.Attributes.Contains("class"));
    //                        if (iconNode != null)
    //                        {
    //                            matchDetailsColumn.Icon = this.GetIconImageFromIconClassName(iconNode.Attributes["class"].Value);
    //                        }

    //                        var minuteNode = columnNode.QuerySelector("span");
    //                        if (minuteNode != null)
    //                        {
    //                            matchDetailsColumn.Minute = HtmlUtilities.ConvertToText(minuteNode.InnerHtml);
    //                        }

    //                        var playerNode = columnNode.QuerySelector("a");
    //                        if (playerNode != null)
    //                        {
    //                            try
    //                            {
    //                                matchDetailsColumn.Text = HtmlUtilities.ConvertToText(playerNode.InnerHtml);
    //                            }
    //                            catch (Exception)
    //                            {
    //                            }

    //                        }
    //                        else
    //                        {
    //                            if (columnNodes.IndexOf(columnNode) == 0)
    //                            {
    //                                //playerNode = columnNode.Descendants().LastOrDefault();
    //                                //if (playerNode != null)
    //                                //{
    //                                //    var playerText = playerNode.InnerHtml;
    //                                //    matchDetailsColumn.Text = HtmlUtilities.ConvertToText(playerText);
    //                                //}
    //                            }
    //                            else if (columnNodes.IndexOf(columnNode) == 1)
    //                            {
    //                                //playerNode = columnNode.Descendants().FirstOrDefault();
    //                                //if (playerNode != null)
    //                                //{
    //                                //    try
    //                                //    {
    //                                //        var playerText = playerNode.InnerHtml;
    //                                //        matchDetailsColumn.Text = HtmlUtilities.ConvertToText(playerText);
    //                                //    }
    //                                //    catch (Exception)
    //                                //    {
    //                                //
    //                                //        throw;
    //                                //    }
    //                                //
    //                                //}
    //                            }
    //                        }

    //                        row.TableColumns.Add(matchDetailsColumn);
    //                    }
    //                }

    //                result.MatchDetailsRows.Add(row);
    //            }
    //        }
    //    }

    //    private void TryGetReport(IHtmlDocument document, MatchDetails result)
    //    {
    //        var element = document.QuerySelector("article.report");
    //        if (element != null)
    //        {
    //            result.Report += element.InnerHtml.Replace("\t", "");
    //        }

    //    }

    //    private void TryGetOnlineFeeds(IHtmlDocument document, MatchDetails matchDetails)
    //    {
    //        var article = document.QuerySelector("article.online-match");
    //        if (article != null)
    //        {
    //            var element = article.QuerySelector("ul.online-list");
    //            if (element != null)
    //            {
    //                var matchFeeds = new List<MatchFeed>();
    //                foreach (var htmlNode in element.QuerySelectorAll("li"))
    //                {
    //                    var feed = new MatchFeed();
    //                    var time = htmlNode.QuerySelector("p.time");
    //                    if (time != null)
    //                    {
    //                        feed.Time = HtmlUtilities.ConvertToText(time.TextContent).Trim();
    //                    }

    //                    var text = htmlNode.QuerySelector("div.text ");
    //                    if (text != null)
    //                    {
    //                        var innerHtml = text.InnerHtml;
    //                        if (innerHtml.StartsWith("<div>\r\n"))
    //                        {
    //                            innerHtml = innerHtml.Remove(5, 2);

    //                            if (innerHtml.EndsWith("\r\n") && innerHtml.Length - 3 > 0)
    //                            {
    //                                innerHtml = innerHtml.Substring(0, innerHtml.Length - 3);
    //                            }
    //                        }

    //                        feed.TextFeed.Text = HtmlUtilities.ConvertToText(innerHtml.Replace("\t", ""));

    //                        var className = text.Attributes.FirstOrDefault(x => x.Value.StartsWith("text "));
    //                        if (className != null)
    //                        {
    //                            feed.TextFeed.ClassName = className.Value;
    //                        }
    //                    }

    //                    var icon = htmlNode.QuerySelector("p.icon");
    //                    if (icon != null)
    //                    {
    //                        var iconNode = icon.Attributes.FirstOrDefault(x => x.Value.StartsWith("icon"));
    //                        if (iconNode != null)
    //                        {
    //                            feed.Icon = "ms-appx://" + this.GetIconImageFromIconClassName(iconNode.Value.Trim());
    //                        }
    //                    }
    //                    matchFeeds.Add(feed);
    //                }

    //                if (matchFeeds.Any())
    //                {
    //                    matchDetails.MatchFeeds = matchFeeds;
    //                }
    //            }
    //        }
    //    }

    //    private string GetIconImageFromIconClassName(string className)
    //    {
    //        var result = string.Empty;
    //        switch (className)
    //        {
    //            case "icon icon1":
    //                result = "/Assets/MatchDetails/whistle.png";
    //                break;

    //            case "icon icon7":
    //            case "icon icon12":
    //            case "goal":
    //                result = "/Assets/MatchDetails/ball.png";
    //                break;

    //            case "icon icon4":
    //            case "yellow-card":
    //                result = "/Assets/MatchDetails/yellow_card.png";
    //                break;

    //            case "icon icon9":
    //                result = "/Assets/MatchDetails/timer.png";
    //                break;

    //            case "icon icon3":
    //                result = "/Assets/MatchDetails/change.png";
    //                break;

    //            case "icon icon5":
    //            case "red-card":
    //                result = "/Assets/MatchDetails/red_card.png";
    //                break;

    //        }

    //        return result;
    //    }
    //}

}