using FootballClient.DataAccess.Request;
using FootballClient.DataAccess.Request.Parsers;
using FootballClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballClient.DataAccess.Providers
{
    public class FictionProvider
    {
        private const string FictionPattern = "FictionNews_{0}_{1}";

        private const string FictionCategories = "FictionCategories";
        private readonly IRestClient _restClient;

        public FictionProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }

        //http://football.ua/handlers/stanfy/newsfilters.ashx?type=fiction
        //public Task<ResponseCategory> LoadFictionCategoriesAsync()
        //{
        //    var request = new HttpRequestMessage();
        //    var parser = new XmlParser<ResponseCategory>();
        //    request.RequestUri = new Uri("http://football.ua/handlers/stanfy/newsfilters.ashx?type=fiction");
        //    return _restClient.SendAsync(request, parser);
        //}

        //public async Task<List<FeedItem>> LoadFictionAsync(FeedItem lastFeedItem = null, string filterCode = "", RequestAccessMode mode = RequestAccessMode.Server)
        //{
        //    var request = new HttpRequestMessage();
        //    var parser = new RssFeedParser();
        //    var requestUriBuilder = new ParameterUriBuilder("http://football.ua/handlers/stanfy/news.ashx");
        //    requestUriBuilder.Add("type", "fiction");
        //    if (lastFeedItem != null)
        //    {
        //        var offsetDate = lastFeedItem.Date.ToString("dd.MM.yyyy HH:mm:sszzz");
        //        offsetDate = offsetDate.Remove(offsetDate.LastIndexOf(":"), 1);
        //        requestUriBuilder.Add("before", offsetDate);
        //    }
        //
        //    if (!string.IsNullOrEmpty(filterCode))
        //    {
        //        requestUriBuilder.Add("filterCode", filterCode);
        //    }
        //    request.RequestUri = requestUriBuilder.BuildParametersUri();
        //
        //    return await _restClient.SendMessageAsync(request, parser, mode);
        //}
        
    }
}
