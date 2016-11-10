//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using FootballClient.DataAccess.Request;
//using FootballClient.DataAccess.Request.Parsers;
//using FootballClient.Models;

//namespace FootballClient.DataAccess.Providers
//{
//    public class TournameData
//    {
//        public string MathcLinkPattern { get; set; }

//        public string ChampionatName { get; set; }
//    }
//    public class TournamentsProvider
//    {
//        private const string TournamentsCategories = "TournamentsCategories";

//        private static readonly Dictionary<string, TournameData> FilterCodeDictionary = new Dictionary<string, TournameData>()
//        {
//            {
//                "18", new TournameData()
//                {
//                    ChampionatName = "УКРАИНА. ПРЕМЬЕР-ЛИГА",
//                    MathcLinkPattern = "http://football.ua/ukraine/game/{0}.html"
//                }
//            },

//            {
//                "15", new TournameData()
//                {
//                    MathcLinkPattern = "http://football.ua/england/game/{0}.html",
//                    ChampionatName = "АНГЛИЯ. ПРЕМЬЕР-ЛИГА"
//                }
//            },

//            {
//                "61", new TournameData()
//                {
//                    MathcLinkPattern ="http://football.ua/argentina/game/{0}.html",
//                    ChampionatName = "Аргентина. ПРИМЕРА"
//                }
//            },

//            {
//                "51", new TournameData()
//                {
//                    MathcLinkPattern ="http://football.ua/brazil/game/{0}.html",
//                    ChampionatName = "Бразилия"
//                }
//            },

//            {
//                "13", new TournameData()
//                {
//                    MathcLinkPattern="http://football.ua/germany/game/{0}.html",
//                    ChampionatName = "ГЕРМАНИЯ. БУНДЕСЛИГА"
//                }
//            },

//            {
//                "12", new TournameData()
//                {
//                    MathcLinkPattern = "http://football.ua/italy/game/{0}.html",
//                    ChampionatName = "ИТАЛИЯ. СЕРИЯ А"
//                }
//            },

//            {
//                "10", new TournameData()
//                {
//                    MathcLinkPattern = "http://football.ua/spain/game/{0}.html",
//                    ChampionatName = "ИСПАНИЯ. ЛА ЛИГА"
//                }
//            },

//            {
//                "38", new TournameData()
//                {
//                    MathcLinkPattern = "http://football.ua/netherlands/game/{0}.html",
//                    ChampionatName = "НИДЕРЛАНДЫ. ЭРЕДИВИЗИЯ"
//                }
//            },


//            {
//                "32", new TournameData()
//                {
//                    MathcLinkPattern = "http://football.ua/portugal/game/{0}.html",
//                    ChampionatName = "ПОРТУГАЛИЯ. ПРЕМЬЕР-ЛИГА"
//                }
//            },

            
//            {
//                "14", new TournameData()
//                {
//                    MathcLinkPattern = "http://football.ua/france/game/{0}.html",
//                    ChampionatName = "ФРАНЦИЯ. ЛИГА 1"
//                }
//            },

//            {
//                "16", new TournameData()
//                {
//                    MathcLinkPattern = "http://football.ua/champions_league/game/{0}.html",
//                    ChampionatName = "Лига Чемпионов"
//                }

//            },

//            {
//                "58",new TournameData()
//                {
//                    MathcLinkPattern =  "http://football.ua/default.aspx?menu_id=football_europaleague_game&game_id={0}",
//                    ChampionatName = "Лига Европы"
//                }
//            },

//            {
//                "62", new TournameData()
//                {
//                    MathcLinkPattern = "http://football.ua/default.aspx?menu_id=football_turkey_game&game_id={0}",
//                    ChampionatName = "ТУРЦИЯ. СУПЕРЛИГА"
//                }
//            },
            
//            {
//                "20", new TournameData()
//                {
//                    MathcLinkPattern = "http://worldcup2014.football.ua/game/{0}.html",
//                    ChampionatName = "ЧЕМПИОНАТ МИРА"
//                }
//            }
//        };

//        public async Task<Response<ResponseCategory>> LoadTournamentCategoriesAsync()
//        {
//            var request = new Request<ResponseCategory>();
//            request.ResponseParser = new XmlParser<ResponseCategory>();
//            request.RequestAddress = "http://football.ua/handlers/stanfy/cups.ashx";

//            return await request.SendAsync(RequestAccessMode.CacheOrServer, TournamentsCategories);
//        }

//        public async Task<Response<Tournament>> LoadTournamentDetailsAsync(string code)
//        {
//            //http://football.ua/handlers/stanfy/cup.ashx?filterCode=18
//            var pattern = "tournament_{0}";
//            var request = new Request<Tournament>();
//            request.ResponseParser = new TournamentParser();
//            request.RequestAddress = "http://football.ua/handlers/stanfy/cup.ashx";
//            request.Parameters.Add("filterCode", code);

//            var result = await request.SendAsync(RequestAccessMode.Server, string.Format(pattern, code));

//            if (FilterCodeDictionary.ContainsKey(code))
//            {
//                if (result.IsSuccess && result.Result != null
//                    && result.Result.Championat != null)
//                {
//                    foreach (var championat in result.Result.Championat)
//                    {
//                        foreach (var gameList in championat.GameLists)
//                        {
//                            gameList.MatchLink = string.Format(FilterCodeDictionary[code].MathcLinkPattern, gameList.GameId);
//                            gameList.ChampName = FilterCodeDictionary[code].ChampionatName;
//                            gameList.TourName = championat.TourName;
//                        }
//                    }
//                }
//            }

//            return result;
//        }


//    }
//}
