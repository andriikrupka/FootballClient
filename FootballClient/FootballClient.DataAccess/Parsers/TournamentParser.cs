
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using FootballClient.Models;
using HtmlAgilityPack;

namespace FootballClient.DataAccess.Request.Parsers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;


    public class TournamentParser : IParserStrategy<Tournament>
    {
        private static readonly Regex FindIdRegex = new Regex("[0-9]{1,5}");

        public Tournament Parse(string data)
        {
            var tournamentResponse = new Tournament()
            {
                Championat = new ObservableCollection<Championat>(),
                Rows = new List<ITournamentRow>()
            };

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(data);

            var wrapper =
                htmlDocument.DocumentNode.Descendants("div")
                    .FirstOrDefault(x => x.Attributes.Contains("id") && x.Attributes["id"].Value == "wrapper");
            if (wrapper != null)
            {
                var ulElements =
                    wrapper.Descendants("ul")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "list");


                foreach (var childNode in ulElements)
                {
                    var championat = new Championat();
                    championat.GameLists = new List<GameList>();
                    if (childNode.ParentNode.Name == "h2" && childNode.ParentNode.Attributes.Contains("class")
                        && childNode.ParentNode.Attributes["class"].Value == "title leftside")
                    {
                        var h2Node = childNode.ParentNode;
                        if (h2Node.FirstChild != null && h2Node.FirstChild.Name == "#text")
                        {
                            championat.TourName = h2Node.FirstChild.InnerText;
                        }

                        var previousSibling = childNode.ParentNode.PreviousSibling;
                        if (previousSibling != null && previousSibling.Name == "h1")
                        {
                            championat.Name = previousSibling.InnerText;
                        }
                    }


                    var liElement = childNode.Descendants("li").ToList();
                    if (liElement != null && liElement.Any())
                    {
                        foreach (var htmlNode in liElement)
                        {

                            var gameList = this.FindGameList(htmlNode);
                            championat.GameLists.Add(gameList);
                        }
                        tournamentResponse.Championat.Add(championat);
                    }
                    else
                    {
                        var gameList = this.FindGameList(childNode);
                        championat.GameLists.Add(gameList);
                        tournamentResponse.Championat.Add(championat);
                    }
                }

                var tableNode =
                    wrapper.Descendants("table")
                        .FirstOrDefault(
                            x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "turnir-table");
                if (tableNode != null)
                {
                    var tBody = tableNode.Descendants("tbody").FirstOrDefault();
                    if (tBody != null)
                    {
                        var rows = tBody.Descendants("tr");
                        if (rows != null)
                        {
                            var position = 1;
                            foreach (var htmlNode in rows)
                            {

                                if (htmlNode.ChildNodes.Count == 9)
                                {
                                    var tableRow = new TournamentRow();
                                    tableRow.Position = position++;
                                    tableRow.TeamName = htmlNode.ChildNodes[0].InnerText;
                                    tableRow.Games = htmlNode.ChildNodes[1].InnerText;
                                    tableRow.Wins = htmlNode.ChildNodes[2].InnerText;
                                    tableRow.Draws = htmlNode.ChildNodes[3].InnerText;
                                    tableRow.Loses = htmlNode.ChildNodes[4].InnerText;
                                    tableRow.GoalsScored = htmlNode.ChildNodes[5].InnerText;
                                    tableRow.GoalsConceded = htmlNode.ChildNodes[6].InnerText;
                                    tableRow.GoalsDifference = htmlNode.ChildNodes[7].InnerText;
                                    tableRow.Points = htmlNode.ChildNodes[8].InnerText;

                                    tournamentResponse.Rows.Add(tableRow);
                                }
                                else if (htmlNode.ChildNodes.Count == 1)
                                {
                                    position = 1;
                                    tournamentResponse.Rows.Add(new SubHeaderRow()
                                    {
                                        Text = htmlNode.ChildNodes[0].InnerText
                                    });
                                }
                            }
                        }
                    }
                }
            }

            return tournamentResponse;
        }

        private GameList FindGameList(HtmlNode htmlNode)
        {
            var gameList = new GameList();
            var dateSpan = htmlNode.Descendants("span").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "list-itemdata");
            if (dateSpan != null)
            {
                gameList.Time = dateSpan.InnerText;
            }

            var firstTeamSpan = htmlNode.Descendants("span").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "list-itemname");
            if (firstTeamSpan != null)
            {
                gameList.NameOne = firstTeamSpan.InnerText;
            }

            var secondTeamSpan = htmlNode.Descendants("span").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "list-itemname rightname");
            if (firstTeamSpan != null)
            {
                gameList.NameTwo = secondTeamSpan.InnerText;
            }

            var link = htmlNode.Descendants("a").FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "list-item-a-link");
            if (link != null && link.Attributes.Contains("href"))
            {
                var id = FindIdRegex.Match(link.Attributes["href"].Value);
                if (id.Success)
                {
                    gameList.GameId = int.Parse(id.Value);
                    //gameList.N = int.Parse(id.Value);
                    gameList.MatchLink = "http://football.ua/games_online/" + gameList.GameId + ".html";
                }
            }

            var scores =
           htmlNode.Descendants("span")
               .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "match").ToList();
            if (scores != null && scores.Count == 2)
            {
                if (scores[0].ParentNode.Name == "div" && scores[0].ParentNode.Attributes.FirstOrDefault(x => x.Value == "score red") != null)
                {
                    gameList.CssClass = "inprogress";
                }

                gameList.TeamScoreOne = scores[0].InnerText;
                gameList.TeamScoreTwo = scores[1].InnerText;
            }

            return gameList;
        }
    }
}
