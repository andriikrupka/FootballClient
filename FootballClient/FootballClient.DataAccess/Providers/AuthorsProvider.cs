﻿using FootballClient.DataAccess.Request.Parsers;
using FootballClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballClient.DataAccess.Providers
{
    public class AuthorsProvider
    {
        private readonly IRestClient _restClient;

        public AuthorsProvider(IRestClient restClient)
        {
            _restClient = restClient;
        }

        ////http://football.ua/handlers/stanfy/authors.ashx
        //public Task<ResponseCategory> LoadAuthorsCategoriesAsync()
        //{
        //    var request = new HttpRequestMessage();
        //    var parser = new XmlParser<ResponseCategory>();
        //    request.RequestUri = new Uri("http://football.ua/handlers/stanfy/authors.ashx");
        //    var settings = new RestSettings<>
        //    return _restClient.SendMessageAsync(request, parser);
        //}

        public Task<List<FeedItem>> LoadAuthorsFeedAsync(FeedItem lastFeedItem = null, string filterCode = "")
        {
            var request = new HttpRequestMessage();
            var parser = new RssFeedParser();
            var requestUriBuilder = new ParameterUriBuilder("http://football.ua/handlers/stanfy/news.ashx");
            requestUriBuilder.Add("type", "author");
            if (lastFeedItem != null)
            {
                var dateTimeOffset = lastFeedItem.Date.ToString("dd.MM.yyyy HH:mm:sszzz");
                dateTimeOffset = dateTimeOffset.Remove(dateTimeOffset.LastIndexOf(":"), 1);
                requestUriBuilder.Add("before", dateTimeOffset);
            }

            if (!string.IsNullOrEmpty(filterCode))
            {
                requestUriBuilder.Add("filterCode", filterCode);
            }

            request.RequestUri = requestUriBuilder.BuildParametersUri();

            var restSettings = new RestSettings<List<FeedItem>>()
                                   .AddMode(RequestAccessMode.Server)
                                   .AddParser(parser)
                                   .AddRequestMessage(request);

            return _restClient.SendAsync(restSettings, null);
        }
    }
}
